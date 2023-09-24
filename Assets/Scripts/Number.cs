using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Number : MonoBehaviour, IClickable
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private SpriteRenderer _spriterenderer;

    private NumbersSource _numbersSource;


    public int ID { get; private set; }

    public string Text { get; set; }


    public void Highlight(bool state)
    {
        if (state)
        {
            _spriterenderer.material.SetFloat("_HighlightOn", 2F);
        }
        else
        {
            _spriterenderer.material.SetFloat("_HighlightOn", 0F);
        }
    }

    public void ChangeID(int id)
    {
        ID = id;
        _text.text = ID.ToString();
    }

    public void Initialize()
    {
        ChangeID((_numbersSource = GetComponentInParent<NumbersSource>()).GetNumber(this));
    }

    public void OnClick()
    {

    }

    public void OnDoubleClick()
    {
        Highlight(true);
        _numbersSource?.Menu.SetActive(false);
        _numbersSource?.TextWindow.gameObject.SetActive(true);
        _numbersSource?.Input.Initialize(this);
    }

    private void OnDestroy()
    {
        GetComponentInParent<NumbersSource>()?.RemoveNumber(this);
    }

    public override string ToString()
    {
        return ID.ToString();
    }
}