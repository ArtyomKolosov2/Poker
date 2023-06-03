using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> enumerable, int k) 
        {
            var nums = enumerable.ToList();
            var result = new List<IEnumerable<T>>();
            if (nums.Count == 0 || k == 0 || nums.Count < k)
            {
                return result;
            }

            if (k == 1)
            {
                foreach (var num in nums)
                {
                    result.Add(new List<T> { num });
                }
                return result;
            }

            var subNums = nums.GetRange(1, nums.Count - 1);
            var subCombinations = GetCombinations(subNums, k - 1);

            foreach (var subCombination in subCombinations)
            {
                var newCombination = new List<T>(subCombination);
                newCombination.Insert(0, nums[0]);
                result.Add(newCombination);
            }

            result.AddRange(GetCombinations(subNums, k));

            return result;
        }
    }
}