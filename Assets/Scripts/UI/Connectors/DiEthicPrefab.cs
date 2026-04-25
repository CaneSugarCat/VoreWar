using OdinSerializer;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiEthicPrefab : MonoBehaviour
{
    public TextMeshProUGUI EthicLeft;
    public TextMeshProUGUI EthicRight;
    public TextMeshProUGUI EthicDescription;
    DichotomyEmpireEthicTypes RepresentedEthic;
    public Button[] ValueSetters;
    int Index;
    int EthicValue; // values -3 to 3; 0 is disabled

    public void Init(DichotomyEmpireEthicTypes type, int index)
    {
        RepresentedEthic = type;
        Index = index;
        EthicValue = 0;
        string[] names = type.ToString().Split('_');
        EthicLeft.text = names[0];
        EthicRight.text = names[1];
    }

    public void SetValue(int value)
    {
        EthicValue = value;
        SetDescription();
    }

    internal void SetButtons()
    {
        foreach (var button in ValueSetters)
        {
            button.interactable = true;
        }
        ValueSetters[EthicValue + 3].interactable = false;
    }

    internal void SetDescription()
    {
        if (EthicValue == 0)
        {
            EthicDescription.text = "";
            return;
        }
        int modifier = Math.Abs(EthicValue);
        string desc = "";
        if (EthicValue < 0) // Left Ethic
        {
            switch (RepresentedEthic)
            {
                case DichotomyEmpireEthicTypes.Militarist_Pacifist:
                    desc = $"Recruiting refunds {modifier} gold.\n " +
                        $"Training grants {ShowValue(5,10,25)}% more exp and is {modifier*5}% cheaper.\n" +
                        $"Increase empire starting exp by {ShowValue(5,10,20)} and team exp by {(modifier-1)*5}.\n" +
                        $"Upkeep reduced by {modifier}% for every 100 gold currently spent on upkeep.\r\n";
                    break;
                case DichotomyEmpireEthicTypes.Grounded_Spiritual:
                    desc = $"Allocate how the population views the power of the empire.\n" +
                        $"Increase effect of bonuses by {ShowValue(0,50,150)}%.\n" +
                        $"Allow reallocation of power every {ShowValue(10,7,3)} strategic turns";
                    break;
                case DichotomyEmpireEthicTypes.Authoritarian_Egalitarian:
                    desc = $"Leader's leadership stat is applied to all units in empire at {ShowValue(25, 50, 100)}% effectiveness, including leader army.";
                    break;
                case DichotomyEmpireEthicTypes.Urban_Agragarian:
                    desc = $"All numerical values of village buildings (including gold price) increased by {ShowValue(10, 25, 50)}%.\n" +
                        $"Max population from farms is reduced by {ShowValue(25, 50, 75)}%\n" +
                        $"Every constructed building provides {ShowValue(5, 15, 30)}% of farm population."; break;
                case DichotomyEmpireEthicTypes.Predator_Prey:
                    desc = $"Prey is treated as if it has {ShowValue(5, 10, 20)}% less HP when calcualting swallow chance.\n" +
                        $"A successful prey escape is rerolled {ShowValue(1, 2, 5)} times per battle\n" +
                        $"Escaping prey take {ShowValue(20, 30, 50)}% digestion damage before escaping"; break;
                case DichotomyEmpireEthicTypes.Might_Magic:
                    desc = $"Increase weapon accuracy by a flat {ShowValue(1, 2, 4)}% (halved for ranged)\n" +
                        $"Weapon base damage increased by {ShowValue(1, 2, 4)} (halved for ranged)\n" +
                        $"Incoming weapon damage reduced by {ShowValue(5, 10, 20)}%";
                    break;
                default:
                    break;
            }
        }
        else // Right Ethic
        {
            switch (RepresentedEthic)
            {
                case DichotomyEmpireEthicTypes.Militarist_Pacifist:
                    desc = $"Start at peace with all empires\n" +
                        $"Unable to declare war on other empires.\n" +
                        $"War can not be declared on this empire unless relation is {modifier * -0.5f}.\n" +
                        $"Max armies reduced by {modifier * 25}% (Minimum three)\n" +
                        $"Max army size reduced by {modifier * 10}%\n" +
                        $"{ShowValue(25, 30, 50)}% Increased village happiness and max population";
                    break;
                case DichotomyEmpireEthicTypes.Grounded_Spiritual:
                    desc = $"Gain 1/3/6 points for allocation to a Diety the population will believe in.\n" +
                        $"Effects of this ethic are based on selected Diety";
                    break;
                case DichotomyEmpireEthicTypes.Authoritarian_Egalitarian:
                    desc = $"Units EXP gains are increased by {ShowValue(10, 15, 25)}% for every level it is beneath the empire's leader.";
                    break;
                case DichotomyEmpireEthicTypes.Urban_Agragarian:
                    desc = $"Max population from farms is increased by {ShowValue(25, 50, 100)}%\n" +
                        $"Population growth is increased by {ShowValue(10, 20, 30)}%\n" +
                        $"Income from village buildings reduced by {ShowValue(15, 30, 60)}%.";
                    break;
                case DichotomyEmpireEthicTypes.Predator_Prey:
                    desc = $"Gain {modifier - 1} EXP per turn eaten and {modifier*2} EXP when escaping.\n" +
                        $"Digestion damage reduced by 100%. Effect reduced by {ShowValue(50, 40, 20)}% per turn eaten, resets after battle.\n" +
                        $"The effect of missing health is reduced by {ShowValue(20, 40, 70)}% when calculating escape chance.\n";
                    break;
                case DichotomyEmpireEthicTypes.Might_Magic:
                    desc = $"Increase max mana by {ShowValue(20, 30, 50)}%\n" +
                        $"Refund {ShowValue(20, 40, 70)}% of spell cost over 3 turns\n" +
                        $"Tier {ShowValue(0, 0, 1)} spells become free";
                    break;
                default:
                    break;
            }
        }

        EthicDescription.text = desc;
    }

    private int ShowValue(int val1, int val2, int val3)
    {
        int modifier = Math.Abs(EthicValue);
        return modifier == 3 ? val3 : modifier == 2 ? val2 : val1;

    }
}

