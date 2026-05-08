using UnityEngine;

public static class FormatUtils
{
    public static string AbbreviateNumber(float num)
    {
        if (num >= 1_000_000_000)
            return (num / 1_000_000_000f).ToString("0.#") + "B";

        if (num >= 1_000_000)
            return (num / 1_000_000f).ToString("0.#") + "M";

        if (num >= 1_000)
            return (num / 1_000f).ToString("0.#") + "k";

        return num.ToString("0");
    }
}