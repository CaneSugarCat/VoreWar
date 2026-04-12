using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmpireModifierPanel : MonoBehaviour
{
    public Button[] ButtonArray;
    public GameObject[] PanelArray;

    //General
    public InputField EmpireName;
    public InputField EmpireTraits;
    public InputField EmpireIncomeShift;
    public InputField EmpireIncomeMult;
    public InputField EmpireIncomingRelation;
    public InputField EmpireOutgoingRelation;
    public InputField EmpireIncomingRelationMult;
    public InputField EmpireOutgoingRelationMult;
    public InputField EmpireArmyMP;
    public InputField EmpirePopGrowthMult;
    public TMP_Dropdown EmpireType;
    public TMP_Dropdown EmpireInnerPersona;
    public TMP_Dropdown EmpireOuterPersona;


    bool ingame;
    object loadedemp;
    public void Open(object targetEmp)
    {
        if (!(targetEmp.GetType() == typeof(StartEmpireUI)) || !(targetEmp.GetType() == typeof(Empire)))
        {
            return;
        }

        gameObject.SetActive(true);
        loadedemp = targetEmp;
        ingame = false;
        ButtonArray[0].interactable = false;
        PanelArray[0].SetActive(true);
        LoadGeneral();
    }

    internal void LoadGeneral()
    {
        if (loadedemp == null)
        {
            return;
        }

        //Load params
        EmpireModifiers modifires;
        if (ingame)
        {
            modifires = ((Empire)loadedemp).Modifiers;
            EmpireName.text = ((Empire)loadedemp).Name.ToString();
            EmpireTraits.text = RaceEditorPanel.TraitListToText(((Empire)loadedemp).EmpTraits).ToString();
        }
        else
        {
            modifires = ((StartEmpireUI)loadedemp).Modifiers;
            EmpireName.text = ((StartEmpireUI)loadedemp).Name.text.ToString();
            EmpireTraits.text = RaceEditorPanel.TraitListToText(((StartEmpireUI)loadedemp).EmpTraits).ToString();
        }

        EmpireIncomeShift.text = modifires.IncomeShift.ToString();
        EmpireIncomeMult.text = modifires.IncomeMult.ToString();
        EmpireIncomingRelation.text = modifires.IncomingOpinionShift.ToString();
        EmpireOutgoingRelation.text = modifires.OutgoingOpinionShift.ToString();
        EmpireIncomingRelationMult.text = modifires.IncomingOpinionMult.ToString();
        EmpireOutgoingRelationMult.text = modifires.OutgoingOpinionMult.ToString();
        EmpireArmyMP.text = modifires.ArmyMPShift.ToString();
        EmpirePopGrowthMult.text = modifires.PopGrowthMult.ToString();

        // Populate dropdowns
        EmpireType.ClearOptions();
        foreach (EmpireType type in ((EmpireType[])Enum.GetValues(typeof(EmpireType))).Where(s => (int)s >= 0))
        {
            EmpireType.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
        }
        EmpireInnerPersona.ClearOptions();
        foreach (EmpireInnerPersona type in ((EmpireInnerPersona[])Enum.GetValues(typeof(EmpireInnerPersona))).Where(s => (int)s >= 0))
        {
            EmpireInnerPersona.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
        }
        EmpireOuterPersona.ClearOptions();
        foreach (EmpireOuterPersona type in ((EmpireOuterPersona[])Enum.GetValues(typeof(EmpireOuterPersona))).Where(s => (int)s >= 0))
        {
            EmpireOuterPersona.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
        }
        EmpireType.value = (int)modifires.EmpireType;
        EmpireInnerPersona.value = (int)modifires.InnerPersona;
        EmpireOuterPersona.value = (int)modifires.OuterPersona;
    }


    public void OpenTab(int index)
    {
        foreach (Button item in ButtonArray)
        {
            item.interactable = true;
        }
        foreach (GameObject item in PanelArray)
        {
            item.SetActive(false);
        }
        ButtonArray[index].interactable = false;
        PanelArray[index].SetActive(true);
        switch (index)
        {
            case 0:
                LoadGeneral();
                break;
            default:
                break;
        }
    }

    public void SaveAndExit()
    {
    }

    public void DiscardAndExit()
    {
    }

    public void ResetAll()
    {
    }

    public void RevertAll()
    {
    }
}