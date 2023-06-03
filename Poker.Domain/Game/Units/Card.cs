namespace Poker.Domain.Game.Units
{
    public readonly struct Card
    {
        public Suit Suit { get; }

        public Rank Rank { get; }

        public Card(SuitEnum suit, RankEnum value)
        {
            Suit = new Suit(suit);
            Rank = new Rank(value);
        }

        public override string ToString() => $"{Rank}{Suit}";
    }
}