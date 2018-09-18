using System;
using System.Collections.Generic;
using System.Text;

namespace Day09
{
    public class CityPair
    {
        public City City1 { get; }
        public City City2 { get; }

        public CityPair(City city1, City city2)
        {
            City1 = city1;
            City2 = city2;
        }

        private sealed class DefaultComparer : IEqualityComparer<CityPair>
        {
            public bool Equals(CityPair x, CityPair y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.City1.Name, y.City1.Name) && Equals(x.City2.Name, y.City2.Name) ||
                       string.Equals(x.City1.Name, y.City2.Name) && Equals(x.City2.Name, y.City1.Name);
            }

            public int GetHashCode(CityPair obj)
            {
                unchecked
                {
                    return (obj.City1 != null ? obj.City1.Name.GetHashCode() : 0) ^
                           (obj.City2 != null ? obj.City2.Name.GetHashCode() : 0);
                }
            }
        }

        public static IEqualityComparer<CityPair> DefaultComparerInstance { get; } = new DefaultComparer();
    }
}
