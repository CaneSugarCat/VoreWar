﻿using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackMagicTowerPanel : MonoBehaviour
{
    public GameObject AfflictionHolder;

    public Button SacButton;
    public Button ResButton;

    public Button NecrosisButton;
    public Button ErrosionButton;
    public Button AgonyButton;
    public Button LethargyButton;
    public TextMeshProUGUI AfflictionDescription;

    public TextMeshProUGUI SoulPower;

    BlackMagicTower BlackMagicTower;
    public void Open(ConstructibleBuilding building)
    {
        BlackMagicTower = (BlackMagicTower)building;
        SacButton.interactable = false;
        ResButton.interactable = false;

        AfflictionHolder.SetActive(BlackMagicTower.afflictUpgrade.built);
        NecrosisButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            NecrosisButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Necrosis;
        });
        ErrosionButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            ErrosionButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Errosion;
        });
        AgonyButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            AgonyButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Agony;
        });
        LethargyButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            LethargyButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Lethargy;
        });
        UpdateButtonInteract();
        switch (BlackMagicTower.Affliction)
        {
            case StatusEffectType.Necrosis:
                NecrosisButton.interactable = false;
                AfflictionDescription.text = "Necrosis reduces healing of enemy units. Can cause healing to deal damage if the effect is strong enough.";
                break;
            case StatusEffectType.Errosion:
                ErrosionButton.interactable = false;
                AfflictionDescription.text = "Errosion greatly increases the damage enemy units take during it's duration.";
                break;
            case StatusEffectType.Agony:
                AgonyButton.interactable = false;
                AfflictionDescription.text = "Agony causes enemy units to take damage over time. It has a longer duration compared to other afflictions.";
                break;
            case StatusEffectType.Lethargy:
                LethargyButton.interactable = false;
                AfflictionDescription.text = "Lethargy reduces the Strength, Dexterity, and Agility of enemy units, the strength wears off as the effect expires.";
                break;
            default:
                break;
        }
    }

    public void UpdateButtonInteract()
    {
        NecrosisButton.interactable = true;
        ErrosionButton.interactable = true;
        AgonyButton.interactable = true;
        LethargyButton.interactable = true;
    }
}