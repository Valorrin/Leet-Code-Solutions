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

        public static bool ValidParentheses(string s)
        {
            // Get ready initial state (enforce element type)
            var k = new Stack<char>();
            // Evaluate each character for potential mismatch 
            foreach (char c in s)
            {
                // Push closing round bracket onto the stack
                if (c == '(') { k.Push(')'); continue; }
                // Push closing curly bracket onto the stack
                if (c == '{') { k.Push('}'); continue; }
                // Push closing square bracket onto the stack
                if (c == '[') { k.Push(']'); continue; }
                // Look out for imbalanced strings or mismatches
                if (k.Count == 0 || c != k.Pop()) return false;
            }
            // Empty stack means string is valid and invalid otherwise
            return k.Count == 0;
        }
    }
}
