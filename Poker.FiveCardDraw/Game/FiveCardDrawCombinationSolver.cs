using Poker.Domain.Game;
using Poker.Domain.Game.Combinations;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Shared;

namespace Poker.FiveCardDraw.Game
{
    public class FiveCardDrawCombinationSolver : ICombinationSolver
    {
        public FiveCardDrawCombinationSolver()
        {
        }

        public Result<ICombination, Error> GetBestPossibleCombinationForHand(IHand hand) => CombinationHelper.GetStrongestCombinationForCards(hand, hand);
    }
}
