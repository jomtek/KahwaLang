using Kahwa.Lexing;
using Kahwa.Parsing.AST;
using Kahwa.Parsing.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Algorithms
{
    public class ShuntingYard
    {
        private static void FoldLastOperands(ref List<IExpr> operands, Token op)
        {
            IExpr leftOperand = operands[^2];
            IExpr rightOperand = operands[^1];

            IExpr operation = new InfixOp(leftOperand, rightOperand, op);

            operands.RemoveAt(operands.Count - 1);
            operands.RemoveAt(operands.Count - 1);

            operands.Add(operation);
        }

        public static IExpr Go(List<IExpr> operands, List<Token> operators)
        {
            var operatorPrecedences = new Dictionary<TokenType, int>()
            {
                [TokenType.DOT] = 8,
                [TokenType.CAST] = 7,
                [TokenType.POWER] = 6,
                [TokenType.DIVIDE] = 5,
                [TokenType.MULTIPLY] = 5,
                [TokenType.MODULO] = 5,
                [TokenType.PLUS] = 4,
                [TokenType.MINUS] = 4,
                [TokenType.GREATER] = 3,
                [TokenType.LESS] = 3,
                [TokenType.GREATER_EQ] = 3,
                [TokenType.LESS_EQ] = 3,
                [TokenType.EQ] = 2,
                [TokenType.NOT_EQ] = 2,
                [TokenType.IN] = 2,
                [TokenType.BOOLEAN_AND] = 1,
                [TokenType.BOOLEAN_OR] = 1
            };

            var operandStack = new List<IExpr>();
            var opStack = new List<Token>();

            int operatorIndex = 0;
            int operandIndex = 0;

            bool wasLastOperand = false;

            while (operandIndex <= operands.Count - 1)
            {
                if (!wasLastOperand)
                {
                    IExpr operand = operands[operandIndex];
                    operandStack.Add(operand);
                    operandIndex++;
                    wasLastOperand = true;
                }
                else
                {
                    Token currentOp = operators[operatorIndex];

                    if (opStack.Count > 0)
                    {
                        var stackCopy = new Token[opStack.Count];
                        opStack.CopyTo(stackCopy);

                        foreach (Token op in stackCopy.Reverse())
                        {
                            if (operatorPrecedences[op.Type] >= operatorPrecedences[currentOp.Type])
                            {
                                FoldLastOperands(ref operandStack, op);
                                opStack.RemoveAt(opStack.Count - 1);
                            }
                        }
                    }

                    opStack.Add(currentOp);
                    operatorIndex++;
                    wasLastOperand = false;
                }
            }

            opStack.Reverse();
            foreach (Token op in opStack)
            {
                FoldLastOperands(ref operandStack, op);
            }

            return operandStack[0];
        }
    }
}
