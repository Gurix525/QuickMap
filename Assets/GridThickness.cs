using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridThickness : MonoBehaviour
{
    private Renderer _renderer;
    private float _originalThickness;
    private float _originalOrtographicSize;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalThickness = _renderer.material.GetFloat("_Thickness");
        _originalOrtographicSize = Camera.main.orthographicSize;
    }

    private void Update()
    {
        _renderer.material.SetFloat("_Thickness", _originalThickness * Camera.main.orthographicSize / _originalOrtographicSize);
    }
}