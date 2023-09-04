using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeSolutions
{
    public class Solutions
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            int[] result = new int[2];

            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int y = i + 1; y < nums.Length; y++)
                {
                    if (nums[i] + nums[y] == target)
                    {
                        result[0] = i;
                        result[1] = y;
                    }
                }
            }

            return result;
        }

        public static int LengthOfLongestSubstring(string s)
        {
            int maxLenght = 0;
            int currLength = 0;
            int startingIndex = 0;

            List<char> chars = new List<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (!chars.Contains(s[i]))
                {
                    chars.Add(s[i]);
                    currLength++;
                }
                else
                {
                    chars.Clear();
                    currLength = 0;

                    i = startingIndex++;
                }

                if (maxLenght < currLength)
                {
                    maxLenght = currLength;
                }
            }

            return maxLenght;
        }
    }
}
