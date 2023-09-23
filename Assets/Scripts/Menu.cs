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

    private Dictionary<string, MenuMode> _menuModes;
    private Vector2 _worldCursorPosition;

    [field: SerializeField] public Transform Lines { get; set; }

    public MenuMode MenuMode { get; private set; }

    private void Awake()
    {
        _menuModes = new Dictionary<string, MenuMode>()
        {
            {"Default", new DefaultMenuMode(_defaultCursor, this) },
            {"Holding", new HoldingMenuMode(_holdingCursor, this) },
            {"Line", new LineMenuMode(_lineCursor, this) },
            {"Text", new TextMenuMode(_textCursor, this) },
            {"Eraser", new EraserMenuMode(_eraserCursor, this) }
        };
        MenuMode = _menuModes["Default"];
    }

    private void OnDefault(InputValue value)
    {
        Cursor.SetCursor(_defaultCursor, Vector2.zero, CursorMode.Auto);
        MenuMode = _menuModes["Default"];
        MenuMode.Reset();
    }

    private void OnLine(InputValue value)
    {
        Cursor.SetCursor(_lineCursor, Vector2.zero, CursorMode.Auto);
        MenuMode = _menuModes["Line"];
        MenuMode.Reset();
    }

    private void OnText(InputValue value)
    {
        Cursor.SetCursor(_textCursor, Vector2.zero, CursorMode.Auto);
        MenuMode = _menuModes["Text"];
        MenuMode.Reset();
    }

    private void OnEraser(InputValue value)
    {
        Cursor.SetCursor(_eraserCursor, new(0F, 32F), CursorMode.Auto);
        MenuMode = _menuModes["Eraser"];
        MenuMode.Reset();
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
        var worldPosition = Camera.main.ScreenToWorldPoint(position);
        _worldCursorPosition = new Vector2(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y));
        MenuMode.OnMousePosition(_worldCursorPosition);
    }

    private void OnRight(InputValue value)
    {
        MenuMode.OnRight();
    }
}