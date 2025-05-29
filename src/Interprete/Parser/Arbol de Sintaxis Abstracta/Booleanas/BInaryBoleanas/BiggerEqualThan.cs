
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

namespace MayorIgualQue
{
    public class BiggerEqualThanNode : BinaryExpression
    {
        public BiggerEqualThanNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //tiene q ser numeros 
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) >= Convert.ToInt32(RightExpression!.Evaluate());
        }
    }

}