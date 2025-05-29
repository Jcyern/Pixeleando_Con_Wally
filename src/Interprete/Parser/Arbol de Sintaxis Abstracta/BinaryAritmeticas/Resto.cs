using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;

namespace Resto
{
    

    public class RestoNode : BinaryExpression
    {
        public RestoNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo )
        {
            return base.CheckSemantic(ExpressionTypes.Number);
        }
        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.value) % Convert.ToInt32(RightExpression!.value);
        }

    }

}