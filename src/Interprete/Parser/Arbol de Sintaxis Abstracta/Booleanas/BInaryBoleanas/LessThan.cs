

using Evalua;
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;

namespace Menorque
{
    public class LessThanNode : BinaryExpression
    {
        public LessThanNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //verifica q sean numeros 
            return base.CheckSemantic(ExpressionTypes.Number);
        }


        public override object Evaluate(Evaluator?  evaluator = null)
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) < Convert.ToInt32(RightExpression!.Evaluate());
        }
    }


    public class LessThanNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new LessThanNode(Left, Operator, Right);

            return null;
        }
    }
}