using System;
using System.Collections.Generic;
using UnityEngine;

public static class SubRaceParameters
{
    public static void CreateSubRaceParameters(Race rootrace, string subracename)
    {
        SubRaceTraits newSubRace = new SubRaceTraits();

        newSubRace.SubRaceName = subracename;

        RaceTraits RootParameters = RaceParameters.GetTraitData(rootrace);

        newSubRace.parameters = new RaceTraits()
        {
            BodySize = RootParameters.BodySize,
            StomachSize = RootParameters.StomachSize,
            HasTail = RootParameters.HasTail,
            FavoredStat = RootParameters.FavoredStat,
            AllowedVoreTypes = RootParameters.AllowedVoreTypes,
            RaceAI = RootParameters.RaceAI,
            RacialTraits = new List<Traits>(),
            RaceDescription = "",
        };
        foreach (Traits trait in RootParameters.RacialTraits)
        {
            newSubRace.parameters.RacialTraits.Add(trait);
        }


        if (State.SubRaces.ContainsKey(rootrace))
        {
            newSubRace.RaceID = (Race)(((int)rootrace * 1000) + 1000 + State.SubRaces[rootrace].Count);
            State.SubRaces[rootrace].Add(newSubRace);
        }
        else
        {
            newSubRace.RaceID = (Race)((int)rootrace * 1000) + 1000;
            List<SubRaceTraits> newsubracelist = new List<SubRaceTraits>(){ newSubRace };
            State.SubRaces.Add(rootrace, newsubracelist);
        }
        
    }

    public static Race DecodeSubRace(Race subrace)
    {
        return (Race)Math.Floor((float)((int)subrace - 1000) / 1000);
    }

}

public class SubRaceTraits
{
    internal Race RaceID;
    internal string SubRaceName;
    internal RaceTraits parameters;
}
