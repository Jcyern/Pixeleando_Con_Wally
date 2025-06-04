
using ExpressionesUnarias;
using ExpressionesTipos;
using TerminalesNode;

using Paleta_Colores;

namespace Cadenas
{

    #region String 

    public class String : TerminalExpression
    {
        public String(object value, (int, int) Location) : base(value, Location)
        {
        }
    


        public override ExpressionTypes GetTipo()
        {
            if (this.value == null)
                return ExpressionTypes.NullString;

            else
            {

                return ExpressionTypes.String;
            }

        }

        public override object? Evaluate()
        {
            if (value != null)
                return value;

            return null;
        }
    }


    #endregion


}