namespace Day07
{
    public class Instruction
    {
        public Instruction(ILogicExpression logicExpression, VariableValueExpression target)
        {
            LogicExpression = logicExpression;
            Target = target;
        }

        public ILogicExpression LogicExpression { get; }
        public VariableValueExpression Target { get; }
    }
}
