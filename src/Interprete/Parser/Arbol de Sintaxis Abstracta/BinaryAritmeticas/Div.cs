using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;


namespace DivisionNode
{

    public class DivisionParse : BinaryExpression
    {
        public DivisionParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) / Convert.ToInt32(RightExpression!.Evaluate());

        }


    }
}