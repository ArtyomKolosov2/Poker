using System;
using System.Collections;
using System.Collections.Generic;
using Poker.Domain.BusinessRules;
using Poker.Domain.Game.Units;
using Poker.Domain.ValueObjects;
using Poker.Shared;
using Poker.Shared.Extensions;

namespace Poker.Domain.Game
{
    public abstract class CardCollection : IReadOnlyCollection<Card>
    {
        private readonly PositiveNumber _capacity;
        private readonly List<Card> _cards = new();
        public int Count => _cards.Count;

        protected CardCollection(IReadOnlyCollection<Card> cards, PositiveNumber capacity)
        {
            _capacity = capacity;
            AddCardRange(cards).OnError(x => throw x.Error!.Exception!);
        }
        
        public IEnumerator<Card> GetEnumerator() => _cards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _cards.GetEnumerator();

        private Result<bool, Error> AddCardRange(IReadOnlyCollection<Card> cards)
        {
            try
            {
                var cardCapacityRule = new CardCapacityRule(_capacity, cards.Count);
                cardCapacityRule.IsViolated()
                    .OnError(x => throw new ArgumentException(x.Error!.Message));
            
                foreach (var card in cards)
                {
                    var uniquenessRule = new CardCollectionUniquenessRule(this, card);
                    uniquenessRule.IsViolated()
                        .OnError(x => throw new ArgumentException(x.Error!.Message))
                        .OnSuccess(_ => _cards.Add(card));
                }
            }
            catch (Exception e)
            {
                return Result<bool, Error>.Failure(Error.WithException(e));
            }
            
            return Result<bool, Error>.Success(true);
        }
    }
}