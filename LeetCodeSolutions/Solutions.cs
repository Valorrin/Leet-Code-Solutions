using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            var stack = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '(') { stack.Push(')'); continue; }
                if (c == '{') { stack.Push('}'); continue; }
                if (c == '[') { stack.Push(']'); continue; }
                if (stack.Count == 0 || c != stack.Pop()) return false;
            }
            return stack.Count == 0;
        }

        public static bool ContainsDuplicate(int[] nums)
        {
            HashSet<int> uniqueNums = new HashSet<int>(nums);

            if (nums.Length == uniqueNums.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int MissingNumber(int[] nums)
        {
            int expectedSum = 0;
            int currentSum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                expectedSum += i + 1;
                currentSum += nums[i];
            }

            return expectedSum - currentSum;
        }

        public static bool IsAnagram(string s, string t)
        {
            s = String.Concat(s.OrderBy(x => x));
            t = String.Concat(t.OrderBy(x => x));

            return s == t;
        }

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, IList<string>> anagramPairs = new Dictionary<string, IList<string>>();

            foreach (string str in strs) 
            {
                string key = String.Concat(str.OrderBy(x => x));

                if (!anagramPairs.ContainsKey(key))
                {
                    anagramPairs[key] = new List<string>();
                }

                anagramPairs[key].Add(str);
            }

            return anagramPairs.Values.ToList();
        }

        public static int RomanToInt(string s)
        {
            int sum = 0;

            Dictionary<char, int> dict = new Dictionary<char, int>();
   
            dict.Add('I', 1);
            dict.Add('V', 5);
            dict.Add('X', 10);
            dict.Add('L', 50);
            dict.Add('C', 100);
            dict.Add('D', 500);
            dict.Add('M', 1000);

            for (int i = 0; i < s.Length; i++)
            {

                if (i + 1 < s.Length && dict[s[i + 1]] > dict[s[i]])
                {
                    sum -= dict[s[i]];
                }
                else
                {
                    sum += dict[s[i]];
                }
            }

            return sum;
        }

        public static int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                dict[nums[i]] = dict.GetValueOrDefault(nums[i], 0) + 1;
            }

            int[] result = new int[k];

            foreach (var item in dict.OrderByDescending(item => item.Value))
            {
                k -= 1;
                if (k >= 0)
                    result[k] = item.Key;
                else
                    break;
            }

            return result;
        }

        public static void Rotate(int[] nums, int k)
        {
            int[] result = new int[nums.Length];

            for (int i = 0; i < k; i++)
            {
                result = new int[nums.Length];

                for (int j = 1; j < nums.Length; j++)
                {
                    result[j] = nums[j - 1];
                }

                result[0] = nums[nums.Length - 1];

                nums = result;
            }

            Console.WriteLine(String.Join(',', result));
        }
    }
}
