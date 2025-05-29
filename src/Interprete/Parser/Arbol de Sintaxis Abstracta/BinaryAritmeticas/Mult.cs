using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;



namespace Multiplicacion
{
    public class MultiplicationNode : BinaryExpression
    {
        public MultiplicationNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            //son expresiones numericas 
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) * Convert.ToInt32(RightExpression!.Evaluate());

        }

    }


    public class MultiplicacionNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new MultiplicationNode(Left, Operator, Right);

            return null;
        }
    }
}