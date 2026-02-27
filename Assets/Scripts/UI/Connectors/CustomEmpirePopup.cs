using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomEmpirePopup : MonoBehaviour
{
    public TMP_Dropdown RaceDropdown;
    public Button AddButton;
    public Button CancelButton;

    public CreateStrategicGame CreateStrategicGame;

    public void Open()
    {
        RaceDropdown.ClearOptions();
        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0).OrderBy((s) => s.ToString()))
        {
            RaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
        }
        RaceDropdown.RefreshShownValue();
    }

    public void Add()
    {
        if (Enum.TryParse(RaceDropdown.options[RaceDropdown.value].text, out Race race))
        {
            State.AdditionalEmpires.Add(200 + State.AdditionalEmpires.Count, race);
            CreateStrategicGame.BuildRaceDisplay();
            Cancel();
        }
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}

