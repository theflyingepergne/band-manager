using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;

[System.Serializable]
public class GenreWeights
{
    public SerializedDictionary<Genre, float> Values = new();

    //---Constructor---//
    public GenreWeights(params (Genre genre, float weight)[] values)
    {
        foreach (var (genre, weight) in values)
            Values[genre] = weight;
    }

    //---Methods---//
    public float GetWeight(Genre genre)
    {
        return Values.TryGetValue(genre, out float weight) ? weight : 0f;
    }

    public void SetWeight(Genre genre, float amount)
    {
        Values[genre] = amount;
    }

    public void ChangeWeight(Genre genre, float amount)
    {
        float currentWeight = GetWeight(genre);
        currentWeight += amount;
        SetWeight(genre, currentWeight);
    }

    public string[] GetGenresAsStrings()
    {
        return Values.Select(g => g.Key.ToString()).ToArray();
    }

    public Dictionary<Genre, float> GetDominantGenres()
    {
        return Values
            .OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}