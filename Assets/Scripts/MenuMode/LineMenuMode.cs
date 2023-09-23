using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LineMenuMode : MenuMode
{
    private GameObject _linePrefab;
    private LineRenderer _line;
    private Vector3 _lineStart;

    public LineMenuMode(Texture2D cursor, Menu menu) : base(cursor, menu)
    {
        _linePrefab = Resources.Load<GameObject>("Prefabs/Line");
    }

    public override void OnSelect(Vector2 position)
    {
        _line = GameObject.Instantiate(_linePrefab, _menu.Lines).GetComponent<LineRenderer>();
        _line.SetPositions(new Vector3[] { position, position });
        _lineStart = position;
    }

    public override void OnMousePosition(Vector2 position)
    {
        if (_line == null)
            return;
        _line.SetPositions(new Vector3[] { _lineStart, position });
    }

    public override void OnRelase(Vector2 position)
    {
        _line = null;
    }

    public override void OnRight()
    {
        if (_line != null)
            GameObject.Destroy(_line.gameObject);
        _line = null;
        Reset();
    }
}