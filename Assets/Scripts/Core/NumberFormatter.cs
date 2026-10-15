using System;
using System.Globalization;

namespace AncestorsWatching
{
    /// <summary>Formats big doubles as 1.2K, 3.4M and so on, for currency and damage.</summary>
    public static class NumberFormatter
    {
        private static readonly string[] Suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp" };

        // Invariant culture so a comma-decimal system locale can't turn "1.2K" into "1,2K".
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        public static string Format(double value)
        {
            double magnitude = Math.Abs(value);
            if (Math.Round(magnitude, 1) < 1000) return value.ToString("0.#", Culture);

            int tier = (int)(Math.Log10(magnitude) / 3);
            double scaled = value / Math.Pow(1000, tier);

            // 999.96K would print as "1000K"; move up a tier instead.
            if (Math.Abs(Math.Round(scaled, 1)) >= 1000)
            {
                tier++;
                scaled /= 1000;
            }

            // Past the last suffix, scientific notation stays readable.
            if (tier >= Suffixes.Length) return value.ToString("0.##e+0", Culture);

            return scaled.ToString("0.#", Culture) + Suffixes[tier];
        }
    }
}
