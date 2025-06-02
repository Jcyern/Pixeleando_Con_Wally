

using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;

namespace NoIguales
{
    public class NotEqualsNode : BinaryExpression
    {
        public NotEqualsNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //verifica solo q sean del mismo tipo
            return base.CheckSemantic(tipo);
        }

        public override object Evaluate()
        {
            var left = LeftExpression!.Evaluate();
            System.Console.WriteLine($"NotEquals Left {left}");
            var right = RightExpression!.Evaluate();
            System.Console.WriteLine($"NotEquals Right {right} ");

            System.Console.WriteLine($"resultado Not equals {left != right}");

            return left!.ToString() != right!.ToString();
        }
    }


    public class NotEqualsNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new NotEqualsNode(Left, Operator, Right);

            return null;
        }
    }
}