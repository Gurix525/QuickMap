using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TextMenuMode : MenuMode
{

    private GameObject _labelPrefab;

    public TextMenuMode(string name, Texture2D cursor, Menu menu) : base(name, cursor, menu)
    {
        _labelPrefab = Resources.Load<GameObject>("Prefabs/Label");
    }

    public override void Reset()
    {
        base.Reset();
        _menu.Pointer.gameObject.SetActive(false);
    }

    public override void OnSelect(Vector2 position)
    {
        GameObject label = GameObject.Instantiate(_labelPrefab, _menu.Labels);
        label.transform.position = position.Round(0.5F);
        label.GetComponent<Label>().Initialize();
    }

    public override void OnMousePosition(Vector2 position)
    {
        _menu.Pointer.gameObject.SetActive(true);
        _menu.Pointer.position = position.Round(0.5F);
    }
}