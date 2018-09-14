using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Sprache;
using static Common.Utils;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 07");

            var input = File.ReadAllText("Input.txt");

            var commands = LogicGatesGrammar.Program.Parse(input).ToArray();
            var commandsDict = commands.ToDictionary(command => command.Target.Name, command => command.LogicExpression);

            var valuesDict = CalculateValuesDict(commandsDict);

            var answer1 = valuesDict["a"];

            // reset wire B
            commandsDict["b"] = new ConcreteValueExpression(answer1);
            valuesDict = CalculateValuesDict(commandsDict);
            var answer2 = valuesDict["a"];
            
            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static IDictionary<string, ushort> CalculateValuesDict(Dictionary<string, ILogicExpression> commandsDict)
        {
            var retVal = new Dictionary<string, ushort>();
            var commandWorkingCopy = new Dictionary<string, ILogicExpression>(commandsDict);

            bool addedItems;
            do
            {
                addedItems = false;
                var processedVariables = new HashSet<string>();

                foreach (var (variable, logicExpression) in commandWorkingCopy)
                {
                    switch (logicExpression)
                    {
                        case ValueExpression valueExpression
                            when TryGetValueExpressionValue(valueExpression, retVal, out var value):
                            processedVariables.Add(variable);
                            retVal.Add(variable, value);
                            break;
                        case UnaryOperationExpression unaryOperationExpression
                            when TryGetValueExpressionValue(unaryOperationExpression.Operand, retVal, out var value):
                            var unaryOperationResult = (ushort)(65535 - value);
                            processedVariables.Add(variable);
                            retVal.Add(variable, unaryOperationResult);
                            break;
                        case BinaryOperationExpression binaryOperationExpression
                            when TryGetValueExpressionValue(binaryOperationExpression.LeftOperand, retVal, out var leftValue) &&
                                 TryGetValueExpressionValue(binaryOperationExpression.RightOperand, retVal, out var rightValue):
                            ushort binaryOperationResult;
                            switch (binaryOperationExpression.Operation)
                            {
                                case BinaryOperation.And:
                                    binaryOperationResult = (ushort) (leftValue & rightValue);
                                    break;
                                case BinaryOperation.Or:
                                    binaryOperationResult = (ushort) (leftValue | rightValue);
                                    break;
                                case BinaryOperation.LeftShift:
                                    binaryOperationResult = (ushort) (leftValue << rightValue);
                                    break;
                                case BinaryOperation.RightShift:
                                    binaryOperationResult = (ushort) (leftValue >> rightValue);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            processedVariables.Add(variable);
                            retVal.Add(variable, binaryOperationResult);

                            break;
                    }
                }

                if (processedVariables.Any())
                {
                    addedItems = true;

                    foreach (var addedItem in processedVariables)
                    {
                        commandWorkingCopy.Remove(addedItem);
                    }
                }

            } while (addedItems);

            return retVal;
        }

        private static bool TryGetValueExpressionValue(ValueExpression valueExpression, IDictionary<string, ushort> valuesDict, out ushort value)
        {
            value = 0;

            switch (valueExpression)
            {
                case ConcreteValueExpression concreteValueExpression:
                    value = concreteValueExpression.Value;
                    return true;
                case VariableValueExpression variableValueExpression
                    when valuesDict.TryGetValue(variableValueExpression.Name, out var variableValue):
                    value = variableValue;
                    return true;
            }

            return false;
        }
    }
}
