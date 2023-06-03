using Poker.Domain.Game;
using Poker.Domain.Game.Units;
using System.Collections.Generic;

namespace Poker.OmahaHoldem.Game
{
    public class OmahaHoldemHand : CardCollection, IHand
    {
        private const int HandCapacity = 4;

        public OmahaHoldemHand(IReadOnlyCollection<Card> cards) : base(cards, HandCapacity)
        {
        }
    }
}
