using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool isMoving = false;



    // Start is called before the first frame update
    void Start()
    {
        nextPos = transform.position;
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
        Debug.DrawRay(nextPos + dir * unit, Vector3.down * 5, Color.red);
        while (Physics.Raycast(nextPos + dir * unit, Vector3.down, out hit, 5f))
        {
            if (hit.transform.CompareTag("Brick") || hit.transform.CompareTag("UnBrick"))
            {
                nextPos = nextPos + dir * unit;
            }
            else break;
        }
        Debug.Log(nextPos);
        return nextPos;
    }
}
