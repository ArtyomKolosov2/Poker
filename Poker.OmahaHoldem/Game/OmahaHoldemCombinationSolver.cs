using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game;
using Poker.Domain.Game.Combinations;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Shared;
using Poker.Shared.Extensions;

namespace Poker.OmahaHoldem.Game
{
    public class OmahaHoldemCombinationSolver : ICombinationSolver
    {
        private readonly IDeck _deck;

        public OmahaHoldemCombinationSolver(IDeck deck)
        {
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }

        public Result<ICombination, Error> GetBestPossibleCombinationForHand(IHand hand)
        {
            var listOfCombinations = new List<ICombination>();
            var deckPossibleCombinations = _deck.GetCombinations(3);
            var handPossibleCombinations = hand.GetCombinations(2);
            foreach (var handCombination in handPossibleCombinations)
                foreach (var deckCombination in deckPossibleCombinations)
                    CombinationHelper.GetStrongestCombinationForCards(deckCombination.Concat(handCombination).ToArray(), hand).OnSuccess(x => listOfCombinations.Add(x.Data));

            return Result<ICombination, Error>.Success(listOfCombinations.Max());
        }
    }
}
