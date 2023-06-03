namespace Poker.Domain.ValueObjects
{
    public interface IValueObject<out T>
    {
        T Value { get; }
    }
}