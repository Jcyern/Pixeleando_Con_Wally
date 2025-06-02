using Boolean;
using Expresion;
using ExpressionesTipos;
using Numero;
using Cadenas;
using vars;


namespace TerminalesNode
{
    public abstract class TerminalExpression : Expression
    {
        public TerminalExpression(object? value, (int, int) Location)
        {
            this.value = value;
            this.Location = Location;
        }

        public override ExpressionTypes GetTipo()
        {
            if (this is Number number)
            {
                System.Console.WriteLine("Type Numero");
                return number.GetTipo();
            }

            else if (this is Bool @bool)
            {
                System.Console.WriteLine("Type Bool");
                return @bool.GetTipo();
            }

            else if (this is Cadenas.String @string)
            {
                System.Console.WriteLine("Type String");
                return @string.GetTipo();
            }

            else if (this is VariableNode vars)
            {
                return vars.GetTipo();
            }


            else
            {
                System.Console.WriteLine("TYpe INalid");
                return ExpressionTypes.Invalid;
            }
        }
    }
}