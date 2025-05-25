using Boolean;
using Expresion;
using ExpressionesTipos;
using Numero;
using Cadenas;


namespace TerminalesNode
{
    public abstract class TerminalExpression : Expression
    {
        public TerminalExpression(object value, (int, int) Location)
        {
            this.value = value;
            this.Location = Location;
        }

        public override ExpressionTypes GetTipo()
        {
            if (this is Number number)
            {
                return number.GetTipo();
            }

            else if (this is Bool @bool)
            {
                return @bool.GetTipo();
            }

            else if (this is Cadenas.String @string)
            {

                return @string.GetTipo();
            }

            else
            {
                return ExpressionTypes.Invalid;
            }
        }
    }
}