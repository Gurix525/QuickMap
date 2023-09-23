using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DefaultMenuMode : MenuMode
{
    public DefaultMenuMode(string name, Texture2D cursor, Menu menu) : base(name, cursor, menu)
    {
    }

    public override void OnSelect(Vector2 position)
    {
        Debug.Log(position);
    }
}