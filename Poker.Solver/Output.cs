
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poker.Domain.Game.Combinations.Abstractions;

namespace Evolution.Poker.Solver
{
    public static class Output
    {
        public static string GetOutput(IReadOnlyCollection<ICombination> combinationsCollection)
        {
            var ordered = combinationsCollection.OrderBy(x => x).ToList();
            var groupedResult = GroupCombinationsOfSamePower(ordered);
            return PrintCombinationFromGrouping(groupedResult);
        }

        private static string PrintCombinationFromGrouping(IEnumerable<IEnumerable<ICombination>> groupedResult)
        {
            var stringBuilder = new StringBuilder();
            foreach (var group in groupedResult)
            {
                if (group.Count() > 1)
                {
                    var strings = group.Select(x => string.Join("", x.Hand.Select(card => card.ToString()))).OrderBy(x => x);
                    stringBuilder.AppendJoin('=', strings);
                }
                else
                {
                    var cards = string.Join("", group.Single().Hand.Select(x => x.ToString()));
                    stringBuilder.Append(cards);
                }
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString().Trim();
        }

        private static IEnumerable<IEnumerable<ICombination>> GroupCombinationsOfSamePower(IList<ICombination> ordered)
        {
            var groupedResult = new List<List<ICombination>>();
            var groupingIndex = 0;
            for (int index = 0; index < ordered.Count; index++)
            {
                if (groupedResult.ElementAtOrDefault(groupingIndex) is null)
                    groupedResult.Add(new List<ICombination>());

                var previous = index == 0 ? null : ordered[index - 1];
                if (ordered[index].CompareTo(previous) == 0)
                    groupedResult[groupingIndex - 1].Add(ordered[index]);
                else
                    groupedResult[groupingIndex++].Add(ordered[index]);
            }

            return groupedResult;
        }
    }
}