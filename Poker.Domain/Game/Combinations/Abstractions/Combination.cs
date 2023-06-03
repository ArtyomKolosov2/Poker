using System;
using System.Collections.Generic;
using Poker.Domain.Game.Units;

namespace Poker.Domain.Game.Combinations.Abstractions
{
    public abstract class Combination : CardCollection, ICombination
    {
        internal static readonly IReadOnlyDictionary<string, int> NameAndPriority = new Dictionary<string, int>()
        {
            { nameof(StraightFlush), 9 },
            { nameof(FourOfAKind), 8 },
            { nameof(FullHouse), 7 },
            { nameof(Flush), 6 },
            { nameof(Straight), 5 },
            { nameof(ThreeOfaKind), 4 },
            { nameof(TwoPairs), 3 },
            { nameof(Pair), 2 },
            { nameof(HighCard), 1 }
        };

        private const int CombinationCapacity = 5;

        protected Combination(IReadOnlyCollection<Card> cards, IHand hand) : base(cards, CombinationCapacity)
        {
            Hand = hand ?? throw new ArgumentNullException(nameof(hand));
        }

        public IHand Hand { get; private init; }

        public virtual int CompareTo(ICombination? other)
        {
            if (other is null)
                return 1;

            if (ReferenceEquals(other, this))
                return 0;

            return NameAndPriority[GetType().Name].CompareTo(NameAndPriority[other.GetType().Name]);
        }
    }
}