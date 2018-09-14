namespace Day07
{
    public class VariableValueExpression : ValueExpression
    {
        public VariableValueExpression(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
