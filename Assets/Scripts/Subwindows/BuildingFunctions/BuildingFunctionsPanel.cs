﻿using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class BuildingFunctionsPanel : MonoBehaviour
{

    public GameObject NoFunction;

    GameObject ActiveObject;

    public WorkCampPanel WorkCamp;
    public LumberSitePanel LumberSite;
    public QuarryPanel Quarry;
    public CasterTowerPanel CasterTower;
    public BarrierTowerPanel BarrierTower;
    public DefenseEncampmentPanel DefenseEncampment;


    public void Open(ConstructibleBuilding building)
    {
        if (building == null)
            return;
        ActiveObject = NoFunction;
        ClearObjects();
        if (building is WorkCamp)
        {
            if (((WorkCamp)building).postUpgrade.built)
            {
                ActiveObject = WorkCamp.gameObject;
                ActiveObject.SetActive(true);
                WorkCamp.Open(building);
            }
            else
                NoFunction.SetActive(true);
        }
        else if (building is LumberSite)
        {
            ActiveObject = LumberSite.gameObject;
            ActiveObject.SetActive(true);
            LumberSite.Open(building);
        }
        else if (building is Quarry)
        {
            ActiveObject = Quarry.gameObject;
            ActiveObject.SetActive(true);
            Quarry.Open(building);
        }
        else if (building is CasterTower)
        {
            ActiveObject = CasterTower.gameObject;
            ActiveObject.SetActive(true);
            CasterTower.Open(building);
        }
        else if (building is BarrierTower)
        {
            ActiveObject = BarrierTower.gameObject;
            ActiveObject.SetActive(true);
            BarrierTower.Open(building);
        }
        else if (building is DefenseEncampment)
        {
            ActiveObject = DefenseEncampment.gameObject;
            ActiveObject.SetActive(true);
            DefenseEncampment.Open(building);
        }
        else
            NoFunction.SetActive(true);
    }

    private void ClearObjects()
    {
        NoFunction.SetActive(false);
        ActiveObject.SetActive(false);

        WorkCamp.gameObject.SetActive(false);
        LumberSite.gameObject.SetActive(false);
        Quarry.gameObject.SetActive(false);
        CasterTower.gameObject.SetActive(false);
        BarrierTower.gameObject.SetActive(false);
        DefenseEncampment.gameObject.SetActive(false);
    }
    public void Close()
    {
        ClearObjects();
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = false;
    }
}
