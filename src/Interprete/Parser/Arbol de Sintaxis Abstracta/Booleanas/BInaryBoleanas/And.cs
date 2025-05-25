using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

namespace AndNode
{
    public class And : BinaryExpression
    {
        public And(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {

        }

        public override bool CheckSemantic(ExpressionTypes tipo )
        {
            return base.CheckSemantic(ExpressionTypes.Bool);
        }


        public override object Evaluate()
        {
            return Convert.ToBoolean(LeftExpression!.Evaluate()) && Convert.ToBoolean(RightExpression!.Evaluate());
        }

    }
}