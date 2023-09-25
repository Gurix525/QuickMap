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
    [SerializeField] private GameObject _scanner;

    private ITextContainer _currentContainer;

    public void Initialize(ITextContainer textContainer)
    {
        _inputField.text = textContainer.Text;
        _currentContainer = textContainer;
        //_inputField.Select();
        _inputField.ActivateInputField();
    }

    private void OnEnable()
    {
        _inputField.onValueChanged.AddListener(ChangeContainerText);
    }

    

    private void OnDisable()
    {
        _inputField.onValueChanged.RemoveAllListeners();
        _currentContainer = null;
    }

    public void Submit()
    {
        _currentContainer?.Highlight(false);
        _menu?.SetActive(true);
        _scanner?.SetActive(true);
        _textWindow?.SetActive(false);
    }

    private void ChangeContainerText(string arg0)
    {
        if (_currentContainer == null)
            return;
        _currentContainer.Text = _inputField.text;
    }
}