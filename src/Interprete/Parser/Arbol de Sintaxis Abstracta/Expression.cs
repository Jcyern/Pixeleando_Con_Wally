using ExpressionesBinarias;
using ArbolSintaxisAbstracta;
using ExpressionesTipos;
using ExpressionesUnarias;
using TerminalesNode;
namespace Expresion
{


    public abstract class Expression : AstNode 
    {
        public object? value;


        public ExpressionTypes type = ExpressionTypes.Null;
        public override   object? Evaluate()
        {
            return null;
        }

        public override ExpressionTypes GetTipo()
        {
            if (this is TerminalExpression te)
            {
                System.Console.WriteLine("Es terminal expression");
                return te.GetTipo();
            }


            else if (this is BinaryExpression expression)
            {
                // llamar a el GetTyoe de BinaryExpression
                System.Console.WriteLine(" Es binary Expression");
                return expression.GetTipo();
            }

            else if (this is UnaryExpression unary)
            {
                System.Console.WriteLine("Es Unary Expression");
                return unary.GetTipo();
            }

            else
            {
                return ExpressionTypes.Invalid;
            }
        }


        public override bool CheckSemantic(ExpressionTypes tipo= ExpressionTypes.nothing )
        {
            // semanticas de las expresiones Generales 

            var type = this.GetTipo();

            if (type == tipo)
            {
                return true;
            }
            else
                return false;
        }
    }


}