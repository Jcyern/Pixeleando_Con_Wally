using ExpressionesUnarias;
using ExpressionesTipos;
using Expresion;
using TerminalesNode;
using Evalua;
using IParseo;
using ArbolSintaxisAbstracta;
using Parseando;
using UnityEngine;


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
                Debug.Log($"Numero Evaluate {value}");
                return value;
            }
            Debug.Log("Numero Evaluate 0");
            return 0;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            Debug.Log("Numero ChekSemantic");
            if (tipo == ExpressionTypes.nothing)
                return true;

            else
                return tipo == ExpressionTypes.Number;
        }
    }


    public class NumberParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            Debug.Log("Parsear Num");
            
            var num = parser.Current;

            //seguir analizando 
            parser.NextToken();
            Debug.Log(parser.Current.value);

            return new Number(num.value, num.Pos);
        }
    }

    #endregion

}