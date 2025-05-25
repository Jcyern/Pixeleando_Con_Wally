using ExpressionesBinarias;
using Expresion;


namespace RestaNode
{
    public class RestaParse : BinaryExpression
    {
        public RestaParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }


        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) - Convert.ToInt32(RightExpression!.Evaluate());

        }


    }

}