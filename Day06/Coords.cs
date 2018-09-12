namespace Day06
{
    public struct Coords
    {
        public ushort X { get; set; }
        public ushort Y { get; set; }

        public Coords(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }
    }
}
