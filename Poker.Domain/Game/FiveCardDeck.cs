using System.Collections.Generic;
using Poker.Domain.Game.Units;

namespace Poker.Domain.Game
{
    public class FiveCardDeck : CardCollection, IDeck
    {
        private const int DeckCapacity = 5;
        
        public FiveCardDeck(IReadOnlyCollection<Card> cards) : base(cards, DeckCapacity)
        {
        }
    }
}