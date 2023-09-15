using System;
using System.Linq;

namespace LeetCodeSolutions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(String.Join(",", Solutions.TwoSum(new int[] { 2, 7, 11, 15 }, 9)));
            // Console.WriteLine(Solutions.LengthOfLongestSubstring("pwwkew").ToString());
            // Console.WriteLine(Solutions.RomanToInt("III"));
            // Console.WriteLine(Solutions.TopKFrequent(new int[] { 1, 1, 1, 2, 2, 3 }, 2));
            Solutions.Rotate(new int[] { -1, -100, 3, 99 }, 2);
        }
    }
}