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
        RaycastHit hit = GetRaycastHit(ref position);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IClickable>()?.OnClick();
        }
    }

    public override void OnDoubleClick(Vector2 position)
    {
        RaycastHit hit = GetRaycastHit(ref position);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IClickable>()?.OnDoubleClick();
        }
    }

    private static RaycastHit GetRaycastHit(ref Vector2 position)
    {
        Ray ray = new(new(position.x, position.y, -10F), new(0F, 0F, 1F));
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }

    
}