using System.Collections.Concurrent;
using SQLite;
using tellahs_library.Helpers;

namespace tellahs_library;

public class ActiveRaces
{
    private readonly ConcurrentDictionary<string, Entities.ActiveRace> RaceList = new();

    private readonly ISQLiteAsyncConnection db;

    public ActiveRaces(ISqliteHelper sqliteHelper)
    {
        db = sqliteHelper.GetAsyncSqlConnection();
        var savedRaces = Task.Run(async () => await LoadSavedRacesAsync());
    }

    private async Task LoadSavedRacesAsync()
    {
        var races = await db.Table<Entities.ActiveRace>().ToListAsync();
        foreach (var race in races)
        {
            RaceList.TryAdd(race.Url, race);
        }
    }

    /// <summary>
    /// A public facing version of the internal concurrent dictionary. Do not keep as a reference
    /// </summary>
    public List<Entities.ActiveRace> Races => [.. RaceList.Values];

    public async Task AddOrUpdateRace(string raceUrl, Entities.ActiveRace race)
    {
        if (!RaceList.ContainsKey(raceUrl))
        {
            RaceList.AddOrUpdate(raceUrl, race, (key, oldValue) => race);
            await db.InsertAsync(race);
        }
    }

    public async Task<bool> RemoveRaceAsync(string raceUrl)
    {
        var response = RaceList.Remove(raceUrl, out var race);

        var deleteResponse = await db.Table<Entities.ActiveRace>().DeleteAsync(x => x.Url == raceUrl);
        return response;
    }

    /// <summary>
    /// A method to get the race details given the dictionary's key
    /// </summary>
    /// <param name="key">the room name/url for the given race</param>
    /// <returns>the race if it exists, null otherwise</returns>
    public Entities.ActiveRace? GetRace(string key) => RaceList.GetValueOrDefault(key);

}
