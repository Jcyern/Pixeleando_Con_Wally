using ExpressionesUnarias;
using ExpressionesTipos;
using Expresion;
using TerminalesNode;
using Evalua;


namespace Numero
{

    #region  Number
    public class Number : TerminalExpression
    {
        public Number(object value, (int, int) Location) : base(value, Location)
        {
            this.value = value;
        }

        public override ExpressionTypes GetTipo()
        {
            if (value  != null)
            {
                return ExpressionTypes.Number;
            }

            return ExpressionTypes.NullNumber;
        }



        public override object Evaluate(Evaluator? evaluator = null)
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