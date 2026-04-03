using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubRaceSlider : MonoBehaviour
{
    public Slider slider;
    public Text subraceName;
    internal SubRaceTraits subRace;
        
    public void SetName(string name)
    {
        subraceName.text = name;
    }

    public void AdjustValue()
    {
        if (subRace != null)
        {
            subRace.SelectionChance = slider.value;
        }
    }
}
