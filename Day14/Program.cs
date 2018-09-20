using System;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 14");

            var input = File.ReadAllLines("Input.txt")
                .Select(ReindeerStats.Parse)
                .ToArray();

            const int rideDuration = 2503;

            var answer1 = CalculateAnswer1(input, rideDuration);
            var answer2 = CalculateAnswer2(input, rideDuration);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(ReindeerStats[] input, int elapsedSeconds)
        {
            return input
                .Select(stats => (Stats: stats, Distance: CalculateDistance(stats, elapsedSeconds)))
                .OrderByDescending(a => a.Distance)
                .First()
                .Distance;
        }

        private static int CalculateAnswer2(ReindeerStats[] input, int rideDuration)
        {
            var ride = input.Select(stats => new ReindeerState(stats)).ToArray();

            for (int i = 0; i < rideDuration; i++)
            {
                foreach (var reindeerState in ride)
                {
                    reindeerState.Advance();
                }

                var maxDistance = ride.Max(reindeerState => reindeerState.Distance);

                foreach (var reindeerState in ride.Where(reindeerState => reindeerState.Distance == maxDistance))
                {
                    reindeerState.GiveOnePoint();
                }
            }

            return ride.Max(reindeerState => reindeerState.AwardedPoints);
        }

        private static int CalculateDistance(ReindeerStats stats, int elapsedSeconds)
        {
            var fullCycleDuration = stats.MoveTime + stats.RestTime;
            var fullCyclesCount = elapsedSeconds / fullCycleDuration;

            var fullCyclesDuration = fullCycleDuration * fullCyclesCount;
            var fullCyclesDistance = fullCyclesCount * stats.Speed * stats.MoveTime;

            var remainingTime = elapsedSeconds - fullCyclesDuration;
            var remainingTimeMoving = Math.Min(stats.MoveTime, remainingTime);
            var remainingDistance = stats.Speed * remainingTimeMoving;

            return fullCyclesDistance + remainingDistance;
        }
    }
}
