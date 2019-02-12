using System.Collections.Generic;

namespace Library
{
    public class Card : IEqualityComparer<Card>, IComparer<Card>
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public override string ToString() => $"{Rank} of {Suit}s";

        public byte GetRank => Rank == Rank.Ace ? (byte)1: (byte)Rank;

        public byte GetSuit => (byte)Suit;

        public bool PairsWith(Card card)
        {
            return CompareSuit(card) != 0 && CompareSuit(card) == 0;
        }

        public int CompareSuit(Card other)
        {
            return Suit.CompareTo(other.Suit);
        }

        public int CompareRank(Card other)
        {
            return Rank.CompareTo(other.Rank);
        }

        public int Compare(Card x, Card y)
        {
            var suit = x.CompareSuit(y);
            var rank = x.CompareRank(y);

            if (suit > 0)
                return 1;
            if (suit < 0)
                return -1;
            if (rank > 0)
                return 1;
            if (rank < 0)
                return -1;
            return 0;
        }

        public bool Equals(Card x, Card y)
        {
            return x.Suit == y.Suit && x.Rank == y.Rank;
        }

        public int GetHashCode(Card obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
