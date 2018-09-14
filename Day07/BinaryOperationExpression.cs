namespace Day07
{
    public class BinaryOperationExpression : ILogicExpression
    {
        public BinaryOperationExpression(ValueExpression leftOperand, BinaryOperation operation, ValueExpression rightOperand)
        {
            Operation = operation;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public BinaryOperation Operation { get; }
        public ValueExpression LeftOperand { get; }
        public ValueExpression RightOperand { get; }
    }
}
