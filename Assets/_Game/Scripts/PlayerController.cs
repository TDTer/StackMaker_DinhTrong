using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private RaycastHit hit;
    public enum Direction
    {
        None,
        Forward,
        Back,
        Right,
        Left,
    }
    private float unit = 1.0f;
    private Vector3 dir;
    Vector3 nextPos;

    public Direction direction;
    public float speed = 10.0f;
    public bool isMoving;
    public bool isFinish;
    public GameObject brickPrefab;

    private int countBrick;
    private float brickHeight;
    private float chestWidth;
    private GameObject[] listOfBricks;



    // Start is called before the first frame update
    void Start()
    {
        nextPos = transform.position;
        countBrick = 0;
        brickHeight = brickPrefab.GetComponent<MeshRenderer>().bounds.size.y;
        listOfBricks = new GameObject[100];
        isMoving = false;
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        if (direction != Direction.None && !isMoving)
        {
            nextPos = NextPosition(direction);
            isMoving = true;
        }

        Move(direction, nextPos);
        if (Vector3.Distance(transform.position, nextPos) < 0.1f)
        {
            isMoving = false;
            direction = Direction.None;
        }
    }


    private void CheckDirection()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Moved || TouchPhase.Moved == TouchPhase.Ended)
            {
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    direction = Direction.None;
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    direction = x > 0 ? Direction.Right : Direction.Left;
                }
                else
                {
                    direction = y > 0 ? Direction.Forward : Direction.Back;
                }
            }
        }
    }

    private void Move(Direction direction, Vector3 nextPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private Vector3 NextPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                dir = Vector3.forward;
                break;

            case Direction.Back:
                dir = Vector3.back;
                break;

            case Direction.Right:
                dir = Vector3.right;
                break;

            case Direction.Left:
                dir = Vector3.left;
                break;
            default:
                return transform.position;
        }

        Vector3 nextPos = transform.position;


        while (Physics.Raycast(nextPos + dir * unit, Vector3.down, out hit, 5f))
        {
            if (hit.transform.CompareTag("Brick") || hit.transform.CompareTag("UnBrick"))
            {
                //Debug.Log(hit.transform.tag);
                nextPos = nextPos + dir * unit;
            }
            else break;
        }
        //Debug.DrawRay(nextPos + new Vector3(0, 3, 0), Vector3.down * 5, Color.red);
        // )
        if (Physics.Raycast(nextPos + dir * unit, Vector3.down, out hit, 5f) && hit.transform.CompareTag("Destination"))
        {
            isFinish = true;
            GameObject finishObject = hit.transform.gameObject;
            GameObject chestObject = finishObject.transform.Find("baoxiang_close").gameObject;

            chestWidth = chestObject.GetComponent<Collider>().bounds.size.z;
            Vector3 finishPos = chestObject.transform.position - new Vector3(0, 0, chestWidth / 2);
            nextPos = finishPos;
        }
        return nextPos;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Brick"))
        {
            if (collisionInfo.gameObject.GetComponent<Renderer>().enabled)
            {
                collisionInfo.gameObject.GetComponent<Renderer>().enabled = false;
                AddBrick(collisionInfo.gameObject);
            }
        }

        if (collisionInfo.gameObject.name == "zhongdian")
        {
            //Debug.Log("Clear Brick");
            ClearBrick();
        }
    }
    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("UnBrick"))
        {
            RemoveBrick();
            Instantiate(brickPrefab, collisionInfo.transform.position, Quaternion.Euler(-90, 0, -180));
        }
        Debug.Log(countBrick);
    }

    void AddBrick(GameObject newBrick)
    {
        GameObject myObject = Instantiate(brickPrefab, transform.position, Quaternion.Euler(-90, 0, -180));
        myObject.transform.parent = transform;
        myObject.tag = "Untagged";
        myObject.GetComponent<BoxCollider>().enabled = false;
        myObject.transform.position = new Vector3(myObject.transform.position.x, myObject.transform.position.y + brickHeight * countBrick - 0.35f, myObject.transform.position.z);
        listOfBricks[countBrick] = myObject;

        countBrick++;
        GameObject child = transform.Find("jiao").gameObject;
        if (countBrick > 1) child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + brickHeight, child.transform.position.z);
    }

    void RemoveBrick()
    {
        countBrick--;
        Destroy(listOfBricks[countBrick]);
        GameObject child = transform.Find("jiao").gameObject;
        if (countBrick > 1) child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - brickHeight, child.transform.position.z);
    }

    void ClearBrick()
    {
        GameObject child = transform.Find("jiao").gameObject;
        for (int i = 0; i < countBrick; i++)
        {
            Destroy(listOfBricks[i]);
            if (i > 0) child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - brickHeight, child.transform.position.z);
        }
    }
}
