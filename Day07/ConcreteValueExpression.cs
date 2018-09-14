namespace Day07
{
    public class ConcreteValueExpression : ValueExpression
    {
        public ConcreteValueExpression(ushort value)
        {
            Value = value;
        }

        public ushort Value { get; }
    }
}
