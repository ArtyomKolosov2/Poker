using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game.Units;
using Poker.Shared;

namespace Poker.Domain.BusinessRules
{
    public class CardCollectionUniquenessRule : IBusinessRule
    {
        private readonly IReadOnlyCollection<Card> _cards;
        private readonly Card _card;

        public CardCollectionUniquenessRule(IReadOnlyCollection<Card> cards, Card card)
        {
            _cards = cards;
            _card = card;
        }

        public string Message => "Card is already contained in collection.";
        
        public Result<bool, Error> IsViolated() => 
            _cards.Contains(_card) ? Result<bool, Error>.Failure(Error.WithMessage(Message)) : Result<bool, Error>.Success(true);
    }
}