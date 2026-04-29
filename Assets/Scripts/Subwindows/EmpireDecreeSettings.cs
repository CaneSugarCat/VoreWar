using LegacyAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmpireDecreeSettings : MonoBehaviour
{
    Dictionary<Decree, int> LocalDecreeValues;

    public Transform DecreeFolder;
    internal List<DecreeSelector> DecreePrefabFolder;
    internal DecreeSelector DecreePrefabInstance;
    public DecreeSelector DecreePrefab;

    public TextMeshProUGUI MaximumPoints;
    public TextMeshProUGUI UsedPoints;


    public void Open(EmpireModifiers modifiers)
    {
        // Nuke prefabs for rebuild.
        int children = DecreeFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(DecreeFolder.GetChild(i).gameObject);
        }

        // Duplicate to local and Construct Prefabs
        LocalDecreeValues = new Dictionary<Decree, int>();
        foreach (var decree in modifiers.DecreeValues)
        {
            LocalDecreeValues.Add(decree.Key, decree.Value);
            DecreePrefabInstance = Instantiate(DecreePrefab, DecreeFolder);
            DecreePrefabInstance.Init(decree.Key, decree.Value);
            DecreePrefabFolder.Add(DecreePrefabInstance);
        }
    }
}
