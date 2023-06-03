using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Domain.Game.Units;
using Poker.Domain.ValueObjects;
using Poker.Shared;

namespace Poker.Domain.Game.Combinations
{
    public static class CombinationHelper
    {
        private const int StraightLength = 5;

        internal static bool IsStraight(IReadOnlyCollection<Card> cards)
        {
            var uniqueValueCount = cards.Select(x => x.Rank.Value).Distinct().Count();

            if (uniqueValueCount < StraightLength)
                return false;

            var straightValues = GetStraightAsRankValueAndAddAceIfRequired(cards).ToArray();
            for (var index = 0; index + StraightLength <= straightValues.Length; index++)
            {
                var range = Enumerable.Range(straightValues[index], StraightLength);
                if (straightValues[index..(index + StraightLength)].SequenceEqual(range))
                    return true;
            }

            return false;
        }

        internal static bool IsFlush(IReadOnlyCollection<Card> cards) =>
            cards.Select(c => c.Suit.Value).Distinct().Count() == 1;

        internal static IReadOnlyCollection<int> GetStraightAsRankValueAndAddAceIfRequired(IReadOnlyCollection<Card> cards)
        {
            var hand = cards.ToArray();
            var handCardValueNumbers = hand.Select(card => (int)card.Rank.Value).OrderBy(x => x).ToArray();
            var isAceAndTwoPresent = handCardValueNumbers.Contains((int)RankEnum.Ace) && handCardValueNumbers.Contains((int)RankEnum.Two);
            return (isAceAndTwoPresent ? handCardValueNumbers.Prepend((int)RankEnum.Two - 1) : handCardValueNumbers).ToArray();
        }

        internal static IEnumerable<IEnumerable<Card>> GetCardsWithExactCardCountOfSameRank(this IEnumerable<Card> cards, PositiveNumber size) =>
            cards.GroupBy(card => card.Rank.Value).Where(x => x.Count() == size);

        internal static int CompareKickers(IReadOnlyCollection<Card> firstKickers, IReadOnlyCollection<Card> secondKickers)
        {
            var uniqueKickersForCurrentCombination = firstKickers.Except(secondKickers).OrderByDescending(x => (int)x.Rank.Value);
            var uniqueKickersForOtherCombination = secondKickers.Except(firstKickers).OrderByDescending(x => (int)x.Rank.Value);

            var kickersComparisonResult = uniqueKickersForCurrentCombination.Zip(uniqueKickersForOtherCombination, (first, second) =>
            {
                return first.Rank.Value.CompareTo(second.Rank.Value);
            }).FirstOrDefault(x => x != 0);

            return kickersComparisonResult;
        }

        public static Result<ICombination, Error> GetStrongestCombinationForCards(IReadOnlyCollection<Card> cards, IHand hand)
        {
            foreach (var combinationName in Combination.NameAndPriority.Keys)
            {
                var combinationObject = TryCreateCardCombination(combinationName, hand, cards);
                if (combinationObject.IsSuccess)
                    return combinationObject;
            }

            return Result<ICombination, Error>.Failure(Error.WithMessage("Unexpected error occured."));
        }

        private static Result<ICombination, Error> TryCreateCardCombination(string combinationName, IHand hand, IReadOnlyCollection<Card> cards)
        {
            Result<ICombination, Error> combinationObject = combinationName switch
            {
                nameof(StraightFlush) => StraightFlush.Create(cards, hand),
                nameof(FourOfAKind) => FourOfAKind.Create(cards, hand),
                nameof(FullHouse) => FullHouse.Create(cards, hand),
                nameof(Flush) => Flush.Create(cards, hand),
                nameof(Straight) => Straight.Create(cards, hand),
                nameof(ThreeOfaKind) => ThreeOfaKind.Create(cards, hand),
                nameof(TwoPairs) => TwoPairs.Create(cards, hand),
                nameof(Pair) => Pair.Create(cards, hand),
                nameof(HighCard) => HighCard.Create(cards, hand),
                _ => throw new ArgumentOutOfRangeException(nameof(combinationName), "Unrecognized combination type.")
            };

            return combinationObject;
        }
    }
}