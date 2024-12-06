﻿using OdinSerializer;
using System.Linq;

public class BuildingUpgrade
{

    [OdinSerialize]
    internal string Name;
    [OdinSerialize]
    internal string Desc;

    [OdinSerialize]
    internal int GoldCost;
    [OdinSerialize]
    internal ConstructionResources ResourceToUpgrade;

    [OdinSerialize]
    public int upgradeTime;

    [OdinSerialize]
    public bool built;

    public BuildingUpgrade()
    {
        GoldCost = 0;
        upgradeTime = 1;
        ResourceToUpgrade = new ConstructionResources();
        built = false;
    }


}

