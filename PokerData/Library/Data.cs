using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
    public static class Data
    {
        /// <summary>
        /// Writes 2,598,960 hand combinations (ordered population) to a the specified file path.
        /// </summary>
        /// <param name="deck">A deck of cards.</param>
        /// <param name="filePath">The path to the file that should be written to.</param>
        /// <param name="generator">An instance of a random number generator.</param>
        public static void SaveOrderedPopulation(Deck deck, string filePath, Random generator = null)
        {
            var hands = CreateOrderedPopulation(deck);
            hands = generator.Shuffle(hands);
            var iteration = 0;
            using (var file = new StreamWriter(filePath, false, Encoding.UTF8, 65536))
            {
                foreach (var hand in hands)
                {
                    if (iteration % 100000 == 0)
                    {
                        Console.Write($"\r{(float)iteration / 2598960.0 * 100.0:0.00}% completed.");
                    }

                    file.WriteLine(hand);
                    iteration++;
                }
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Writes 311,875,200 hand combinations (unordered population) to a the specified file path.
        /// </summary>
        /// <param name="deck">A deck of cards.</param>
        /// <param name="filePath">The path to the file that should be written to.</param>
        /// <param name="generator">An instance of a random number generator.</param>
        public static void SaveUnorderedPopulation(Deck deck, string filePath, Random generator = null)
        {
            var hands = CreateUnorderedPopulation(deck);
            hands = generator.Shuffle(hands);
            var iteration = 0;
            using (var file = new StreamWriter(filePath, false, Encoding.UTF8, 65536))
            {
                foreach (var hand in hands)
                {
                    if (iteration % 100000 == 0)
                    {
                        Console.Write($"\r{(float)iteration / 311875200.0 * 100.0:0.00}% completed.");
                    }

                    file.WriteLine(hand);
                    iteration++;
                }
            }
            Console.WriteLine("");
        }

        private static IEnumerable<Hand> CreateOrderedPopulation(Deck deck)
        {
            var cards = deck.Cards.Length;
            for (var first = 0; first < cards; first++)
            {
                for (var second = first + 1; second < cards; second++)
                {
                    for (var third = second + 1; third < cards; third++)
                    {
                        for (var fourth = third + 1; fourth < cards; fourth++)
                        {
                            for (var fifth = fourth + 1; fifth < cards; fifth++)
                            {
                                yield return new Hand(
                                    deck.Cards[first],
                                    deck.Cards[second],
                                    deck.Cards[third],
                                    deck.Cards[fourth],
                                    deck.Cards[fifth]);
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<Hand> CreateUnorderedPopulation(Deck deck)
        {
            var cards = deck.Cards.Length;
            for (var first = 0; first < cards; first++)
            {
                for (var second = 0; second < cards; second++)
                {
                    if (second == first)
                    {
                        continue;
                    }
                    for (var third = 0; third < cards; third++)
                    {
                        if (third == first || third == second)
                        {
                            continue;
                        }
                        for (var fourth = 0; fourth < cards; fourth++)
                        {
                            if (fourth == first || fourth == second || fourth == third)
                            {
                                continue;
                            }
                            for (var fifth = 0; fifth < cards; fifth++)
                            {
                                if (fifth == first || fifth == second || fifth == third || fifth == fourth)
                                {
                                    continue;
                                }

                                yield return new Hand(
                                    deck.Cards[first],
                                    deck.Cards[second],
                                    deck.Cards[third],
                                    deck.Cards[fourth],
                                    deck.Cards[fifth]);
                            }
                        }
                    }
                }
            }
        }
    }
}
