using ExpressionesBinarias;
using Expresion;

namespace RestoNode
{
    

    public class RestoParse : BinaryExpression
    {
        public RestoParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.value) % Convert.ToInt32(RightExpression!.value);
        }

    }

}