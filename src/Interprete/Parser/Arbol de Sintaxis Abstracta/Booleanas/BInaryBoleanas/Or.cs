using ExpressionesTipos;
using Expresion;
using ExpressionesBinarias;



namespace OrNode
{
    public class OrNode : BinaryExpression
    {
        public OrNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }


        public override bool CheckSemantic(ExpressionTypes tipo  )
        {
            //verifica q sean de tipo bool 
            return base.CheckSemantic(ExpressionTypes.Bool);
        }


        public override object Evaluate()
        {
            return Convert.ToBoolean(LeftExpression!.Evaluate()) || Convert.ToBoolean(RightExpression!.Evaluate());
        }
    }
}