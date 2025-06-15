
using ArbolSintaxisAbstracta;
using Cadenas;
using Evalua;
using ExpressionesTipos;
using IParseo;
using Parseando;
using TerminalesNode;
using UnityEngine;

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


        public override object? Evaluate(Evaluator? evaluator = null)
        {
            Debug.Log($"Evaluate Color");
            return value;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            Debug.Log("Color CheckSemantic");
            //un color siempre se define bien 
            return true;
        }
    }


    public class ColorParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var color = parser.Current;
            //Seguir la evaluacion
            parser.NextToken();

            return new Color(color.value, color.Pos);
        }
    }
}