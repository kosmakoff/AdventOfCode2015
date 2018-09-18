using System.Collections.Generic;

namespace Day09
{
    public class City
    {
        public City(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"City of {Name}";
        }

        private sealed class NameEqualityComparer : IEqualityComparer<City>
        {
            public bool Equals(City x, City y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Name, y.Name);
            }

            public int GetHashCode(City obj)
            {
                return (obj.Name != null ? obj.Name.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<City> NameComparer { get; } = new NameEqualityComparer();
    }
}
