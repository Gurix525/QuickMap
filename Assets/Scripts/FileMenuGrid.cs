using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FileMenuGrid : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Scanner _scanner;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Button button = transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => SendCommandMessage(button));
        }
    }

    private void SendCommandMessage(Button button)
    {
        int mode = button.GetComponent<Counter>().Number;
        if (mode == 0)
            _menu.SendMessage("OnSave", new InputValue());
        if (mode == 1)
            _menu.SendMessage("OnLoad", new InputValue());
        if (mode == 2)
            _scanner.SendMessage("OnScan", new InputValue());
    }
}