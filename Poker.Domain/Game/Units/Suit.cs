using System;
using Poker.Domain.ValueObjects;

namespace Poker.Domain.Game.Units
{
    public readonly struct Suit : IValueObject<SuitEnum>
    {
        public SuitEnum Value { get; }

        public Suit(SuitEnum value)
        {
            Value = value switch
            {
                >= SuitEnum.Heart and <= SuitEnum.Spade => value,
                _ => throw new ArgumentOutOfRangeException(nameof(value)),
            };
        }

        public override string ToString() => Value switch
        {
            SuitEnum.Heart => "h",
            SuitEnum.Diamond => "d",
            SuitEnum.Club => "c",
            SuitEnum.Spade => "s",
            _ => throw new ArgumentOutOfRangeException(nameof(Value), "Card value isn't recognized.")
        };
    }
}