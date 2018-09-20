using System;
using System.Text.RegularExpressions;

namespace Day14
{
    public class ReindeerStats
    {
        private static readonly Regex StatsStringRegex = new Regex(@"^(?<name>\w+) can fly (?<speed>\d+) km/s for (?<moveTime>\d+) seconds, but then must rest for (?<restTime>\d+) seconds?\.$", RegexOptions.Compiled);

        public ReindeerStats(string name, int speed, int moveTime, int restTime)
        {
            Name = name;
            Speed = speed;
            MoveTime = moveTime;
            RestTime = restTime;
        }

        public string Name { get; }
        public int Speed { get; }
        public int MoveTime { get; }
        public int RestTime { get; }

        public static ReindeerStats Parse(string reindeerStatsString)
        {
            var match = StatsStringRegex.Match(reindeerStatsString);
            if (!match.Success)
                throw new Exception($"Failed to parse string '{reindeerStatsString}'");

            var name = match.Groups["name"].Value;
            var speed = int.Parse(match.Groups["speed"].Value);
            var moveTime = int.Parse(match.Groups["moveTime"].Value);
            var restTime = int.Parse(match.Groups["restTime"].Value);

            return new ReindeerStats(name, speed, moveTime, restTime);
        }
    }
}