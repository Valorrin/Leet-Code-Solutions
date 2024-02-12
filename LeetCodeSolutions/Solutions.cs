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
                    result[i] = leftProduct[i - 1] * rightProduct[i + 1];
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
                if (nums[i] + 1 == nums[i + 1])
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

        public static int RemoveDuplicates(int[] nums)
        {
            int k = 1;
            int previouse = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] != previouse)
                {
                    nums[k] = nums[i];
                    k++;
                }
                previouse = nums[i];
            }
            return k;

        }

        public static int SingleNumber(int[] nums)
        {
            int res = nums[0];

            for (int i = 1; i < nums.Length; i++)
                res ^= nums[i];

            return res;
        }

        public bool IsHappy(int n)
        {
            Dictionary<int, bool> map = new Dictionary<int, bool>();
            while (!map.ContainsKey(n))
            {
                map.Add(n, true);
                if (n == 1)
                {
                    return true;
                }
                n = SumOfDigitSquare(n);
            }
            return false;
        }

        int SumOfDigitSquare(int n)
        {
            int currentDigit = 0, sum = 0;
            while (n > 0)
            {
                currentDigit = n % 10;
                n = n / 10;
                sum += currentDigit * currentDigit;
            }
            return sum;
        }

        public bool IsUgly(int n)
        {
            if (n <= 0) return false;

            while (n % 2 == 0) n = n / 2;
            while (n % 3 == 0) n = n / 3;
            while (n % 5 == 0) n = n / 5;
            return n == 1;
        }

        public IList<string> SummaryRanges(int[] nums)
        {
            var result = new List<string>();
            for (int i = 0; i < nums.Length; i++)
            {
                int start = nums[i];
                while (i < nums.Length - 1 && nums[i + 1] - nums[i] == 1)
                {
                    i++;
                }
                if (start != nums[i])
                    result.Add($"{start}->{nums[i]}");
                else
                    result.Add($"{start}");
            }

            return result;

        }

        public bool CanWinNim(int n)
        {
            return n % 4 != 0;
        }

        public bool IsPowerOfThree(int n)
        {
            if (n == 0)
                return false;

            while (n != 1)
            {
                if (n % 3 != 0)
                    return false;
                n = n / 3;
            }

            return true;
        }

        public static string LongestPalindrome(string s)
        {
            string result = "";
            int resultLength = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int l = i;
                int r = i;
                int l2 = i;
                int r2 = i + 1;

                while (l >= 0 && r < s.Length && s[l] == s[r])
                {
                    if (resultLength < r - l + 1)
                    {
                        resultLength = r - l + 1;
                        result = s.Substring(l, resultLength);
                    }

                    l--;
                    r++;
                }

                while (l2 >= 0 && r2 < s.Length && s[l2] == s[r2])
                {
                    if (resultLength < r2 - l2 + 1)
                    {
                        resultLength = r2 - l2 + 1;
                        result = s.Substring(l2, resultLength);
                    }

                    l2--;
                    r2++;
                }

            }
            return result;

        }

        public static int BinarySearch(int[] nums, int target)
        {
            int low = 0;
            int high = nums.Length - 1;

            while (low <= high)
            {
                int middle = high - (high - low) / 2;

                if (nums[middle] == target) return middle;
                else if (target < nums[middle]) high = middle - 1;
                else low = middle + 1;
            }

            return -1;
        }

        public static bool SearchMatrix(int[][] matrix, int target)
        {
            int lowRow = 0;
            int highRow = matrix.GetLength(0) - 1;

            while (lowRow <= highRow)
            {
                int currentRow = lowRow + (highRow - lowRow) / 2;
                int value = matrix[currentRow][0];
                int lastRowElement = matrix[currentRow].Last();

                if (value == target) return true;
                else if (target < value) highRow = currentRow - 1;
                else if (target > value && target > lastRowElement) lowRow = currentRow + 1;
                else
                {
                    int highRowElement = matrix[currentRow].Length - 1;
                    int lowRowElement = 0;

                    while (lowRowElement <= highRowElement)
                    {
                        int currentRowIndex = lowRowElement + (highRowElement - lowRowElement) / 2;
                        int value2 = matrix[currentRow][currentRowIndex];

                        if (value2 == target) return true;
                        else if (target < value2) highRowElement = currentRowIndex - 1;
                        else lowRowElement = currentRowIndex + 1;
                    }

                    return false;
                }
            }

            return false;
        }

        public static int FindMin(int[] nums)
        {
            int low = 0;
            int high = nums.Length - 1;
            int target = nums.Min();

            while (low <= high)
            {
                int current = low + (high - low) / 2;
                int value = nums[current];

                if (value == target)
                {
                    int[] temp = new int[current];

                    for (int i = 0; i < current; i++)
                    {
                        temp[i] = nums[i];
                    }

                    for (int i = current; i < nums.Length; i++)
                    {
                        nums[i - current] = nums[i];
                    }

                    for (int i = 0; i < temp.Length; i++)
                    {
                        nums[nums.Length - current + i] = temp[i];
                    }
                    return target;
                }
                else if (value < target) high = current - 1;
                else low = current + 1;
            }
            return target;
        }

        public static int Search(int[] nums, int target)
        {
            int[] numsOriginal = new int[nums.Length];
            Array.Copy(nums, numsOriginal, nums.Length);

            int minElement = nums.Min();
            int index = Array.IndexOf(nums, minElement);

            int[] temp = new int[index];

            for (int i = 0; i < index; i++)
            {
                temp[i] = nums[i];
            }

            for (int i = 0; i < nums.Length - index; i++)
            {
                nums[i] = nums[i + index];
            }

            for (int i = 0; i < index; i++)
            {
                nums[nums.Length - index + i] = temp[i];
            }


            int low = 0;
            int high = nums.Length - 1;

            while (low <= high)
            {
                int middle = high - (high - low) / 2;
                int value = nums[middle];

                if (target == value) return Array.IndexOf(numsOriginal, value);
                else if (target < value) high = middle - 1;
                else low = middle + 1;
            }

            return -1;
        }

        public class TimeMap
        {
            private readonly Dictionary<string, IList<(int time, string value)>> map = new();

            public void Set(string key, string value, int timestamp)
            {
                if (!map.ContainsKey(key))
                {
                    map[key] = new List<(int, string)>();
                }

                map[key].Add((timestamp, value));
            }

            public string Get(string key, int timestamp)
            {
                if (!map.ContainsKey(key))
                {
                    return string.Empty;
                }

                var list = map[key];
                var left = 0;
                var right = list.Count - 1;

                while (left <= right)
                {
                    var mid = (right + left) / 2;
                    if (list[mid].time == timestamp)
                    {
                        return list[mid].value;
                    }

                    if (list[mid].time <= timestamp)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }

                return left == 0 ? string.Empty : list[left - 1].value;
            }
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {

            ListNode dummyHead = new ListNode(-1);
            ListNode current = dummyHead;


            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val)
                {
                    current.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    current.next = list2;
                    list2 = list2.next;
                }
                current = current.next;
            }

            if (list1 != null)
            {
                current.next = list1;
            }
            else
            {
                current.next = list2;
            }

            return dummyHead.next;
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();
            Array.Sort(nums);

            int target = 0;

            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue; 
                }

                int left = i + 1;
                int right = nums.Length - 1;

                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];

                    if (sum == target)
                    {
                        result.Add(new List<int> { nums[i], nums[left], nums[right] });

                        while (left < right && nums[left] == nums[left + 1])
                        {
                            left++;
                        }
                        while (left < right && nums[right] == nums[right - 1])
                        {
                            right--;
                        }

                        left++;
                        right--;
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }

            return result;
        }

        public string LongestCommonPrefix(string[] strs)
        {
            int index = 0;
            var sb = new StringBuilder();
            while (index < strs[0].Length)
            {
                char ch = strs[0][index];
                for (int i = 1; i < strs.Length; i++)
                {
                    if (index == strs[i].Length || strs[i][index] != ch)
                    {
                        return sb.ToString();
                    }
                }

                sb.Append(ch);

                index++;
            }

            return sb.ToString();
        }

        public int RemoveElement(int[] nums, int val)
        {
            int result = 0;
            foreach (var item in nums)
            {
                if (item != val)
                {
                    nums[result] = item;
                    result++;
                }
            }

            return result;
        }

        public int StrStr(string haystack, string needle)
        {
            for (var i = 0; i < haystack.Length - needle.Length + 1; i++)
            {
                if (haystack.Substring(i, needle.Length) == needle)
                {
                    return i;
                }
            }

            return -1;
        }

        public int ClimbStairs(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            int[] dp = new int[n + 1];
            dp[1] = 1;
            dp[2] = 2;
            for (int i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
            return dp[n];
        }

        public int SearchInsert(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1, mid;
            while (left <= right)
            {
                mid = (right + left) / 2;
                if (nums[mid] < target) left = mid + 1;
                else if (nums[mid] > target) right = mid - 1;
                else return mid;
            }
            return left;
        }

        public string SimplifyPath(string path)
        {
            String[] p = path.Split('/');
            List<string> sList = new();
            foreach (var px in p)
            {
                if (string.IsNullOrEmpty(px) || px == ".")
                    continue;
                if (px == "..")
                {
                    if (sList.Count > 0)
                        sList.RemoveAt(sList.Count - 1);

                }
                else
                    sList.Add(px);
            }
            StringBuilder sb = new();
            foreach (var s in sList)
                sb.Append("/" + s);
            if (sb.Length == 0)
                sb.Append("/");
            return sb.ToString();
        }

        public int RemoveDuplicatess(int[] nums)
        {
            var replaceIndex = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (replaceIndex - 2 >= 0 && nums[replaceIndex - 2] == nums[i])
                {
                    continue;
                }
                nums[replaceIndex] = nums[i];
                replaceIndex++;
            }
            return replaceIndex;
        }

    }
}
