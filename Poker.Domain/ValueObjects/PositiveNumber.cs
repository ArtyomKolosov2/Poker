using System;

namespace Poker.Domain.ValueObjects
{
    public readonly struct PositiveNumber : IValueObject<int>
    {
        public int Value { get; }

        public PositiveNumber(int value)
        {
            Value = value switch
            {
                > 0 => value,
                _ => throw new ArgumentException("Given number is negative or equal to zero.")
            };
        }

        public static implicit operator PositiveNumber(int number) => new(number);

        public static implicit operator int(PositiveNumber positiveNumber) => positiveNumber.Value;
    }
}