using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Erasable : MonoBehaviour
{
    private Menu _menu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Eraser")
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Eraser")
            Destroy(gameObject);
    }

    private void Awake()
    {
        _menu = FindObjectOfType<Menu>();
    }
}