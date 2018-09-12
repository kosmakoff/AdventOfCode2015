using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day06
{
    class BitGrid
    {
        private readonly ushort _width;
        private readonly ushort _height;

        private readonly BitArray _buffer;

        public BitGrid(ushort width, ushort height)
        {
            _width = width;
            _height = height;
            var length = width * height;
            _buffer = new BitArray(length, false);
        }

        public void ChangeData(Coords from, Coords to, ChangeMethod changeMethod)
        {
            for (ushort x = from.X; x <= to.X; x++)
            for (ushort y = from.Y; y <= to.Y; y++)
            {
                var bufferIndex = y * _width + x;
                switch (changeMethod)
                {
                    case ChangeMethod.TurnOn:
                        _buffer[bufferIndex] = true;
                        break;
                    case ChangeMethod.TurnOff:
                        _buffer[bufferIndex] = false;
                        break;
                    case ChangeMethod.Toggle:
                        _buffer[bufferIndex] = !_buffer[bufferIndex];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(changeMethod), changeMethod, null);
                }
            }
        }

        public int GetLightsOnCount()
        {
            return _buffer.Cast<bool>().AsParallel().Count(b => b);
        }
    }
}
