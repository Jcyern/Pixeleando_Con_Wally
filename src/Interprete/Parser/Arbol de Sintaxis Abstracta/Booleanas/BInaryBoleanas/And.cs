using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;

namespace AndNodo
{
    public class AndNode : BinaryExpression
    {
        public AndNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {

        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            //verifica si son de tipo bool 

            return base.CheckSemantic(ExpressionTypes.Bool);
        }


        public override object Evaluate()
        {
            return Convert.ToBoolean(LeftExpression!.Evaluate()) && Convert.ToBoolean(RightExpression!.Evaluate());
        }

    }

    public class AndNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new AndNode(Left, Operator, Right);

            return null;
        }
    }
}