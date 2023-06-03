using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Shared;

namespace Poker.Domain.Game.Combinations
{
    public class HighCard : Combination
    {
        private IReadOnlyCollection<Card> Kickers { get; init; }

        private HighCard(IReadOnlyCollection<Card> cards, IHand hand, IReadOnlyCollection<Card> kickers) : base(cards, hand)
        {
            Kickers = kickers;
        }

        public static Result<ICombination, Error> Create(IReadOnlyCollection<Card> cards, IHand hand)
        {
            var highCard = cards.OrderByDescending(x => x.Rank.Value).First();
            return Result<ICombination, Error>.Success(new HighCard(new[] { highCard }, hand, cards.Except(new[] { highCard }).ToArray()));
        }

        public override int CompareTo(ICombination? other)
        {
            var baseComperison = base.CompareTo(other);
            if (baseComperison != 0)
                return baseComperison;

            var otherCombination = (HighCard)other!;

            var currentMax = this.Max(x => (int)x.Rank.Value);
            var otherMax = otherCombination.Max(x => (int)x.Rank.Value);
            var maxComparison = currentMax.CompareTo(otherMax);
            if (maxComparison != 0)
                return maxComparison;

            return CombinationHelper.CompareKickers(Kickers, otherCombination.Kickers);
        }
    }
}