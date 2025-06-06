using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;
using Evalua;


namespace Division
{

    public class DivisionNode : BinaryExpression
    {
        public DivisionNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate(Evaluator? evaluator= null)
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) / Convert.ToInt32(RightExpression!.Evaluate());

        }


    }

    public class DivisionNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new DivisionNode(Left, Operator, Right);

            return null;
        }
    }
}