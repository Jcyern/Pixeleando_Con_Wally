

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
            return LeftExpression!.Evaluate() != RightExpression!.Evaluate();
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