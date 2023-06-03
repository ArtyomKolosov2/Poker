using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Shared;

namespace Poker.Domain.Game.Combinations
{
    public class TwoPairs : Combination
    {
        private const int CountOfPairs = 2;

        private IReadOnlyCollection<Card> Kickers { get; init; }

        private TwoPairs(IReadOnlyCollection<Card> cards, IHand hand, IReadOnlyCollection<Card> kickers) : base(cards, hand)
        {
            Kickers = kickers;
        }

        public static Result<ICombination, Error> Create(IReadOnlyCollection<Card> cards, IHand hand)
        {
            var pairs = cards.GetCardsWithExactCardCountOfSameRank(Pair.CardCombinationCount).ToArray();

            if (pairs.Length == CountOfPairs)
            {
                var combinationCards = pairs.SelectMany(x => x).ToArray();
                return Result<ICombination, Error>.Success(new TwoPairs(combinationCards, hand, cards.Except(combinationCards).ToArray()));
            }

            return Result<ICombination, Error>.Failure(Error.Empty);
        }

        public override int CompareTo(ICombination? other)
        {
            var baseComperison = base.CompareTo(other);
            if (baseComperison != 0)
                return baseComperison;

            var otherCombination = (TwoPairs)other!;

            var currentMax = this.Distinct().OrderByDescending(x => (int)x.Rank.Value);
            var otherMax = otherCombination.Distinct().OrderByDescending(x => (int)x.Rank.Value);

            int? comparisonResult = currentMax.Zip(otherMax, (first, second) => ((int)first.Rank.Value).CompareTo((int)second.Rank.Value)).FirstOrDefault(x => x != 0);

            if (comparisonResult is not null)
                return comparisonResult.Value;

            return CombinationHelper.CompareKickers(Kickers, otherCombination.Kickers);
        }
    }
}