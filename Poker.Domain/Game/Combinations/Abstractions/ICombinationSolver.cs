using Poker.Shared;

namespace Poker.Domain.Game.Combinations.Abstractions
{
    public interface ICombinationSolver
    {
        Result<ICombination, Error> GetBestPossibleCombinationForHand(IHand hand);
    }
}