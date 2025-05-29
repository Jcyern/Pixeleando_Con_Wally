

using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

namespace Menorque
{
    public class LessThanNode : BinaryExpression
    {
        public LessThanNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //verifica q sean numeros 
            return base.CheckSemantic(ExpressionTypes.Number);
        }


        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) < Convert.ToInt32(RightExpression!.Evaluate());
        }
    }
}