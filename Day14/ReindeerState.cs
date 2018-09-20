using System;

namespace Day14
{
    public class ReindeerState
    {
        private ReindeerAction _action = ReindeerAction.Moving;
        private int _currentActionTimeLeft;

        public ReindeerState(ReindeerStats stats)
        {
            Stats = stats;

            _currentActionTimeLeft = Stats.MoveTime;
        }

        public ReindeerStats Stats { get; }

        public int Distance { get; private set; } = 0;
        
        public int AwardedPoints { get; private set; } = 0;

        public void Advance()
        {
            if (_action == ReindeerAction.Moving)
                Distance += Stats.Speed;

            _currentActionTimeLeft--;

            if (_currentActionTimeLeft == 0)
            {
                switch (_action)
                {
                    case ReindeerAction.Moving:
                        _action = ReindeerAction.Resting;
                        _currentActionTimeLeft = Stats.RestTime;
                        break;
                    case ReindeerAction.Resting:
                        _action = ReindeerAction.Moving;
                        _currentActionTimeLeft = Stats.MoveTime;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void GiveOnePoint()
        {
            AwardedPoints++;
        }

        private enum ReindeerAction
        {
            Moving,
            Resting
        }
    }
}