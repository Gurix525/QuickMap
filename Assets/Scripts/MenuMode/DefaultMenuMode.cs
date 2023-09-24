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
        Ray ray = new(new(position.x, position.y, -10F), new(0F, 0F, 1F));
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null) 
        {
            Debug.Log(hit.collider);
        }
    }
}