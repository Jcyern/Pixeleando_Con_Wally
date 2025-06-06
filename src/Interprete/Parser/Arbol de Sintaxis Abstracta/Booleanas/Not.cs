
using Evalua;
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using ExpressionesUnarias;
using INodeCreador;
using TerminalesNode;

namespace NotNode
{
    public class NotNode : UnaryExpression
    {
        public NotNode(Expression expresion, Token Operator, (int, int) Location) : base(expresion, Operator, Location)
        {

        }

        public override ExpressionTypes GetTipo()
        {
            if (expresion != null)
            {
                if (expresion is BinaryExpression be)
                    return be.GetTipo();

                if (expresion is TerminalExpression te)
                    return te.GetTipo();
            }

            return ExpressionTypes.Invalid;
        }


        public override bool CheckSemantic(ExpressionTypes tipo)
        {

            //tiene q ser un booleano 
            return base.CheckSemantic(ExpressionTypes.Bool);
        }



        public override object? Evaluate(Evaluator? evaluator =  null)
        {
            return !Convert.ToBoolean(expresion!.Evaluate());
        }
    }


    public class NotNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left , Token Operator, Expression? Right)
        {
            if (Right != null)
                return new NotNode(Right, Operator , Operator.Pos);
            return null;
        }
    }

}