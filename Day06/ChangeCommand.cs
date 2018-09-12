using System;
using System.Text;

namespace Day06
{
    public class ChangeCommand
    {
        public ChangeMethod ChangeMethod { get; }
        public Coords CoordsFrom { get; }
        public Coords CoordsTo { get; }

        public ChangeCommand(ChangeMethod changeMethod, Coords coordsFrom, Coords coordsTo)
        {
            ChangeMethod = changeMethod;
            CoordsFrom = coordsFrom;
            CoordsTo = coordsTo;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            switch (ChangeMethod)
            {
                case ChangeMethod.TurnOn:
                    sb.Append("+ ");
                    break;
                case ChangeMethod.TurnOff:
                    sb.Append("- ");
                    break;
                case ChangeMethod.Toggle:
                    sb.Append("* ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            sb.Append(CoordsFrom);
            sb.Append(" => ");
            sb.Append(CoordsTo);

            return sb.ToString();
        }
    }
}