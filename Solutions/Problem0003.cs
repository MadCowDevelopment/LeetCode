using System.Collections.Generic;
using Xunit;

namespace Solutions
{
    public class Problem0003
    {
        [Theory]
        [InlineData("abcabcbb", 3)]
        [InlineData("bbbbb", 1)]
        [InlineData("pwwkew", 3)]
        public void Solution(string input, int expected)
        {
            Assert.Equal(expected, LengthOfLongestSubstring(input));
        }

        private static int LengthOfLongestSubstring(string s)
        {
            int longest = 0;

            List<char> currentCharacters = new List<char>();

            foreach (char c in s)
            {
                var indexOfC = currentCharacters.IndexOf(c);
                if (indexOfC >= 0)
                {
                    currentCharacters = currentCharacters.GetRange(indexOfC + 1, currentCharacters.Count - indexOfC - 1);
                }

                currentCharacters.Add(c);
                if (currentCharacters.Count > longest)
                {
                    longest = currentCharacters.Count;
                }
            }

            return longest;
        }
    }
}
