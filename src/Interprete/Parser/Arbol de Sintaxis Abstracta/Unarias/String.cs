
using ExpressionesUnarias;
using ExpressionesTipos;

namespace Cadenas
{
    
    #region String 
    
    public class String :UnaryExpression
    {

        public String(object value, (int, int) Location)
        {
            this.value = value;
            this.Location = Location;
        }

        public override ExpressionTypes GetTipo()
        {
            if (this.value == null)
                return ExpressionTypes.NullString;

            else
                return ExpressionTypes.String;

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