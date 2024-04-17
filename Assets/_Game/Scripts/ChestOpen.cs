using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public GameObject openChestObject;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        transform.gameObject.SetActive(false);
        openChestObject.SetActive(true);
    }
}
