using System;
using System.Collections;
using System.Linq;

namespace Common
{
    public class BitGrid
    {
        public ushort Width { get; }
        public ushort Height { get; }

        private readonly BitArray _buffer;

        public BitGrid(ushort width, ushort height, bool[] values = null)
        {
            Width = width;
            Height = height;
            var length = width * height;
            
            if (values == null)
                _buffer = new BitArray(length, false);
            else
            {
                if (values.Length != length)
                    throw new ArgumentException("Values length must match dimensions.");

                _buffer = new BitArray(values);
            }
        }

        public BitGrid(BitGrid bitGrid)
        {
            Width = bitGrid.Width;
            Height = bitGrid.Height;
            _buffer = new BitArray(bitGrid._buffer);
        }

        public bool this[int x, int y]
        {
            get
            {
                ThrowIfOutOfBounds(x, y);
                var bufferIndex = y * Width + x;

                return _buffer[bufferIndex];
            }
            set
            {
                ThrowIfOutOfBounds(x, y);
                var bufferIndex = y * Width + x;

                _buffer[bufferIndex] = value;
            }
        }

        private void ThrowIfOutOfBounds(int x, int y)
        {
            if (x < 0 || x >= Width)
                throw new IndexOutOfRangeException($"X must fall between {0} and {Width - 1} but was {x}");

            if (y < 0 || y >= Height)
                throw new IndexOutOfRangeException($"Y must fall between {0} and {Height - 1} but was {y}");
        }

        public int GetLightsOnCount()
        {
            return _buffer.Cast<bool>().AsParallel().Count(b => b);
        }
    }
}
