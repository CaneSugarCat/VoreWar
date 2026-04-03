using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestrictedList : MonoBehaviour
{
    public Text Label;
    public string defaultText;
    public Button Add;
    public Button Remove;
    public Button reset;
    public bool inverted;

    internal void Init(string text, Action<bool> action)
    {
        Label.text = text;
        defaultText = text;
        Add.onClick.AddListener(() => action(true));
        Remove.onClick.AddListener(() => action(false));
    }
}
