﻿using OdinSerializer;
using System;
using System.Collections.Generic;

class CasterTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade forceUpgrade;
    public BuildingUpgrade buffUpgrade;

    [OdinSerialize]
    internal int ManaCharges;
    [OdinSerialize]
    internal int SetMagnitude;
    [OdinSerialize]
    internal Dictionary<SpellTypes,int> spellCasts;
    
    public CasterTower(Vec2i location) : base(location)
    {
        Name = "Caster Tower";
        Desc = "The caster tower uses mana to launch a barrage of spells on the first turn of combat that starts in it's range.";
        spriteID = 24;
        buildingType = ConstructibleType.CasterTower;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.CasterTowerImproveUpgrade);
        forceUpgrade = AddUpgrade(forceUpgrade, Config.BuildCon.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.CasterTowerBuffUpgrade);

        ManaCharges = Config.BuildCon.CasterTowerManaChargesMax;

        spellCasts = new Dictionary<SpellTypes, int> 
        {
            [SpellTypes.Fireball] = 1,
            [SpellTypes.LightningBolt] = 1,
            [SpellTypes.PowerBolt] = 3,

            [SpellTypes.Predation] = 1,
            [SpellTypes.Valor] = 1,
            [SpellTypes.Speed] = 1,
            [SpellTypes.Shield] = 1,

            [SpellTypes.Pyre] = 1,
            [SpellTypes.IceBlast] = 2,
            [SpellTypes.ForkLightning] = 1,
            [SpellTypes.Flamberge] = 1,
        };
    }
    internal override void RunBuildingFunction()
    {
        int ManaChargesMax = Config.BuildCon.CasterTowerManaChargesMax * (improveUpgrade.built ? 2 : 1);
        int ManaChargesRegen = Config.BuildCon.CasterTowerManaChargesRegen * (improveUpgrade.built ? 2 : 1);
        if (ManaChargesMax > ManaCharges)
        {
            ManaCharges += ManaChargesRegen;
            if (ManaCharges > ManaChargesMax)
            {
                ManaCharges = ManaChargesMax;
            }
        }
        
    }
}

