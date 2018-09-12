using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day06
{
    class IntGrid
    {
        private readonly ushort _width;
        private readonly ushort _height;

        private readonly short[] _buffer;

        public IntGrid(ushort width, ushort height)
        {
            _width = width;
            _height = height;
            var length = width * height;
            _buffer = new short[length];
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
                        _buffer[bufferIndex]++;
                        break;
                    case ChangeMethod.TurnOff:
                        if (_buffer[bufferIndex] > 0)
                            _buffer[bufferIndex]--;
                        break;
                    case ChangeMethod.Toggle:
                        _buffer[bufferIndex] += 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(changeMethod), changeMethod, null);
                }
            }
        }

        public int GetLightsTotalBrightness()
        {
            return _buffer.Sum(x => x);
        }
    }
}
