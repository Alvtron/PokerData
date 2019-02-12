using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Hand
    {
        public Card[] Cards { get; private set; }

        public byte ClassNumber => (byte)Class;
        public Class Class
        {
            get
            {
                if (HasRoyalFlush)
                    return Class.RoyalFlush;
                if (HasStraightFlush)
                    return Class.StraightFlush;
                if (OfOneKind >= 4)
                    return Class.FourOfAKind;
                if (HasFullHouse)
                    return Class.FullHouse;
                if (HasFlush)
                    return Class.Flush;
                if (HasStraight)
                    return Class.Straight;
                if (OfOneKind >= 3)
                    return Class.ThreeOfAKind;
                if (Pairs >= 2)
                    return Class.TwoPairs;
                if (Pairs >= 1)
                    return Class.OnePair;
                return Class.NothingInHand;
            }
        }

        public Hand(Card firstCard, Card secondCard, Card thirdCard, Card fourthCard, Card fifthCard)
        {
            Cards = new Card[5]
            {
                firstCard, secondCard, thirdCard, fourthCard, fifthCard
            };
        }

        public string ToCSV() =>
            $"{Cards[0].SuitNumber},{Cards[0].RankNumber}," +
            $"{Cards[1].SuitNumber},{Cards[1].RankNumber}," +
            $"{Cards[2].SuitNumber},{Cards[2].RankNumber}," +
            $"{Cards[3].SuitNumber},{Cards[3].RankNumber}," +
            $"{Cards[4].SuitNumber},{Cards[4].RankNumber}," +
            $"{ClassNumber}";

        public int Pairs => Cards.GroupBy(c => c.Rank).Where(c => c.Count() > 1).Count();

        public int OfOneKind => Cards.GroupBy(c => c.Rank).Max(r => r.Count());

        public bool HasStraight => !Cards.OrderBy(c => c.Rank).Select(c => c.Rank).Select((i, j) => (byte)i - j).Distinct().Skip(1).Any();

        public bool HasFlush => Cards.GroupBy(c => c.Suit).Count() == 1;

        public bool HasFullHouse => Cards.GroupBy(c => c.Rank).Count() == 2 && Cards.GroupBy(c => c.Rank).Max(r => r.Count() == 3);

        public bool HasStraightFlush
        {
            get
            {
                var suit = Cards.First().Suit;
                return Cards.All(c => c.Suit == suit) && !Cards.OrderBy(c => c.Rank).Select(c => c.Rank).Select((i, j) => (byte)i - j).Distinct().Skip(1).Any();
            }
        }

        public bool HasRoyalFlush
        {
            get
            {
                var suit = Cards.First().Suit;
                return Cards.All(c => c.Suit == suit) && Cards.All(c => c.Rank == Rank.Ace || c.Rank == Rank.King || c.Rank == Rank.Queen || c.Rank == Rank.Jack || c.Rank == Rank.Ten);
            }
        }
    }
}
