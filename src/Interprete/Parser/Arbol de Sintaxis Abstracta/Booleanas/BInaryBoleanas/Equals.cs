

using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

namespace Iguales
{
    public class Equal : BinaryExpression
    {
        public Equal(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }


        public override bool CheckSemantic(ExpressionTypes tipo )
        {
            return base.CheckSemantic(ExpressionTypes.Bool);
        }
    }
}