using Library;
using System;
using System.Diagnostics;

namespace Console
{

    class Program
    {
        static void Main(string[] args)
        {
            var royalFlush = new Hand(
                new Card { Suit = Suit.Spade, Rank = Rank.Ace },
                new Card { Suit = Suit.Spade, Rank = Rank.Ten },
                new Card { Suit = Suit.Spade, Rank = Rank.Queen },
                new Card { Suit = Suit.Spade, Rank = Rank.King },
                new Card { Suit = Suit.Spade, Rank = Rank.Jack });

            var straightFlush = new Hand(
                new Card { Suit = Suit.Club, Rank = Rank.Jack },
                new Card { Suit = Suit.Club, Rank = Rank.Nine },
                new Card { Suit = Suit.Club, Rank = Rank.Ten },
                new Card { Suit = Suit.Club, Rank = Rank.Eight },
                new Card { Suit = Suit.Club, Rank = Rank.Seven });

            var fourOfAKind = new Hand(
                new Card { Suit = Suit.Club, Rank = Rank.Five },
                new Card { Suit = Suit.Heart, Rank = Rank.Five },
                new Card { Suit = Suit.Spade, Rank = Rank.Five },
                new Card { Suit = Suit.Diamond, Rank = Rank.Five },
                new Card { Suit = Suit.Diamond, Rank = Rank.Two });

            var fullHouse = new Hand(
                new Card { Suit = Suit.Spade, Rank = Rank.Six },
                new Card { Suit = Suit.Heart, Rank = Rank.Six },
                new Card { Suit = Suit.Diamond, Rank = Rank.Six },
                new Card { Suit = Suit.Club, Rank = Rank.King },
                new Card { Suit = Suit.Heart, Rank = Rank.King });

            var flush = new Hand(
                new Card { Suit = Suit.Diamond, Rank = Rank.Jack },
                new Card { Suit = Suit.Diamond, Rank = Rank.Nine },
                new Card { Suit = Suit.Diamond, Rank = Rank.Eight },
                new Card { Suit = Suit.Diamond, Rank = Rank.Four },
                new Card { Suit = Suit.Diamond, Rank = Rank.Three });

            var straight = new Hand(
                new Card { Suit = Suit.Diamond, Rank = Rank.Ten },
                new Card { Suit = Suit.Spade, Rank = Rank.Nine },
                new Card { Suit = Suit.Heart, Rank = Rank.Eight },
                new Card { Suit = Suit.Diamond, Rank = Rank.Seven },
                new Card { Suit = Suit.Club, Rank = Rank.Six });

            var threeOfAKind = new Hand(
                new Card { Suit = Suit.Club, Rank = Rank.Queen },
                new Card { Suit = Suit.Spade, Rank = Rank.Queen },
                new Card { Suit = Suit.Heart, Rank = Rank.Queen },
                new Card { Suit = Suit.Heart, Rank = Rank.Nine },
                new Card { Suit = Suit.Spade, Rank = Rank.Two });

            var twoPair = new Hand(
                new Card { Suit = Suit.Heart, Rank = Rank.Jack },
                new Card { Suit = Suit.Spade, Rank = Rank.Jack },
                new Card { Suit = Suit.Club, Rank = Rank.Three },
                new Card { Suit = Suit.Spade, Rank = Rank.Three },
                new Card { Suit = Suit.Heart, Rank = Rank.Two });

            var onePair = new Hand(
                new Card { Suit = Suit.Spade, Rank = Rank.Ten },
                new Card { Suit = Suit.Heart, Rank = Rank.Ten },
                new Card { Suit = Suit.Spade, Rank = Rank.Eight },
                new Card { Suit = Suit.Heart, Rank = Rank.Seven },
                new Card { Suit = Suit.Club, Rank = Rank.Four });

            var nothing = new Hand(
                new Card { Suit = Suit.Diamond, Rank = Rank.King },
                new Card { Suit = Suit.Diamond, Rank = Rank.Queen },
                new Card { Suit = Suit.Spade, Rank = Rank.Seven },
                new Card { Suit = Suit.Spade, Rank = Rank.Four },
                new Card { Suit = Suit.Heart, Rank = Rank.Three });

            var random = new Random(1);
            var deck = new Deck();

            var stopwatch = Stopwatch.StartNew();
            System.Console.WriteLine($"Writing {2598960:0 000 000} ordered hands to file...");
            Data.CreateCSV(deck, "poker.ordered.csv", Data.PopulationType.Ordered, random);
            stopwatch.Stop();
            System.Console.WriteLine($"Finished after {stopwatch.ElapsedMilliseconds / 1000.0:0.00} seconds.");

            System.Console.WriteLine($"Writing {311875200:000 000 000} unordered hands to file...");
            System.Console.WriteLine($"Estimated completion time is {stopwatch.ElapsedMilliseconds / 1000.0 / 60 * 120:0.00} minutes.");
            stopwatch.Restart();
            Data.CreateCSV(deck, "poker.unordered.csv", Data.PopulationType.All, random);
            stopwatch.Stop();
            System.Console.WriteLine($"Finished after {stopwatch.ElapsedMilliseconds / 1000.0:0.00} seconds ({stopwatch.ElapsedMilliseconds / 1000.0 / 60:0.00} minutes).");
        }
    }
}
