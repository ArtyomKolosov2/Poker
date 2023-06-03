using System;
using Poker.Domain.ValueObjects;

namespace Poker.Domain.Game.Units
{
    public readonly struct Rank : IValueObject<RankEnum>
    {
        public RankEnum Value { get; }

        public Rank(RankEnum value)
        {
            Value = value switch
            {
                >= RankEnum.Two and <= RankEnum.Ace => value,
                _ => throw new ArgumentOutOfRangeException(nameof(value)),
            };
        }

        public override string ToString() => Value switch
        {
            RankEnum.Two => "2",
            RankEnum.Three => "3",
            RankEnum.Four => "4",
            RankEnum.Five => "5",
            RankEnum.Six => "6",
            RankEnum.Seven => "7",
            RankEnum.Eight => "8",
            RankEnum.Nine => "9",
            RankEnum.Ten => "T",
            RankEnum.Jack => "J",
            RankEnum.Queen => "Q",
            RankEnum.King => "K",
            RankEnum.Ace => "A",
            _ => throw new ArgumentOutOfRangeException(nameof(Value), "Card value isn't recognized.")
        };
    }
}