using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;


namespace Resta
{
    public class RestaNode : BinaryExpression
    {
        public RestaNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }


        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) - Convert.ToInt32(RightExpression!.Evaluate());

        }

    }

    public class RestaNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new RestaNode(Left, Operator, Right);

            return null;
        }
    }
}