using TMPro;
using UnityEngine;

public class Label : MonoBehaviour, IClickable, ITextContainer, IDraggable
{
    [SerializeField] private TextMeshPro _tmpro;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Labels _labels;
    private string _text;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            _tmpro.text = value;
        }
    }

    public void Highlight(bool state)
    {
        if (state)
        {
            _spriteRenderer.material.SetFloat("_HighlightOn", 2F);
        }
        else
        {
            _spriteRenderer.material.SetFloat("_HighlightOn", 0F);
        }
    }

    public void Initialize()
    {
        _labels = GetComponentInParent<Labels>();
    }

    public void OnClick()
    {
    }

    public void OnDoubleClick()
    {
        Highlight(true);
        _labels?.Menu.SetActive(false);
        _labels?.TextWindow.gameObject.SetActive(true);
        _labels?.Input.Initialize(this);
    }
}