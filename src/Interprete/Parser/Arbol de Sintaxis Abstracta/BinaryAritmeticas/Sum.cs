using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;


namespace Suma
{
    public class SumaNode : BinaryExpression
    {
        public SumaNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            // la suma de dos numeros 
            return base.CheckSemantic(ExpressionTypes.Number);
        }


        public override object Evaluate()
        {

            return Convert.ToInt32(LeftExpression!.Evaluate()) + Convert.ToInt32(RightExpression!.Evaluate());

        }



    }


    public class SumaNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new SumaNode(Left, Operator, Right);

            return null;
        }
    }

}