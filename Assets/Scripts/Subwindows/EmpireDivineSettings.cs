using LegacyAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmpireDivineSettings : MonoBehaviour
{
    public Button ExitButton;
    public Button HelpButton;
    public Button RenounceButton;
    public Button LevelUpButton;

    public TextMeshProUGUI DietyName;
    public TextMeshProUGUI RemainingPoints;
    public TextMeshProUGUI PointProgressCounter;
    public TextMeshProUGUI EffectText;
    public TextMeshProUGUI PreferencesText;
    public TextMeshProUGUI CurrentLevel;
    public TextMeshProUGUI CurrentAttitude;

    public Slider PointProgressSlider;

    public Image DietyImage;

    public GameObject HelpMenu;

    public Transform DeityFolder;
    internal List<DeitySelector> DeityPrefabFolder;
    public DeitySelector DeityPrefab;

    Dictionary<Deity, int> LocalDeityValues;

    public void Open(EmpireModifiers modifiers)
    {
        DeityPrefabFolder = new List<DeitySelector>();
        int children = DeityFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(DeityFolder.GetChild(i).gameObject);
        }
    }

    public void OpenHelp()
    {
        HelpMenu.gameObject.SetActive(true);
    }
    public void SetHelpText(int index)
    {
        switch (index)
        {
            default:
                break;
        }
    }

    public void ExitHelp()
    {
        HelpMenu.gameObject.SetActive(false);
    }
}
