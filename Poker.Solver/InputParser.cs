using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Game;
using Poker.Domain.Game.Units;
using Poker.FiveCardDraw.Game;
using Poker.OmahaHoldem.Game;
using Poker.TexasHoldem.Game;

namespace Evolution.Poker.Solver
{
    public static class InputParser
    {
        private const char Splitter = ' ';

        public static (IDeck, IEnumerable<IHand>, PokerGame) Parse(string input)
        {
            var strings = input.Split(Splitter);
            var (type, deckString, handsStrings) = (strings[0], strings[1], strings[2..]);

            var pokerGame = ParsePokerGame(type);
            var deck = ParseDeck(pokerGame, deckString);
            var hands = ParseHands(pokerGame, pokerGame switch
            {
                PokerGame.TexasHoldem or PokerGame.OmahaHoldem => handsStrings,
                _ => handsStrings.Prepend(deckString),
            });

            return (deck, hands, pokerGame);
        }

        private static PokerGame ParsePokerGame(string type) => type switch
        {
            "texas-holdem" => PokerGame.TexasHoldem,
            "omaha-holdem" => PokerGame.OmahaHoldem,
            "five-card-draw" => PokerGame.FiveCardDraw,
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Game type isn't recognized.")
        };

        private static IDeck ParseDeck(PokerGame pokerGame, string deckString) => pokerGame switch
        {
            PokerGame.TexasHoldem or PokerGame.OmahaHoldem => new FiveCardDeck(ParseCardsFromString(deckString)),
            _ => null,
        };

        private static IEnumerable<IHand> ParseHands(PokerGame pokerGame, IEnumerable<string> handsStrings)
        {
            var hands = handsStrings.Select(ParseCardsFromString).Select(cards => (IHand)(pokerGame switch
            {
                PokerGame.TexasHoldem => new TexasHoldemHand(cards),
                PokerGame.OmahaHoldem => new OmahaHoldemHand(cards),
                PokerGame.FiveCardDraw => new FiveCardDrawHand(cards),
                _ => throw new ArgumentOutOfRangeException(nameof(pokerGame), "Unsupported game type.")
            }));

            return hands.ToList();
        }

        private static IReadOnlyCollection<Card> ParseCardsFromString(string deckString) =>
            deckString.Select((c, index) => new { c, index })
                .GroupBy(x => x.index / 2)
                .Select(group => group.Select(elem => elem.c).ToArray())
                .Select(chars => new Card(ParseCardSuit(chars[1]), ParseCardValue(chars[0])))
                .ToList();

        private static RankEnum ParseCardValue(char cardValue) => cardValue switch
        {
            '2' => RankEnum.Two,
            '3' => RankEnum.Three,
            '4' => RankEnum.Four,
            '5' => RankEnum.Five,
            '6' => RankEnum.Six,
            '7' => RankEnum.Seven,
            '8' => RankEnum.Eight,
            '9' => RankEnum.Nine,
            'T' => RankEnum.Ten,
            'J' => RankEnum.Jack,
            'Q' => RankEnum.Queen,
            'K' => RankEnum.King,
            'A' => RankEnum.Ace,
            _ => throw new ArgumentOutOfRangeException(nameof(cardValue), "Card value isn't recognized.")
        };

        private static SuitEnum ParseCardSuit(char cardSuit) => cardSuit switch
        {
            'h' => SuitEnum.Heart,
            'd' => SuitEnum.Diamond,
            'c' => SuitEnum.Club,
            's' => SuitEnum.Spade,
            _ => throw new ArgumentOutOfRangeException(nameof(cardSuit), "Card value isn't recognized.")
        };
    }

    public enum PokerGame
    {
        Default = 0,
        TexasHoldem = 1,
        OmahaHoldem = 2,
        FiveCardDraw = 3,
    }
}