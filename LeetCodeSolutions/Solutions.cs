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
    }
}
