using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;


namespace Suma
{
    public class SumaNode : BinaryExpression
    {
        public SumaNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo )
        {
            // la suma de dos numeros 
            return base.CheckSemantic(ExpressionTypes.Number);
        }



        public override object Evaluate()
        {

            return Convert.ToInt32(LeftExpression!.Evaluate()) + Convert.ToInt32(RightExpression!.Evaluate());

        }
    }

}