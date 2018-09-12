using System;
using System.Collections.Generic;

namespace Day03
{
    public struct Coords
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Coords operator +(Coords coords, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Coords(coords.X, coords.Y + 1);
                case Direction.Down:
                    return new Coords(coords.X, coords.Y - 1);
                case Direction.Left:
                    return new Coords(coords.X - 1, coords.Y);
                case Direction.Right:
                    return new Coords(coords.X + 1, coords.Y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        private sealed class XYEqualityComparer : IEqualityComparer<Coords>
        {
            public bool Equals(Coords x, Coords y)
            {
                return x.X == y.X && x.Y == y.Y;
            }

            public int GetHashCode(Coords obj)
            {
                unchecked
                {
                    return (obj.X * 397) ^ obj.Y;
                }
            }
        }

        public static IEqualityComparer<Coords> XYComparer { get; } = new XYEqualityComparer();
    }
}
