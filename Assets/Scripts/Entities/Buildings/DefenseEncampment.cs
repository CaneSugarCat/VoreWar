﻿using OdinSerializer;
using System;

class DefenseEncampment : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade unitUpgrade;
    public BuildingUpgrade levelUpgrade;

    [OdinSerialize]
    internal int AvailibleDefenders;
    [OdinSerialize]
    internal int TrainTimer;

    internal int maxDefenders => (int)(Owner.MaxGarrisonSize * Config.BuildCon.DefenseEncampmentMaxGarrisonSizeScale * (unitUpgrade.built ? 1.5f : 1));

    public DefenseEncampment(Vec2i location) : base(location)
    {
        Name = "Defense Encampment";
        Desc = "The defense encampment sends reinforcements when a nearby army is attacked.";
        spriteID = 40;
        buildingType = ConstructibleType.DefEncampment;

        ApplyConfigStats(Config.BuildCon.DefenseEncampment);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.DefenseEncampmentImproveUpgrade);
        unitUpgrade = AddUpgrade(unitUpgrade, Config.BuildCon.DefenseEncampmentUnitsUpgrade);
        levelUpgrade = AddUpgrade(levelUpgrade, Config.BuildCon.DefenseEncampmentLevelUpgrade);

        AvailibleDefenders = 0;
        TrainTimer = 0;
    }
    internal override void RunBuildingFunction()
    {
        if (maxDefenders > AvailibleDefenders)
        {
            TrainTimer--;
            if (TrainTimer < 0)
            {
                AvailibleDefenders++;
                TrainTimer = (int)Math.Ceiling(Config.BuildCon.DefenseEncampmentTrainTime * (levelUpgrade.built ? 0.5 : 1));
            }
        }

        if (AvailibleDefenders > maxDefenders)
        {
            AvailibleDefenders = maxDefenders;
        }
    }
}

