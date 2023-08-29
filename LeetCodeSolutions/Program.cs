using System;
using System.Linq;

namespace LeetCodeSolutions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(String.Join(",", Solutions.TwoSum(new int[] { 2, 7, 11, 15 }, 9)));
        }
    }
}