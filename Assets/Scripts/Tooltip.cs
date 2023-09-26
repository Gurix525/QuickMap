using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject _tooltipWindow;
    [SerializeField] private GraphicRaycaster _raycaster;
    [SerializeField] private TextMeshProUGUI _tmpro;

    private void OnMousePosition(InputValue value)
    {
        PointerEventData eventData = new(null);
        eventData.position = value.Get<Vector2>();
        List<RaycastResult> results = new();
        _raycaster.Raycast(eventData, results);
        if (results.Count > 0)
        {
            if (results[0].gameObject.TryGetComponent(out TooltipText tooltipText))
            {
                _tooltipWindow.SetActive(true);
                _tmpro.text = tooltipText.Text;
            }
            else
                _tooltipWindow.SetActive(false);
        }
    }
}