using Poker.Domain.Game;
using Poker.Domain.Game.Units;
using System.Collections.Generic;

namespace Poker.FiveCardDraw.Game
{
    public class FiveCardDrawHand : CardCollection, IHand
    {
        private const int HandCapacity = 5;

        public FiveCardDrawHand(IReadOnlyCollection<Card> cards) : base(cards, HandCapacity)
        {
        }
    }
}