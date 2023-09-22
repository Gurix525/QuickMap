using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float _hoverSpeed = 1F;

    private Vector3 _originalPosition;
    private float _originalOrtographicSize;
    private bool _isMiddlePressed;

    private void Awake()
    {
        _originalPosition = transform.position;
        _originalOrtographicSize = Camera.main.orthographicSize;
    }

    private void OnMiddle(InputValue value)
    {
        _isMiddlePressed = value.Get<float>() == 1 ? true : false;
        Cursor.visible = !_isMiddlePressed;
    }

    private void OnMouseDelta(InputValue value)
    {
        if (_isMiddlePressed)
        {
            transform.position -= _hoverSpeed * (Vector3)value.Get<Vector2>() * Camera.main.orthographicSize / _originalOrtographicSize;
        }
    }

    private void OnHome()
    {
        transform.position = _originalPosition;
        Camera.main.orthographicSize = _originalOrtographicSize;
    }

    private void OnZoom(InputValue value)
    {
        float input = value.Get<float>();
        if (input != 0F)
            Camera.main.orthographicSize -= Mathf.Sign(input) * 2F;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3F, 27F);
    }
}