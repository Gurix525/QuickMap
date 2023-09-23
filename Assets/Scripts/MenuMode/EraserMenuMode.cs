using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EraserMenuMode : MenuMode
{
    public EraserMenuMode(string name, Texture2D cursor, Menu menu) : base(name, cursor, menu)
    {
    }

    public override void Reset()
    {
        OnRelase(Vector2.zero);
    }

    public override void OnSelect(Vector2 position)
    {
        IsEnabled = true;
        _menu.Eraser.gameObject.SetActive(true);
    }

    public override void OnMousePosition(Vector2 position)
    {
        _menu.Eraser.position = position;
    }

    public override void OnRelase(Vector2 position)
    {
        IsEnabled = false;
        _menu.Eraser.gameObject.SetActive(false);
    }
}