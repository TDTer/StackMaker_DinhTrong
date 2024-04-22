using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;
    // Gọi trước khi được active
    public virtual void Setup()
    {

    }

    // Gọi sau khi được active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }

    public virtual void CloseDirectly()
    {
        if (isDestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
            gameObject.SetActive(false);
    }
}
