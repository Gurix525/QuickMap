using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWindow : MonoBehaviour
{
    [SerializeField] private Input _input;

    private void OnSubmit()
    {
        _input.Submit();
    }
}