using ExpressionesUnarias;
using ExpressionesTipos;
using Expresion;
using TerminalesNode;
using Evalua;
using IParseo;
using ArbolSintaxisAbstracta;
using Parseando;


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
            if (value != null)
            {
                return ExpressionTypes.Number;
            }

            return ExpressionTypes.NullNumber;
        }



        public override object Evaluate(Evaluator? evaluator = null)
        {
            if (value != null)
            {
                System.Console.WriteLine($"Numero Evaluate {value}");
                return value;
            }
            System.Console.WriteLine("Numero Evaluate 0");
            return 0;
        }
    }


    public class NumberParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            System.Console.WriteLine("Parsear Num");
            System.Console.WriteLine(parser.Current.value);
            var num = parser.Current;

            //seguir analizando 
            parser.NextToken();
            System.Console.WriteLine(parser.Current.value);

            return new Number(num.value, num.Pos);
        }
    }

    #endregion

}