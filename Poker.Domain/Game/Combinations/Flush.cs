using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Shared;

namespace Poker.Domain.Game.Combinations
{
    public class Flush : Combination
    {
        private const int CountForFlush = 5;

        private Flush(IReadOnlyCollection<Card> cards, IHand hand) : base(cards, hand)
        {
        }

        public static Result<ICombination, Error> Create(IReadOnlyCollection<Card> cards, IHand hand)
        {
            var orderedCards = cards
                .GroupBy(x => x.Suit.Value)
                .Where(x => x.Count() == CountForFlush)
                .ToArray();

            var flush = orderedCards.SingleOrDefault();

            return flush is not null
                ? Result<ICombination, Error>.Success(new Flush(cards, hand))
                : Result<ICombination, Error>.Failure(Error.Empty);
        }

        public override int CompareTo(ICombination? other)
        {
            var baseComperison = base.CompareTo(other);
            if (baseComperison != 0)
                return baseComperison;

            var otherCombination = (Flush)other!;

            var currentMax = this.Distinct().OrderByDescending(x => (int)x.Rank.Value);
            var otherMax = otherCombination.Distinct().OrderByDescending(x => (int)x.Rank.Value);

            var comparisonResult = currentMax.Zip(otherMax, (first, second) =>
            {
                return first.Rank.Value.CompareTo(second.Rank.Value);
            }).FirstOrDefault(x => x != 0);

            return comparisonResult;
        }
    }
}