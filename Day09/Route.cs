namespace Day09
{
    public class Route
    {
        public City A { get; }
        public City B { get; }
        public int Distance { get; }

        public Route(City a, City b, int distance)
        {
            A = a;
            B = b;
            Distance = distance;
        }

        public override string ToString()
        {
            return $"{A.Name} to {B.Name} = {Distance}";
        }
    }
}
