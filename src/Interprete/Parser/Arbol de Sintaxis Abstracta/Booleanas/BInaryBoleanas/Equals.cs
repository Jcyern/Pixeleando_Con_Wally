
using Evalua;
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
        public override object Evaluate(Evaluator? evaluator = null)
        {
            var left = LeftExpression!.Evaluate();
            System.Console.WriteLine($"Equals Left {left}  type {left!.GetType()} ");
            var right = RightExpression!.Evaluate();
            System.Console.WriteLine($"Equals Right {right} type {right!.GetType()}");

            //en ese punto se suponen q son del mismo tipo 
            //coger el tipo de uno convertirlos y luego apliar ==
            if (left!.GetType() == typeof(int))
            {
                return Convert.ToInt32(left) == Convert.ToInt32(right);
            }
            //pq el true y el false lo q coge como  string el object  , y en caso de q la exp devuelv un bool en el evaluate son otrs 20 pesos 
            else if (left!.GetType() == typeof(bool))
            {

                return Convert.ToBoolean(left) == Convert.ToBoolean(right);
            }
            else
            {
                //tiene q ser string
                return left.ToString() == right!.ToString();
            }
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