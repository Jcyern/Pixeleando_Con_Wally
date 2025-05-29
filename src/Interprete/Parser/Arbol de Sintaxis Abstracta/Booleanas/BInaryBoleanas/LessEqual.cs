
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;

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

    public class LessEqualThanNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new LessEqualThanNode(Left, Operator, Right);

            return null;
        }
    }

}