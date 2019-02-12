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
        public enum PopulationType
        {
            Ordered,
            All
        }
        private const int SizeUnordered = 311875200;
        private const int SizeOrdered = 2598960;
        private static readonly Dictionary<byte, double> ClassProbabilities = new Dictionary<byte, double>
        {
            { 0, 0.50117739 },
            { 1, 0.42256903 },
            { 2, 0.04753902 },
            { 3, 0.02112845 },
            { 4, 0.00392464 },
            { 5, 0.0019654 },
            { 6, 0.00144058 },
            { 7, 0.0002401 },
            { 8, 0.00001385 },
            { 9, 0.00000154 }
        };

        /// <summary>
        /// Creates ordered or unordered hand combinations and saves it as a SVG file with the provided file name.
        /// </summary>
        /// <param name="deck">A deck of cards.</param>
        /// <param name="filePath">The path to the file that should be written to.</param>
        /// <param name="populationType">Type of population.</param>
        /// <param name="generator">An instance of a random number generator.</param>
        public static void CreateCSV(Deck deck, string filePath, PopulationType populationType, Random generator = null)
        {
            switch (populationType)
            {
                case PopulationType.Ordered:
                    SaveDataToFile(filePath, CreateOrderedPopulation(deck, filePath, generator));
                    return;
                case PopulationType.All:
                    SaveDataToFile(filePath, CreateUnorderedPopulation(deck, filePath, generator));
                    return;
                default:
                    throw new ArgumentOutOfRangeException("Invalid population type", nameof(populationType));
            }
        }

        /// <summary>
        /// Creates 2,598,960 hand combinations (ordered population).
        /// </summary>
        /// <param name="deck">A deck of cards.</param>
        /// <param name="filePath">The path to the file that should be written to.</param>
        /// <param name="generator">An instance of a random number generator.</param>
        /// <returns>2,598,960 ordered hand combinations</returns>
        /// <exception cref="ArgumentNullException">Null is not allowed. - deck</exception>
        /// <exception cref="ArgumentException">Empty file path. - filePath</exception>
        public static IEnumerable<Hand> CreateOrderedPopulation(Deck deck, string filePath, Random generator = null)
        {
            if (deck == null)
            {
                throw new ArgumentNullException("Null is not allowed.", nameof(deck));
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Empty file path.", nameof(filePath));
            }

            generator = generator ?? new Random();

            return generator.Shuffle(CreateOrderedPopulation(deck));
        }

        /// <summary>
        /// Creates 311,875,200 hand combinations (unordered population).
        /// </summary>
        /// <param name="deck">A deck of cards.</param>
        /// <param name="filePath">The path to the file that should be written to.</param>
        /// <param name="generator">An instance of a random number generator.</param>
        /// <returns>2,598,960 unordered hand combinations</returns>
        /// <exception cref="ArgumentNullException">Null is not allowed. - deck</exception>
        /// <exception cref="ArgumentException">Empty file path. - filePath</exception>
        public static IEnumerable<Hand> CreateUnorderedPopulation(Deck deck, string filePath, Random generator = null)
        {
            if (deck == null)
            {
                throw new ArgumentNullException("Null is not allowed.", nameof(deck));
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Empty file path.", nameof(filePath));
            }

            generator = generator ?? new Random();

            return generator.Shuffle(CreateUnorderedPopulation(deck));
        }

        /// <summary>
        /// Creates an ordered population.
        /// </summary>
        /// <param name="deck">The deck.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates an unordered population.
        /// </summary>
        /// <param name="deck">The deck.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Saves the data to file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="sample">The sample.</param>
        private static void SaveDataToFile(string filePath, IEnumerable<Hand> sample)
        {
            var iteration = 0;
            using (var file = new StreamWriter(filePath, false, Encoding.UTF8, 65536))
            {
                foreach (var hand in sample)
                {
                    if (iteration % 100000 == 0)
                    {
                        Console.Write($"\r{(float)iteration / 2598960.0 * 100.0:0.00}% completed.");
                    }

                    file.WriteLine(hand.ToCSV());
                    iteration++;
                }
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Creates a stratified sample.
        /// </summary>
        /// <param name="hands">The hands.</param>
        /// <param name="sampleSize">Size of the sample.</param>
        /// <returns></returns>
        private static IEnumerable<Hand> CreateStratifiedSample(IEnumerable<Hand> hands, int sampleSize)
        {
            var classSampleSize = new Dictionary<byte, int>();
            foreach (var probability in ClassProbabilities)
            {
                classSampleSize.Add(probability.Key, (int)(sampleSize * probability.Value));
            }

            foreach (var classGroups in hands.GroupBy(h => h.Class))
            {
                foreach (var hand in classGroups)
                    yield return hand;
            }
        }
    }
}
