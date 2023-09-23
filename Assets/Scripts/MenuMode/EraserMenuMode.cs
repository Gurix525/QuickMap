using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EraserMenuMode : MenuMode
{
    private bool _isEnabled;

    public EraserMenuMode(Texture2D cursor, Menu menu) : base(cursor, menu)
    {
    }

    public override void OnSelect(Vector2 position)
    {
        _isEnabled = true;
    }

    public override void OnRelase(Vector2 position)
    {
        _isEnabled = false;
    }
}