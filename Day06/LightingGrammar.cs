using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace Day06
{
    public static class LightingGrammar
    {
        private static readonly Parser<Coords> Coords =
            from firstNumber in Parse.Number
            from coma in Parse.String(",")
            from secondNumber in Parse.Number
            select new Coords(ushort.Parse(firstNumber), ushort.Parse(secondNumber));

        private static readonly Parser<ChangeMethod> ChangeMethod =
            Parse.String("turn on").Return(Day06.ChangeMethod.TurnOn)
                .Or(Parse.String("turn off").Return(Day06.ChangeMethod.TurnOff))
                .Or(Parse.String("toggle").Return(Day06.ChangeMethod.Toggle));

        private static readonly Parser<ChangeCommand> ChangeCommand =
            from changeMethod in ChangeMethod
            from coords1 in Coords.Token()
            from throughString in Parse.String("through").Token()
            from coords2 in Coords
            select new ChangeCommand(changeMethod, coords1, coords2);

        public static readonly Parser<IEnumerable<ChangeCommand>> Program =
            from commands in ChangeCommand.DelimitedBy(Parse.LineEnd)
            select commands;
    }
}
