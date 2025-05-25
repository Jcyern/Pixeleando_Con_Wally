using ExpressionesUnarias;
using ExpressionesTipos;

namespace Numero
{

    #region  Number
    public class Number : UnaryExpression
    {
        public Number(object number, (int, int) pos)
        {
            this.value = number;
            this.Location = pos;
        }

        public override ExpressionTypes GetTipo()
        {
            if (this.value == null)
            {

                System.Console.WriteLine("es Null  ( Number) ");
                return ExpressionTypes.NullNumber;
            }

            else
            {
                System.Console.WriteLine($" Es numero  - valor {this.value}");
                return ExpressionTypes.Number;
            }
        }



        public override object Evaluate()
        {
            if (value != null)
                return value;

            return 0;
        }
    }

    #endregion

}