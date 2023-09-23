using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class MenuMode
{
    protected Menu _menu;

    public bool IsEnabled { get; protected set; }

    public string Name { get; }
    public Texture2D Cursor { get; }

    public MenuMode(string name, Texture2D cursor, Menu menu)
    {
        Name = name;
        Cursor = cursor;
        _menu = menu;
    }

    public virtual void Reset()
    {
        OnRelase(Vector2.zero);
    }

    public virtual void OnSelect(Vector2 position)
    {
    }

    public virtual void OnMousePosition(Vector2 position)
    {
    }

    public virtual void OnRelase(Vector2 position)
    {
    }

    public virtual void OnRight()
    {
    }
}