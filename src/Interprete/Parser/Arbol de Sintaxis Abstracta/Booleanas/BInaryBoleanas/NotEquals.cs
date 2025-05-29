

using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

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
            return  LeftExpression!.Evaluate() != RightExpression!.Evaluate();
        }
    }
}