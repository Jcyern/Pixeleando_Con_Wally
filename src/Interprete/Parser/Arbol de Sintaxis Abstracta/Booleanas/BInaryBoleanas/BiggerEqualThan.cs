
using Evalua;
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;

namespace MayorIgualQue
{
    public class BiggerEqualThanNode : BinaryExpression
    {
        public BiggerEqualThanNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //tiene q ser numeros 
            return base.CheckSemantic(ExpressionTypes.Number);
        }

        public override object Evaluate(Evaluator? evaluator = null)
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) >= Convert.ToInt32(RightExpression!.Evaluate());
        }

        public override ExpressionTypes GetTipo()
        {
            //si opera con numeros devuelve
            var tipo = base.GetTipo();

            if (tipo == ExpressionTypes.Number)
            {
                return ExpressionTypes.Bool;
            }
            else
            {
                return tipo;
            }
        }
    }


    public class BiggerEqualThanNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new BiggerEqualThanNode(Left, Operator, Right);

            return null;
        }
    }

}