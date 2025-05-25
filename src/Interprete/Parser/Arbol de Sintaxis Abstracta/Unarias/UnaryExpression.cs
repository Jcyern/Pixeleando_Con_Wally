using Expresion;
using ExpressionesTipos;
using Numero;
using Cadenas;
using Boolean;

namespace ExpressionesUnarias
{
    #region UnaryExpression

    public abstract class UnaryExpression : Expression
    {

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

    #endregion





}