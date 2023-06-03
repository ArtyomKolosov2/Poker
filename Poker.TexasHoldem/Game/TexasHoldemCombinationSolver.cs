using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game;
using Poker.Domain.Game.Combinations;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.Shared;
using Poker.Shared.Extensions;

namespace Poker.TexasHoldem.Game
{
    public class TexasHoldemCombinationSolver : ICombinationSolver
    {
        private readonly IDeck _deck;

        public TexasHoldemCombinationSolver(IDeck deck)
        {
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }
        
        public Result<ICombination, Error> GetBestPossibleCombinationForHand(IHand hand)
        {
            var listOfCombinations = new List<ICombination>();
            var deckPossibleCombinations = _deck.GetCombinations(3);
            foreach (var combination in deckPossibleCombinations)
                CombinationHelper.GetStrongestCombinationForCards(combination.Concat(hand).ToList(), hand).OnSuccess(x => listOfCombinations.Add(x.Data));

            return Result<ICombination, Error>.Success(listOfCombinations.Max());
        }
    }
}