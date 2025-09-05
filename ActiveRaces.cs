using System;
using System.Collections.Concurrent;
using tellahs_library.Services.RacetimeModels;

namespace tellahs_library;

public class ActiveRaces
{
    private ConcurrentDictionary<string, Race> RaceList = new();

    public Dictionary<string, Race> GetRaces => RaceList.ToDictionary();

    public void AddOrUpdateRace(string something, Race race) => RaceList.AddOrUpdate(something, race, (key, oldValue) => race);

}
