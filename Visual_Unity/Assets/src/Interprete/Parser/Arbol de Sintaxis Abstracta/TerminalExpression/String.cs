
using ExpressionesUnarias;
using ExpressionesTipos;
using TerminalesNode;

using Paleta_Colores;
using Evalua;
using IParseo;
using ArbolSintaxisAbstracta;
using Parseando;

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

        public override object? Evaluate(Evaluator? evaluator = null)
        {
            if (value != null)
                return value;

            return null;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (tipo == ExpressionTypes.nothing)
                return true;
            else
            {
                return tipo == ExpressionTypes.String;
            }
        }
    }


    #endregion


    public class StringParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var str = parser.Current;
            //seguir avanzando 

            parser.NextToken();

            return new Cadenas.String(str.value, str.Pos);
        }
    }
}