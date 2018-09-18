using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Extensions;
using static Common.Utils;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 09");

            var input = File.ReadAllLines("Input.txt");

            var routes = input.Select(ParseRoute).ToArray();

            var answer1 = CalculateAnswer1(routes);
            var answer2 = CalculateAnswer2(routes);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(Route[] routes)
        {
            var routesDict = routes.ToDictionary(route => new CityPair(route.A, route.B), route => route.Distance,
                CityPair.DefaultComparerInstance);

            var separateCities = routes
                .SelectMany(route => new[] {route.A, route.B}).Distinct(City.NameComparer)
                .ToArray();

            var citiesPermutations = separateCities.Permutations();

            var minimumDistanceJourney = citiesPermutations
                .Select(journey => (Journey: journey, Distance: journey.Pairwise()
                    .Select(pair => new CityPair(pair[0], pair[1]))
                    .Select(pair => routesDict[pair])
                    .Sum()))
                .OrderBy(a => a.Distance)
                .First();

            return minimumDistanceJourney.Distance;
        }

        private static int CalculateAnswer2(Route[] routes)
        {
            var routesDict = routes.ToDictionary(route => new CityPair(route.A, route.B), route => route.Distance,
                CityPair.DefaultComparerInstance);

            var separateCities = routes
                .SelectMany(route => new[] {route.A, route.B}).Distinct(City.NameComparer)
                .ToArray();

            var citiesPermutations = separateCities.Permutations();

            var maximumDistanceJourney = citiesPermutations
                .Select(journey => (Journey: journey, Distance: journey.Pairwise()
                    .Select(pair => new CityPair(pair[0], pair[1]))
                    .Select(pair => routesDict[pair])
                    .Sum()))
                .OrderByDescending(a => a.Distance)
                .First();

            return maximumDistanceJourney.Distance;
        }

        private static readonly Regex RouteRegex = new Regex(@"^(?<cityA>\w+) to (?<cityB>\w+) = (?<distance>\d+)$", RegexOptions.Compiled);

        private static Route ParseRoute(string routeString)
        {
            var match = RouteRegex.Match(routeString);
            var cityA = new City(match.Groups["cityA"].Value);
            var cityB = new City(match.Groups["cityB"].Value);
            var distance = int.Parse(match.Groups["distance"].Value);

            return new Route(cityA, cityB, distance);
        }
    }
}
