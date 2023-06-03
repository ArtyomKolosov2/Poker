using Poker.Shared;

namespace Poker.Domain.BusinessRules
{
    public interface IBusinessRule
    {
        string Message { get; }
        
        Result<bool, Error> IsViolated();
    }
}