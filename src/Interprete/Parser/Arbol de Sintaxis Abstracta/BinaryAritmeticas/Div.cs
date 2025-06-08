using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;
using Evalua;
using Errores;


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
        public override ExpressionTypes GetTipo()
        {
            var tipo = base.GetTipo();

            if (tipo == ExpressionTypes.Number)
            {
                return ExpressionTypes.Number;
            }
            else
            {
                return tipo;
            }
        }

        public override object Evaluate(Evaluator? evaluator = null)
        {
            var right = Convert.ToInt32(RightExpression!.Evaluate());

            if (right != 0)
                return Convert.ToInt32(LeftExpression!.Evaluate()) / right;
            else
            {
                //nuevo error de division por cero 
                if (evaluator != null)
                    evaluator.AddError(new ZeroDivitionError((Operator.fila, Operator.columna + 1)));
                return 0;
            }

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