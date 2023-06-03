using System;

namespace Poker.Domain.ValueObjects
{
    public readonly struct NonNegativeNumber : IValueObject<int>
    {
        public int Value { get; }

        public NonNegativeNumber(int value)
        {
            Value = value switch
            {
                >= 0 => value,
                _ => throw new ArgumentException("Given number is negative.")
            };
        }

        public static implicit operator NonNegativeNumber(int number) => new(number);

        public static implicit operator int(NonNegativeNumber nonNegativeNumber) => nonNegativeNumber.Value;
    }
}