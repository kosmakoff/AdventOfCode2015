using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    public class SueData
    {
        public string Id { get; }
        public IDictionary<string, int> Data { get; }

        public SueData(string id, IDictionary<string, int> data)
        {
            Id = id;
            Data = data;
        }

        public static SueData Parse(string sueDataString)
        {
            var firstColonPosition = sueDataString.IndexOf(':');
            var id = sueDataString.Substring(4, firstColonPosition - 4);

            var remainingPart = sueDataString.Substring(firstColonPosition + 2)
                .Split(", ")
                .Select(x =>
                {
                    var parts = x.Split(": ");
                    var name = parts[0];
                    var count = int.Parse(parts[1]);
                    return (Name: name, Count: count);
                })
                .ToDictionary(a => a.Name, a => a.Count);

            return new SueData(id, remainingPart);
        }
    }
}
