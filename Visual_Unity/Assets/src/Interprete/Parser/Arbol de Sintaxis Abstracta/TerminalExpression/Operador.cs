
using Evalua;
using ExpressionesTipos;
using IParseo;
using Numero;
using Parseando;
using TerminalesNode;
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
                Debug.Log("parseando negativo ");
                //puede ser un numero negativo 
                if (parser.current - 1 < 0 && parser.GetNextToken().type == TypeToken.Numero)
                {
                    var numero = parser.NextToken();
                    //seguir avanzando 
                    parser.NextToken();
                    //esta en la pos inicial es un negativo 
                    Debug.Log($"Devolver numero negativo {"-"+numero.value.ToString()}");
                    return new Number("-"+numero.value, numero.Pos);
                }
                else
                {
                    if ((parser.tokens[parser.current - 1].type == TypeToken.OpenParenthesis || parser.tokens[parser.current - 1].type == TypeToken.Operador || parser.tokens[parser.current - 1].type == TypeToken.Asignacion) && parser.GetNextToken().type == TypeToken.Numero)
                    {
                        var numero = parser.NextToken();
                        //para seguir analizando
                        parser.NextToken();
                        Debug.Log($"Devolver numero negativo {"-"+numero.value.ToString()}");
                        return new Number("-"+numero.value, numero.Pos);
                    }

                    
                    parser.NextToken();
                    return new OperadorNode(operador);

                }
            }

            
            Debug.Log("Se acabo ");
            parser.NextToken();
            return new OperadorNode(operador);
        }
    }

}