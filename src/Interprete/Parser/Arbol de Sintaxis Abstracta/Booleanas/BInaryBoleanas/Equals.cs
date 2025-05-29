
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;

namespace Iguales
{
    public class EqualNode : BinaryExpression
    {
        //builder
        public EqualNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        #region  Semantica
        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            //solo verifica q sean del mismo tipo
            //y q no sean ambos  invalid 
            return base.CheckSemantic();
        }


        #endregion



        #region Evaluate
        public override object Evaluate()
        {
            return LeftExpression!.Evaluate() == RightExpression!.Evaluate();
        }
    }

    #endregion


    public class EqualNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new EqualNode(Left, Operator, Right);

            return null;
        }
    }
}