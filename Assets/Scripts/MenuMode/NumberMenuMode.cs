using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NumberMenuMode : MenuMode
{
    private GameObject _numberPrefab;

    public NumberMenuMode(string name, Texture2D cursor, Menu menu) : base(name, cursor, menu)
    {
        _numberPrefab = Resources.Load<GameObject>("Prefabs/Number");
    }

    public override void Reset()
    {
        base.Reset();
        _menu.Pointer.gameObject.SetActive(false);
    }

    public override void OnSelect(Vector2 position)
    {
        var number = GameObject.Instantiate(_numberPrefab, _menu.Numbers);
        number.transform.position = position.Round(0.5F);
    }

    public override void OnMousePosition(Vector2 position)
    {
        _menu.Pointer.gameObject.SetActive(true);
        _menu.Pointer.position = position.Round(0.5F);
    }
}