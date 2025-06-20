
using Evalua;
using ExpressionesTipos;
using IParseo;
using Numero;
using Parseando;
using TerminalesNode;
using Unity.VisualScripting;
using UnityEngine;

namespace ArbolSintaxisAbstracta
{
    public class OperadorNode : TerminalExpression
    {
        Token Operador;
        public OperadorNode(Token operador) : base(operador.value, operador.Pos)
        {
            Operador = operador;
        }

        public override ExpressionTypes GetTipo()
        {
            return ExpressionTypes.Operator;
        }

        public override object? Evaluate(Evaluator? evaluator = null)
        {

            return Operador;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            return true;
        }
    }



    public class OperadorParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            Debug.Log("parseo de operador");
            var operador = parser.Current;

            if (operador.value == "-")
            {

                //puede ser un numero negativo 
                if (parser.current - 1 < 0 && (parser.GetNextToken().type == TypeToken.Numero || parser.GetNextToken().type == TypeToken.OpenParenthesis))
                {
                    Debug.Log(" no es operador");
                    var negative = new NegativeParse();

                    return negative.Parse(parser);

                }
                else if ((parser.tokens[parser.current - 1].type == TypeToken.OpenParenthesis || parser.tokens[parser.current - 1].type == TypeToken.Operador || parser.tokens[parser.current - 1].type == TypeToken.Asignacion) && (parser.GetNextToken().type == TypeToken.Numero && parser.GetNextToken().type == TypeToken.OpenParenthesis))
                {
                    //es negativo 

                    Debug.Log("no es operador");
                    var negative = new NegativeParse();

                    return null;
                }
                else
                {
                    Debug.Log("Operador");

                    parser.NextToken();
                    return new OperadorNode(operador);
            
                }
            }
            else
            {
                Debug.Log("Operador");

                parser.NextToken();
                return new OperadorNode(operador);
            }
        }
    }

}