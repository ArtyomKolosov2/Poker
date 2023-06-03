using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Shared;

namespace Poker.Domain.Game.Combinations
{
    public class Pair : Combination
    {
        internal const int CardCombinationCount = 2;

        private IReadOnlyCollection<Card> Kickers { get; init; }

        private Pair(IReadOnlyCollection<Card> cards, IHand hand, IReadOnlyCollection<Card> kickers) : base(cards, hand)
        {
            Kickers = kickers;
        }

        public static Result<ICombination, Error> Create(IReadOnlyCollection<Card> cards, IHand hand)
        {
            var pairCards = cards.GetCardsWithExactCardCountOfSameRank(CardCombinationCount).ToArray();

            if (pairCards.Length == 1)
            {
                var combinationCards = pairCards.SelectMany(x => x).ToArray();
                var pair = new Pair(combinationCards, hand, cards.Except(combinationCards).ToArray());

                return Result<ICombination, Error>.Success(pair);
            }

            return Result<ICombination, Error>.Failure(Error.Empty);
        }

        public override int CompareTo(ICombination? other)
        {
            var baseComperison = base.CompareTo(other);
            if (baseComperison != 0)
                return baseComperison;

            var otherCombination = (Pair)other!;

            var currentMax = this.Max(x => (int)x.Rank.Value);
            var otherMax = otherCombination.Max(x => (int)x.Rank.Value);
            var maxComparison = currentMax.CompareTo(otherMax);
            if (maxComparison != 0)
                return maxComparison;

            return CombinationHelper.CompareKickers(Kickers, otherCombination.Kickers);
        }
    }
}