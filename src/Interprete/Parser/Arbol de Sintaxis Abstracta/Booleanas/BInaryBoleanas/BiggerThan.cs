
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

namespace MayorQue
{
    public class BiggerThanNode : BinaryExpression
    {
        public BiggerThanNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }



        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //solo se pueden usar en expresiones numericas en este lenguaje 

            return base.CheckSemantic(ExpressionTypes.Number);
        }



        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) > Convert.ToInt32(RightExpression!.Evaluate());
        }
    }
}