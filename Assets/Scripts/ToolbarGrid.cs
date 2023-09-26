using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToolbarGrid : MonoBehaviour
{
    [SerializeField] private Menu _menu;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Button button = transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => SendModeMessage(button));
        }
    }

    private void SendModeMessage(Button button)
    {
        int mode = button.GetComponent<Counter>().Number;
        _menu.SendMessage(mode switch 
        {
            0 => "OnDefault",
            1 => "OnEraser",
            2 => "OnLine",
            3 => "OnNumber",
            4 => "OnText",
            _ => "OnDefault"
        },
        new InputValue());
    }
}
