using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Shared;
using Poker.Shared.Extensions;

namespace Poker.Domain.Game.Combinations
{
    public class FullHouse : Combination
    {

        private IReadOnlyCollection<Card> Pair { get; init; }

        private IReadOnlyCollection<Card> ThreeOfaKind { get; init; }

        private FullHouse(IReadOnlyCollection<Card> cards, IHand hand, IReadOnlyCollection<Card> pair, IReadOnlyCollection<Card> tripleOfaKind) : base(cards, hand)
        {
            Pair = pair;
            ThreeOfaKind = tripleOfaKind;
        }

        public static Result<ICombination, Error> Create(IReadOnlyCollection<Card> cards, IHand hand)
        {
            var threeOfAKind = cards.GetCardsWithExactCardCountOfSameRank(Combinations.ThreeOfaKind.CardCombinationCount).ToArray();
            var pair = cards.GetCardsWithExactCardCountOfSameRank(Combinations.Pair.CardCombinationCount).ToArray();

            if (threeOfAKind.Length == 1 && pair.Length == 1)
            {
                return Result<ICombination, Error>.Success(new FullHouse(cards, hand, pair.SelectMany(x => x).ToArray(), threeOfAKind.SelectMany(x => x).ToArray()));
            }

            return Result<ICombination, Error>.Failure(Error.Empty);
        }

        public override int CompareTo(ICombination? other)
        {
            var baseComperison = base.CompareTo(other);
            if (baseComperison != 0)
                return baseComperison;

            var otherCombination = (FullHouse)other!;

            var maxOfTripleOfAKind = ThreeOfaKind.Max(x => (int)x.Rank.Value);
            var otherMaxOfTripleOfaKind = otherCombination.ThreeOfaKind.Max(x => (int)x.Rank.Value);
            var maxComparison = maxOfTripleOfAKind.CompareTo(otherMaxOfTripleOfaKind);
            if (maxComparison != 0)
                return maxComparison;

            var maxOfPair = Pair.Max(x => (int)x.Rank.Value);
            var otherMaxOfPair = otherCombination.Pair.Max(x => (int)x.Rank.Value);
            return maxOfPair.CompareTo(otherMaxOfPair);
        }
    }
}