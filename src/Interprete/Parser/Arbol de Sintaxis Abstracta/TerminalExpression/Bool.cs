using ExpressionesUnarias;
using ExpressionesTipos;
using TerminalesNode;

namespace Boolean
{

    #region Bool

    public class Bool : TerminalExpression
    {
        public Bool(object value, (int, int) Location) : base(value, Location)
        {
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