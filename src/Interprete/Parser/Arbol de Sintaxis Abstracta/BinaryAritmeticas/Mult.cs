using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;



namespace Multiplicacion
{
        public class MultiplicationNode : BinaryExpression
    {
        public MultiplicationNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo  )
        {
            //son expresiones numericas 
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) * Convert.ToInt32(RightExpression!.Evaluate());

        }

    }
}