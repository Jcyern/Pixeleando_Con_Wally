using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;
using Evalua;

namespace Pow
{

    public class PowNode : BinaryExpression
    {
        public PowNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {

        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate(Evaluator? evaluator = null)
        {
            return Math.Pow(Convert.ToDouble(LeftExpression!.Evaluate()), Convert.ToDouble(RightExpression!.Evaluate()));
        }



    }

    public class PowNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new PowNode(Left, Operator, Right);

            return null;
        }
    }
}