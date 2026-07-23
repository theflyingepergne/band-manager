using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

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

    public List<string> FormatGenreAffinities(bool formatVerbose = true)
    {
        List<string> formattedGenres = new();

        foreach (var keyValuePair in Values)
        {
            string formattedGenre;

            // Format text
            if (keyValuePair.Value > 5)
            {
                string prefix = formatVerbose ? "Loves " : "<color=green>+++";
                formattedGenre = $"{prefix}{keyValuePair.Key}";
            }
            else if (5 >= keyValuePair.Value && keyValuePair.Value > 0)
            {
                string prefix = formatVerbose ? "Likes " : "<color=green>+";
                formattedGenre = $"{prefix}{keyValuePair.Key}";
            }
            else if (0 >= keyValuePair.Value && keyValuePair.Value > -5)
            {
                string prefix = formatVerbose ? "Doesn't like " : "<color=red>-";
                formattedGenre = $"{prefix}{keyValuePair.Key}";
            }
            else
            {
                string prefix = formatVerbose ? "Hates " : "<color=red>---";
                formattedGenre = $"{prefix}{keyValuePair.Key}";
            }

            formattedGenres.Add(formattedGenre);
        }

        return formattedGenres;
    }

    public Dictionary<Genre, float> GetDominantGenres()
    {
        return Values
            .OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public KeyValuePair<Genre, float> GetRandomGenre()
    {
        return Values
            .ElementAt(Random.Range(0, Values.Count - 1));
    }
}