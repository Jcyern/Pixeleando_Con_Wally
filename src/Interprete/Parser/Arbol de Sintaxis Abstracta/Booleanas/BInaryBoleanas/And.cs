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
            //Evaluando and 
            var left = Convert.ToBoolean(LeftExpression!.Evaluate());
            System.Console.WriteLine($"Left And {left}");
            var right = Convert.ToBoolean(RightExpression!.Evaluate());
            System.Console.WriteLine($"Right And {right}");


            return left && right;
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