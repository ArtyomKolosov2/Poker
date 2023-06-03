using Poker.Domain.ValueObjects;
using Poker.Shared;

namespace Poker.Domain.BusinessRules
{
    public class CardCapacityRule : IBusinessRule
    {
        private readonly PositiveNumber _maxCapacity;
        private readonly NonNegativeNumber _currentCapacity;
        
        public string Message => $"Count of cards ({_currentCapacity}) exceeds deck capacity ({_maxCapacity}).";

        public CardCapacityRule(PositiveNumber maxCapacity, NonNegativeNumber currentCapacity)
        {
            _maxCapacity = maxCapacity;
            _currentCapacity = currentCapacity;
        }

        public Result<bool, Error> IsViolated()
        {
            return _currentCapacity > _maxCapacity ? Result<bool, Error>.Failure(Error.WithMessage(Message)) : Result<bool, Error>.Success(true);
        }
    }
}