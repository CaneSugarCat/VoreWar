using UnityEngine;
using UnityEngine.UI;

public class StartEmpireUI : MonoBehaviour
{
    public Text Name;
    public Toggle AIPlayer;
    public InputField VillageCount;
    public Dropdown StrategicAI;
    public Dropdown TacticalAI;
    public Toggle CanVore;
    public InputField Team;
    public Dropdown PrimaryColor;
    public Dropdown SecondaryColor;
    public InputField TurnOrder;
    public Slider MaxArmySize;
    public Slider MaxGarrisonSize;
    public Button RemoveButton;

    internal int LastColor;
    internal int EmpireID;
    internal Race RepresentedRace = Race.none;

    private void Start()
    {
        if (PrimaryColor != null)
            LastColor = PrimaryColor.value;
        if (RemoveButton != null)
            RemoveButton.onClick.AddListener(() => State.GameManager.Start_Mode.CreateStrategicGame.RemoveRace(this));
    }

    public void UpdateColor()
    {
        State.GameManager.Start_Mode.CreateStrategicGame.UpdateColor(this);
    }

    public void UpdateSecondaryColor()
    {
        State.GameManager.Start_Mode.CreateStrategicGame.UpdateSecondaryColor(this);
    }


}

