using System;
using System.Collections.Concurrent;
using tellahs_library.Services.RacetimeModels;

namespace tellahs_library;

public class ActiveRaces
{
    private readonly ConcurrentDictionary<string, Race> RaceList = new();

    /// <summary>
    /// A public facing version of the internal concurrent dictionary. Do not keep as a reference
    /// </summary>
    public Dictionary<string, Race> GetRaces => RaceList.ToDictionary();

    public void AddOrUpdateRace(string something, Race race) => RaceList.AddOrUpdate(something, race, (key, oldValue) => race);

    /// <summary>
    /// A method to get the race details given the dictionary's key
    /// </summary>
    /// <param name="key">the room name/url for the given race</param>
    /// <returns>the race if it exists, null otherwise</returns>
    public Race? GetRace(string key) => RaceList.GetValueOrDefault(key);

}
