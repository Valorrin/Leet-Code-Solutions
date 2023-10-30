using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static int[] ProductExceptSelf(int[] nums)
        {
            int length = nums.Length;

            int[] result = new int[length];
            int[] leftProduct = new int[length];
            int[] rightProduct = new int[length];

            for (int i = 0, product = 1; i < length; i++)
            {
                product *= nums[i];
                leftProduct[i] = product;
            }

            for (int i = nums.Length - 1, product = 1; i >= 0; i--)
            {
                product *= nums[i];
                rightProduct[i] = product;
            }

            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    result[0] = rightProduct[1];
                }
                else if (i == length - 1)
                {
                    result[i] = leftProduct[length - 2];
                }
                else
                {
                    result[i] = leftProduct[i-1] * rightProduct[i+1];
                }
            }


            return result;
        }

        public static bool IsValidSudoku(char[][] board)
        {
            HashSet<char>[] rowDigits = new HashSet<char>[9];
            HashSet<char>[] colDigits = new HashSet<char>[9];
            HashSet<char>[] boxDigits = new HashSet<char>[9];

            for (int i = 0; i < 9; i++)
            {
                rowDigits[i] = new HashSet<char>();
                colDigits[i] = new HashSet<char>();
                boxDigits[i] = new HashSet<char>();
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char element = board[i][j];

                    if (element == '.')
                    {
                        continue;
                    }

                    if (!rowDigits[i].Add(element))
                    {
                        return false;
                    }

                    if (!colDigits[j].Add(element))
                    {
                        return false;
                    }

                    int box = (3 * (i / 3)) + (j / 3);

                    if (!boxDigits[box].Add(element))
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public static int LongestConsecutive(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            int max = 0;
            int current = 1;

            Array.Sort(nums);

            HashSet<int> numsHs = new HashSet<int>(nums);
            nums = numsHs.ToArray();

            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i]+1 == nums[i+1])
                {
                    current++;
                }
                else
                {
                    if (max < current)
                    {
                        max = current;
                    }
                    current = 1;
                }
            }

            if (current > max)
            {
                max = current;
            }

            return max;
        }

        public static bool IsPalindrome(string s)
        {
            s = s.ToLower().Trim();
            s = String.Concat(Array.FindAll(s.ToCharArray(), Char.IsLetterOrDigit));

            char[] sArray = s.ToCharArray();
            Array.Reverse(sArray);
            string reverseString = new string(sArray);

            if (s == reverseString)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static int MaxProfit(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }

                int profit = prices[i] - minPrice;
                if (profit > maxProfit)
                {
                    maxProfit = profit;
                }
            }

            return maxProfit;
        }

        public static IList<IList<int>> Generate(int numRows)
        {
            int[][] result = new int[numRows][];
            int i = 0;
            while (i < numRows)
            {
                result[i] = new int[i + 1];
                result[i][0] = result[i][i] = 1;
                int j = 1;
                while (j < i)
                {
                    result[i][j] = result[i - 1][j - 1] + result[i - 1][j];
                    j++;
                }
                i++;
            }
            return result;
        }

        public static IList<int> GetRow(int rowIndex)
        {
            IList<int> row = new List<int>();

            for (int i = 0; i <= rowIndex; i++)
            {
                row.Add(1);

                for (int j = i - 1; j > 0; j--)
                {
                    row[j] += row[j - 1];
                }
            }

            return row;
        }

        public static int LengthOfLastWord(string s)
        {
            s = s.Trim();

            int length = 0;

            for (int i = s.Length - 1; i >= 0; i--)
            {

                if (s[i] == ' ')
                {
                    break;
                }
                length++;
            }

            return length;
        }

        public static int[] PlusOne(int[] digits)
        {
            int n = digits.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }
                digits[i] = 0;
            }
            int[] newNumber = new int[n + 1];
            newNumber[0] = 1;

            return newNumber;
        }

    }
}
