using System;
using System.Collections.Generic;
using Poker.Domain.Game.Combinations.Abstractions;
using Poker.FiveCardDraw.Game;
using Poker.OmahaHoldem.Game;
using Poker.Shared.Extensions;
using Poker.TexasHoldem.Game;

namespace Evolution.Poker.Solver
{
    public class Solver
    {
        public static string Process(string line)
        {
            var (deck, hands, game) = InputParser.Parse(line);
            var solver = (ICombinationSolver)(game switch
            {
                PokerGame.TexasHoldem => new TexasHoldemCombinationSolver(deck),
                PokerGame.FiveCardDraw => new FiveCardDrawCombinationSolver(),
                PokerGame.OmahaHoldem => new OmahaHoldemCombinationSolver(deck),
                _ => throw new ArgumentOutOfRangeException($"Game type {game} isn't supported!"),
            });

            var solverList = new List<ICombination>();
            foreach (var hand in hands)
                solver.GetBestPossibleCombinationForHand(hand).OnSuccess(x => solverList.Add(x.Data));

            return Output.GetOutput(solverList);
        }
    }
}
