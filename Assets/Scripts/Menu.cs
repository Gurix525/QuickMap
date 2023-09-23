using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private Texture2D _holdingCursor;
    [SerializeField] private Texture2D _lineCursor;
    [SerializeField] private Texture2D _textCursor;
    [SerializeField] private Texture2D _eraserCursor;
    [SerializeField] private Texture2D _numberCursor;

    private Dictionary<string, MenuMode> _menuModes;
    private Vector2 _worldCursorPosition;

    [field: SerializeField] public Transform Numbers { get; set; }
    [field: SerializeField] public Transform Lines { get; set; }
    [field: SerializeField] public Transform Eraser { get; set; }
    [field: SerializeField] public Transform Pointer { get; set; }

    public MenuMode MenuMode { get; private set; }

    private void Awake()
    {
        _menuModes = new Dictionary<string, MenuMode>()
        {
            {"Default", new DefaultMenuMode("Default", _defaultCursor, this) },
            {"Holding", new HoldingMenuMode("Holding", _holdingCursor, this) },
            {"Line", new LineMenuMode("Line", _lineCursor, this) },
            {"Text", new TextMenuMode("Text", _textCursor, this) },
            {"Eraser", new EraserMenuMode("Eraser", _eraserCursor, this) },
            {"Number", new NumberMenuMode("Number", _numberCursor, this) },
        };
        MenuMode = _menuModes["Default"];
    }

    private void OnDefault(InputValue value)
    {
        MenuMode.Reset();
        Cursor.SetCursor(_defaultCursor, Vector2.zero, CursorMode.Auto);
        MenuMode = _menuModes["Default"];
    }

    private void OnLine(InputValue value)
    {
        MenuMode.Reset();
        Cursor.SetCursor(_lineCursor, Vector2.zero, CursorMode.Auto);
        MenuMode = _menuModes["Line"];
    }

    private void OnText(InputValue value)
    {
        MenuMode.Reset();
        Cursor.SetCursor(_textCursor, Vector2.zero, CursorMode.Auto);
        MenuMode = _menuModes["Text"];
    }

    private void OnEraser(InputValue value)
    {
        MenuMode.Reset();
        Cursor.SetCursor(_eraserCursor, new(0F, 32F), CursorMode.Auto);
        MenuMode = _menuModes["Eraser"];
    }

    private void OnNumber(InputValue value)
    {
        MenuMode.Reset();
        Cursor.SetCursor(_numberCursor, Vector2.zero, CursorMode.Auto);
        MenuMode = _menuModes["Number"];
    }

    private void OnSelect(InputValue value)
    {
        MenuMode.OnSelect(_worldCursorPosition);
    }

    private void OnRelase(InputValue value)
    {
        MenuMode.OnRelase(_worldCursorPosition);
    }

    private void OnMousePosition(InputValue value)
    {
        var position = value.Get<Vector2>();
        _worldCursorPosition = Camera.main.ScreenToWorldPoint(position);
        MenuMode.OnMousePosition(_worldCursorPosition);
    }

    private void OnRight(InputValue value)
    {
        MenuMode.OnRight();
    }
}