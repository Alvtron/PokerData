using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Deck
    {
        public Card[] Cards { get; private set; }

        public Deck()
        {
            Cards = CreateDeck().ToArray();
        }

        private static IEnumerable<Card> CreateDeck()
        {
            for (var suit = 1; suit <= 4; suit++)
            {
                for (var rank = 2; rank <= 14; rank++)
                {
                    yield return new Card
                    {
                        Suit = (Suit)suit,
                        Rank = (Rank)rank
                    };
                }
            }
        }

        public void Shuffle(Random generator = null)
        {
            generator.Shuffle(Cards);
        }
    }
}
