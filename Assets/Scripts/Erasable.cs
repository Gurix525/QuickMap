using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Erasable : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        Erase(other);
    }

    protected void OnTriggerStay(Collider other)
    {
        Erase(other);
    }

    protected virtual void Erase(Collider other)
    {
        if (other.gameObject.name == "Eraser")
            Destroy(gameObject);
    }
}