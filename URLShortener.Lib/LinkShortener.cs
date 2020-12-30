using System;
using System.Linq;

namespace URLShortener.Lib
{
    /// <summary>
    /// URL Shortener
    /// </summary>
    public static class LinkShortener
    {
        // Seed is always the same so that we have the same order
        private static readonly Random Randomizer = new Random(10);

        // Shuffled alphabet
        private static readonly char[] Alphabet =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                .ToCharArray()
                .OrderBy(x => Randomizer.Next()).ToArray();
        
        private static readonly int Base = Alphabet.Length;

        /// <summary>
        /// Converts base-10 integer to base 62 and then uses the number to encode a string
        /// </summary>
        /// <param name="i"></param>
        /// <returns>Encoded string</returns>
        public static string Encode(int i)
        {
            if (i == 0) return Alphabet[0].ToString();

            var s = string.Empty;

            while (i > 0)
            {  
                s += Alphabet[i % Base];
                i /= Base;
            }

            return string.Join(string.Empty, s.Reverse());
        }

        /// <summary>
        /// Decodes string and evaluates base-10 integer from base-62
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Base-10 integer used to encode the string</returns>
        public static int Decode(string s)
        {
            var i = 0;

            foreach (var c in s)
            {
                i = (i * Base) + Alphabet.IndexOf(c);
            }

            return i;
        }

        /// <summary>
        /// Find the location of a character within the array
        /// </summary>
        /// <param name="arr">Source array</param>
        /// <param name="c">Character to look for</param>
        /// <returns>Index of the first instance of the character
        /// or -1 if the character is not in the array</returns>
        public static int IndexOf(this char[] arr, char charToLook)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                if (arr[i] == charToLook)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}