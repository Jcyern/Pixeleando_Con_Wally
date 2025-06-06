
using Cadenas;
using Evalua;
using ExpressionesTipos;
using TerminalesNode;

namespace Paleta_Colores
{
    public class Color : TerminalExpression
    {
        public Color(object? value, (int, int) Location) : base(value, Location)
        {
        }
        public override ExpressionTypes GetTipo()
        {
            if (this.value == null)
                return ExpressionTypes.NullString;

            else
                return ExpressionTypes.Color;
        }


        public override object? Evaluate(Evaluator? evaluator =  null)
        {
            System.Console.WriteLine($"Evaluate Color");
            return value;
        }
    }
}