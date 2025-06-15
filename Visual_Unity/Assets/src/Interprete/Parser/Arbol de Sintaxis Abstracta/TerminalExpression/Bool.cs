using ExpressionesUnarias;
using ExpressionesTipos;
using TerminalesNode;
using Evalua;
using IParseo;
using ArbolSintaxisAbstracta;
using Parseando;
using System;
using UnityEngine;

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
                Debug.Log("Null Bool");
                return ExpressionTypes.Null;
            }
            else
            {
                Debug.Log($" Es bool - valor {value}");
                return ExpressionTypes.Bool;
            }
        }

        public override object Evaluate(Evaluator? evaluator = null)
        {
            if (value != null)
                return Convert.ToBoolean(value);

            return false;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            Debug.Log("BoolCheckSemantic");
            //siempre se define bien 
            return true;
        }
    }

    #endregion

    public class BoolParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var  valor  = parser.Current;
            //para seguir las evaluaciones
            parser.NextToken();

            return new Bool(valor.value, valor.Pos);
        }
    }
}