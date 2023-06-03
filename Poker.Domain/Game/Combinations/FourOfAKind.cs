using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Shared;

namespace Poker.Domain.Game.Combinations
{
    public class FourOfAKind : Combination
    {
        private const int CardCombinationCount = 4;

        private IReadOnlyCollection<Card> Kickers { get; init; }

        private FourOfAKind(IReadOnlyCollection<Card> cards, IHand hand, IReadOnlyCollection<Card> kickers) : base(cards, hand)
        {
            Kickers = kickers;
        }

        public static Result<ICombination, Error> Create(IReadOnlyCollection<Card> cards, IHand hand)
        {
            var cardDuplicates = cards.GetCardsWithExactCardCountOfSameRank(CardCombinationCount).ToArray();
            if (cardDuplicates.Length == 1)
            {
                var cardForCombination = cardDuplicates.SelectMany(x => x).ToArray();
                var fourOfAKind = new FourOfAKind(cardForCombination, hand, cards.Except(cardForCombination).ToArray());
      
                return Result<ICombination, Error>.Success(fourOfAKind);
            }

            return Result<ICombination, Error>.Failure(Error.Empty);
        }

        public override int CompareTo(ICombination? other)
        {
            var baseComperison = base.CompareTo(other);
            if (baseComperison != 0)
                return baseComperison;

            var otherCombination = (FourOfAKind)other!;

            var currentMax = this.Max(x => (int)x.Rank.Value);
            var otherMax = otherCombination.Max(x => (int)x.Rank.Value);
            var maxComparison = currentMax.CompareTo(otherMax);
            if (maxComparison != 0)
                return maxComparison;

            return CombinationHelper.CompareKickers(Kickers, otherCombination.Kickers);
        }
    }
}