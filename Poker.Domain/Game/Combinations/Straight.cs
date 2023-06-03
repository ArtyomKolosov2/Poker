using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Shared;

namespace Poker.Domain.Game.Combinations
{
    public class Straight : Combination
    {
        private Straight(IReadOnlyCollection<Card> cards, IHand hand) : base(cards, hand)
        {
        }

        public static Result<ICombination, Error> Create(IReadOnlyCollection<Card> cards, IHand hand)
        {
            return CombinationHelper.IsStraight(cards)
                ? Result<ICombination, Error>.Success(new Straight(cards, hand))
                : Result<ICombination, Error>.Failure(Error.Empty);
        }

        public override int CompareTo(ICombination? other)
        {
            var baseComperison = base.CompareTo(other);
            if (baseComperison != 0)
                return baseComperison;

            var otherCombination = (Straight)other!;
            var otherCombinationSum = CombinationHelper.GetStraightAsRankValueAndAddAceIfRequired(otherCombination).Sum();
            var currentCombinationSum = CombinationHelper.GetStraightAsRankValueAndAddAceIfRequired(this).Sum();

            return currentCombinationSum.CompareTo(otherCombinationSum);
        }
    }
}