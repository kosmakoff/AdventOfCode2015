using System.Collections.Generic;
using Sprache;

namespace Day07
{
    public static class LogicGatesGrammar
    {
        private static readonly Parser<BinaryOperation> BinaryOperator =
            Parse.String("AND").Return(BinaryOperation.And)
                .Or(Parse.String("OR").Return(BinaryOperation.Or))
                .Or(Parse.String("LSHIFT").Return(BinaryOperation.LeftShift))
                .Or(Parse.String("RSHIFT").Return(BinaryOperation.RightShift));

        private static readonly Parser<UnaryOperation> UnaryOperator =
            Parse.String("NOT").Return(UnaryOperation.Not);

        private static readonly Parser<ValueExpression> VariableValueExpression =
            from identifier in Parse.Identifier(Parse.Letter, Parse.Letter)
            select new VariableValueExpression(identifier);

        private static readonly Parser<ValueExpression> ConcreteValueExpression =
            from value in Parse.Number
            select new ConcreteValueExpression(ushort.Parse(value));

        private static readonly Parser<ValueExpression> ValueExpression =
            VariableValueExpression.Or(ConcreteValueExpression);

        private static readonly Parser<BinaryOperationExpression> BinaryOperationExpression =
            from leftOperand in ValueExpression.Token()
            from @operator in BinaryOperator.Token()
            from rightOperand in ValueExpression
            select new BinaryOperationExpression(leftOperand, @operator, rightOperand);

        private static readonly Parser<ILogicExpression> UnaryOperationExpression =
            from @operator in UnaryOperator.Token()
            from operand in ValueExpression
            select new UnaryOperationExpression(@operator, operand);

        private static readonly Parser<ILogicExpression> LogicExpression =
            UnaryOperationExpression
                .Or(BinaryOperationExpression)
                .Or(ValueExpression);

        private static readonly Parser<Instruction> Instruction =
            from logicExpression in LogicExpression.Token()
            from arrow in Parse.String("->").Token()
            from target in VariableValueExpression
            select new Instruction(logicExpression, (VariableValueExpression) target);

        public static readonly Parser<IEnumerable<Instruction>> Program =
            Instruction.DelimitedBy(Parse.LineEnd);
    }
}
