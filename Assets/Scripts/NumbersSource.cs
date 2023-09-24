using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersSource : MonoBehaviour
{
    public List<Number> Numbers { get; } = new();
    [field: SerializeField] public TextWindow TextWindow { get; set; }
    [field: SerializeField] public GameObject Menu { get; set; }
    [field: SerializeField] public Input Input { get; set; }

    public int GetNumber(Number number)
    {
        Numbers.Add(number);
        return Numbers.Count;
    }

    public void RemoveNumber(Number number)
    {
        Numbers.Remove(number);
        UpdateNumbers();
    }

    private void UpdateNumbers()
    {
        for (int i = 1; i <= Numbers.Count; i++)
        {
            Numbers[i - 1].ChangeID(i);
        }
    }
}