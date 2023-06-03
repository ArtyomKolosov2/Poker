using System.Collections.Generic;
using Poker.Domain.Game;
using Poker.Domain.Game.Units;

namespace Poker.TexasHoldem.Game
{
    public class TexasHoldemHand : CardCollection, IHand
    {
        private const int HandCapacity = 2;

        public TexasHoldemHand(IReadOnlyCollection<Card> cards) : base(cards, HandCapacity)
        {
        }
    }
}