using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;

namespace Pow
{

    public class PowNode : BinaryExpression
    {
        public PowNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {

        }

        public override bool CheckSemantic(ExpressionTypes tipo )
        {
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate()
        {
            return Math.Pow(Convert.ToDouble(LeftExpression!.Evaluate()), Convert.ToDouble(RightExpression!.Evaluate()));
        }



    }
}