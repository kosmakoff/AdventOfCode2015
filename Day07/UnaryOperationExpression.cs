namespace Day07
{
    public class UnaryOperationExpression : ILogicExpression
    {
        public UnaryOperationExpression(UnaryOperation operation, ValueExpression operand)
        {
            Operation = operation;
            Operand = operand;
        }

        public UnaryOperation Operation { get; }
        public ValueExpression Operand { get; }
    }
}
