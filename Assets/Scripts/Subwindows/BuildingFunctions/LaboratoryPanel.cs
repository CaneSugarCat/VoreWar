﻿using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaboratoryPanel : MonoBehaviour
{
    Laboratory Laborotory;

    public void Open(ConstructibleBuilding building)
    {
        Laborotory = (Laboratory)building;

    }
}