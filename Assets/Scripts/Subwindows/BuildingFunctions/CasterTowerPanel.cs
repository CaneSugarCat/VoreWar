﻿using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CasterTowerPanel : MonoBehaviour
{

    public GameObject MagnitudeHolder;
    public GameObject SpellCountHolder;

    public TextMeshProUGUI Magnitude;
    public Slider MagnitudeSlider;

    public TextMeshProUGUI FireballCasts;
    public Slider valueFireballCasts;
    public TextMeshProUGUI PowerBoltCasts;
    public Slider valuePowerBoltCasts;
    public TextMeshProUGUI LightningBoltCasts;
    public Slider valueLightningBoltCasts;

    public TextMeshProUGUI PredationCasts;
    public Slider valuePredationCasts;
    public TextMeshProUGUI ValorCasts;
    public Slider valueValorCasts;
    public TextMeshProUGUI SpeedCasts;
    public Slider valueSpeedCasts;
    public TextMeshProUGUI ShieldCasts;
    public Slider valueShieldCasts;

    public TextMeshProUGUI PyreCasts;
    public Slider valuePyreCasts;
    public TextMeshProUGUI IceBlastCasts;
    public Slider valueIceBlastCasts;
    public TextMeshProUGUI FlambergeCasts;
    public Slider valueFlambergeCasts;
    public TextMeshProUGUI ForkLightingCasts;
    public Slider valueForkLightingCasts;

    public TextMeshProUGUI CurrentMana;
    public TextMeshProUGUI MaxMana;

    CasterTower CasterTower;

    public void Open(ConstructibleBuilding building)
    {
        CasterTower = (CasterTower)building;

        UpdateTextValues();
        UpdateSliderValues();

        CurrentMana.text = CasterTower.ManaCharges.ToString();
        MaxMana.text = (Config.BuildCon.CasterTowerManaChargesMax * (CasterTower.improveUpgrade.built ? 2 : 1)).ToString();

        valueFireballCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.basicCasts[SpellTypes.Fireball] = (int)newVal;
            FireballCasts.text = valueFireballCasts.value.ToString();
        });
        valuePowerBoltCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.basicCasts[SpellTypes.PowerBolt] = (int)newVal;
            PowerBoltCasts.text = valuePowerBoltCasts.value.ToString();
        });
        valueLightningBoltCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.basicCasts[SpellTypes.LightningBolt] = (int)newVal;
            LightningBoltCasts.text = valueLightningBoltCasts.value.ToString();
        });
        valuePredationCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.buffCasts[SpellTypes.Predation] = (int)newVal;
            PredationCasts.text = valuePredationCasts.value.ToString();
        });
        valueValorCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.buffCasts[SpellTypes.Valor] = (int)newVal;
            ValorCasts.text = valueValorCasts.value.ToString();
        });
        valueShieldCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.buffCasts[SpellTypes.Shield] = (int)newVal;
            ShieldCasts.text = valueShieldCasts.value.ToString();
        });
        valuePyreCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.advancedCasts[SpellTypes.Pyre] = (int)newVal;
            PyreCasts.text = valuePyreCasts.value.ToString();
        });
        valueIceBlastCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.advancedCasts[SpellTypes.IceBlast] = (int)newVal;
            IceBlastCasts.text = valueIceBlastCasts.value.ToString();
        });
        valueFlambergeCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.advancedCasts[SpellTypes.Flamberge] = (int)newVal;
            FlambergeCasts.text = valueFlambergeCasts.value.ToString();
        });
        valueForkLightingCasts.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.advancedCasts[SpellTypes.ForkLightning] = (int)newVal;
            ForkLightingCasts.text = valueForkLightingCasts.value.ToString();
        });
        MagnitudeSlider.onValueChanged.AddListenerOnce((float newVal) =>
        {
            CasterTower.SetMagnitude = newVal;
            Magnitude.text = MagnitudeSlider.value.ToString();
        });
        UpdateVisibility();
    }

    void UpdateVisibility()
    {
        if (CasterTower.buffUpgrade.built) 
        { 
            SpellCountHolder.gameObject.SetActive(true);
        }
        else
        {
            SpellCountHolder.gameObject.SetActive(false);
        }

        if (CasterTower.forceUpgrade.built) 
        { 
            MagnitudeHolder.gameObject.SetActive(true);
        }
        else
        {
            MagnitudeHolder.gameObject.SetActive(false);
        }
        valuePredationCasts.interactable = CasterTower.buffUpgrade.built;
        valueValorCasts.interactable = CasterTower.buffUpgrade.built;
        valueSpeedCasts.interactable = CasterTower.buffUpgrade.built;
        valueShieldCasts.interactable = CasterTower.buffUpgrade.built;

        valuePyreCasts.interactable = CasterTower.forceUpgrade.built;
        valueIceBlastCasts.interactable = CasterTower.forceUpgrade.built;
        valueFlambergeCasts.interactable = CasterTower.forceUpgrade.built;
        valueForkLightingCasts.interactable = CasterTower.forceUpgrade.built;
        MagnitudeSlider.interactable = CasterTower.forceUpgrade.built;
    }

    void UpdateTextValues()
    {
        FireballCasts.text = valueFireballCasts.value.ToString();
        PowerBoltCasts.text = valuePowerBoltCasts.value.ToString();
        LightningBoltCasts.text = valueLightningBoltCasts.value.ToString();
        PredationCasts.text = valuePredationCasts.value.ToString();
        ValorCasts.text = valueValorCasts.value.ToString();
        SpeedCasts.text = valueSpeedCasts.value.ToString();
        ShieldCasts.text = valueShieldCasts.value.ToString();
        PyreCasts.text = valuePyreCasts.value.ToString();
        IceBlastCasts.text = valueIceBlastCasts.value.ToString();
        FlambergeCasts.text = valueFlambergeCasts.value.ToString();
        ForkLightingCasts.text = valueForkLightingCasts.value.ToString();
        Magnitude.text = MagnitudeSlider.value.ToString();
    }
    void UpdateSliderValues()
    {
        valueFireballCasts.value = CasterTower.basicCasts[SpellTypes.Fireball];
        valuePowerBoltCasts.value = CasterTower.basicCasts[SpellTypes.PowerBolt];
        valueLightningBoltCasts.value = CasterTower.basicCasts[SpellTypes.LightningBolt];
        valuePredationCasts.value = CasterTower.buffCasts[SpellTypes.Predation];
        valueValorCasts.value = CasterTower.buffCasts[SpellTypes.Valor];
        valueSpeedCasts.value = CasterTower.buffCasts[SpellTypes.Speed];
        valueShieldCasts.value = CasterTower.buffCasts[SpellTypes.Shield];
        valuePyreCasts.value = CasterTower.advancedCasts[SpellTypes.Pyre];
        valueIceBlastCasts.value = CasterTower.advancedCasts[SpellTypes.IceBlast];
        valueFlambergeCasts.value = CasterTower.advancedCasts[SpellTypes.Flamberge];
        valueForkLightingCasts.value = CasterTower.advancedCasts[SpellTypes.ForkLightning];
    }
}