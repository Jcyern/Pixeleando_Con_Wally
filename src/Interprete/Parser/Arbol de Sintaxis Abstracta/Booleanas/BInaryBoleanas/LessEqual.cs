
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

namespace MenorIgualque
{
    public class LessEqualThanNode : BinaryExpression
    {
        public LessEqualThanNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //verificar q se vayan a comparar dos numeros 
            return base.CheckSemantic(ExpressionTypes.Number);
        }


        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) <= Convert.ToInt32(RightExpression!.Evaluate());
        }
    }

}