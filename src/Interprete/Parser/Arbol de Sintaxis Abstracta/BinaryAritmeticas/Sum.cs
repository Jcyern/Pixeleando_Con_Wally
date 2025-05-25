using ExpressionesBinarias;
using Expresion;


namespace SumaNode
{
    public class SumaParse : BinaryExpression
    {
        public SumaParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }



        public override object Evaluate()
        {

            return Convert.ToInt32(LeftExpression!.Evaluate()) + Convert.ToInt32(RightExpression!.Evaluate());

        }
    }

}