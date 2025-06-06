using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;
using Evalua;



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

        public override object Evaluate(Evaluator? evaluator = null)
        {
            var left = Convert.ToInt32(LeftExpression!.Evaluate());
            System.Console.WriteLine($"Mult Left {left}");
            var right = Convert.ToInt32(RightExpression!.Evaluate());
            System.Console.WriteLine($"Mult Right {right}");

            return left * right;

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