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

    public LineMenuMode(string name, Texture2D cursor, Menu menu) : base(name, cursor, menu)
    {
        _linePrefab = Resources.Load<GameObject>("Prefabs/Line");
    }

    public override void Reset()
    {
        if (_line != null)
            GameObject.Destroy(_line.gameObject);
        _line = null;
    }

    public override void OnSelect(Vector2 position)
    {
        var roundedPosition = position.Round(0.5F);
        _line = GameObject.Instantiate(_linePrefab, _menu.Lines).GetComponent<LineRenderer>();
        _line.SetPositions(new Vector3[] { roundedPosition, roundedPosition });
        _lineStart = roundedPosition;
    }

    public override void OnMousePosition(Vector2 position)
    {
        if (_line == null)
            return;
        var roundedPosition = position.Round(0.5F);
        _line.SetPositions(new Vector3[] { _lineStart, roundedPosition });
    }

    public override void OnRelase(Vector2 position)
    {
        if (_line != null)
            _line.GetComponent<LineCollider>().BakeMesh();
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