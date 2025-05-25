using ExpressionesUnarias;
using ExpressionesTipos;

namespace Boolean
{

    #region Bool

    public class Bool : UnaryExpression
    {

        public Bool(object value, (int, int) Location)
        {
            this.value = value;
            this.Location = Location;
        }


        public override ExpressionTypes GetTipo()
        {
            if (value == null)
            {
                System.Console.WriteLine("Null Bool");
                return ExpressionTypes.Null;
            }
            else
            {
                System.Console.WriteLine($" Es bool - valor {value}");
                return ExpressionTypes.Bool;
            }
        }




        public override object Evaluate()
        {
            if (value != null)
                return value;

            return false;
        }
    }

    #endregion



}