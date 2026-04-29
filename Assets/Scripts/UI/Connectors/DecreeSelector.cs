using OdinSerializer;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecreeSelector : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Effect;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Cost;
    public Button LevelUp;
    public Button LevelDown;
    internal Decree RepresentedDecree;
    internal int CurrentStacks;

    internal void Init(Decree decree, int stacks)
    {
        RepresentedDecree = decree;
        CurrentStacks = stacks;

        Name.text = decree.ToString();
        Level.text = stacks.ToString();
        Cost.text = CalculateCost().ToString();
    }
    
    internal int CalculateCost()
    {
        return 0;
    }
    
    internal string SetEffectText()
    {
        string desc = "";
        return desc;
    }

}

