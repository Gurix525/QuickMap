using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DefaultMenuMode : MenuMode
{
    private IDraggable _currentDraggable;

    public DefaultMenuMode(string name, Texture2D cursor, Menu menu) : base(name, cursor, menu)
    {
    }

    public override void OnSelect(Vector2 position)
    {
        RaycastHit hit = GetRaycastHit(ref position, false);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out IDraggable draggable))
            {
                //hit.collider.GetComponent<IDraggable>().Drag();
                _currentDraggable = draggable;
            }
        }
    }

    public override void OnRelase(Vector2 position)
    {
        if (_currentDraggable != null)
        {
            //_currentDraggable.Drop();
        }
        _currentDraggable = null;
    }

    public override void OnMousePosition(Vector2 position)
    {
        if (_currentDraggable != null)
        {
            (_currentDraggable as MonoBehaviour).transform.position = position.Round(0.5F);
        }
    }

    public override void OnDoubleClick(Vector2 position)
    {
        RaycastHit hit = GetRaycastHit(ref position, false);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IClickable>()?.OnDoubleClick();
        }
    }

    private static RaycastHit GetRaycastHit(ref Vector2 position, bool areTriggersIncluded)
    {
        bool oldQuariesHitTriggers = Physics.queriesHitTriggers;
        Physics.queriesHitTriggers = areTriggersIncluded;
        Ray ray = new(new(position.x, position.y, -10F), new(0F, 0F, 1F));
        Physics.Raycast(ray, out RaycastHit hit);
        Physics.queriesHitTriggers = oldQuariesHitTriggers;
        return hit;
    }

    
}