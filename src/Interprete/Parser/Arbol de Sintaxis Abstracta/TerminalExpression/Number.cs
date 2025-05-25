using ExpressionesUnarias;
using ExpressionesTipos;
using Expresion;
using TerminalesNode;


namespace Numero
{

    #region  Number
    public class Number : TerminalExpression
    {
        public Number(object value , (int, int) Location) : base(value , Location)
        {
        }

        public override ExpressionTypes GetTipo()
        {
            if (value  != null)
            {
                return ExpressionTypes.Number;
            }

            return ExpressionTypes.NullNumber;
        }



        public override object Evaluate()
        {
            if (value != null)
            {
                return value;
            }

            return 0;
        }
    }

    #endregion

}