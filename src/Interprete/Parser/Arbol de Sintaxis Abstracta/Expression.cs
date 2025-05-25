using ExpressionesBinarias;
using ArbolSintaxisAbstracta;
using ExpressionesTipos;
using ExpressionesUnarias;

namespace Expresion
{


    public abstract class Expression : AstNode
    {
        public object? value;


        public ExpressionTypes type;
        public virtual object? Evaluate()
        {
            return null;
        }

        public virtual ExpressionTypes GetTipo()
        {
            if (this is UnaryExpression @unary)
            {
                return @unary.GetTipo();
            }


            else if (this is BinaryExpression expression)
            {
                // llamar a el GetTyoe de BinaryExpression
                System.Console.WriteLine(" Es binary Expression");
                return expression.GetTipo();
            }

            else
            {
                return ExpressionTypes.Invalid;
            }
        }


        public override bool CheckSemantic()
        {
            // semanticas de las expresiones Generales 

            var type = this.GetTipo();

            if (type == ExpressionTypes.Invalid || type == ExpressionTypes.Null)
            {
                return false;
            }
            else
                return true;
        }
    }


}