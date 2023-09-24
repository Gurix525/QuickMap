using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Input : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _textWindow;

    private Number _currentNumber;

    public void Initialize(Number number)
    {
        _inputField.text = number.Text;
        _currentNumber = number;
        //_inputField.Select();
        _inputField.ActivateInputField();
    }

    public void Submit()
    {
        _menu.SetActive(true);
        _textWindow.SetActive(false);
        _currentNumber.Text = _inputField.text;
        _currentNumber.Highlight(false);
    }
}