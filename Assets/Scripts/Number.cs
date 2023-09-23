using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;

    public int ID { get; private set; }

    public void ChangeID(int id)
    {
        ID = id;
        _text.text = ID.ToString();
    }

    public void Initialize()
    {
        ChangeID(GetComponentInParent<NumbersSource>().GetNumber(this));
    }

    private void OnDestroy()
    {
        GetComponentInParent<NumbersSource>().RemoveNumber(this);
    }
}