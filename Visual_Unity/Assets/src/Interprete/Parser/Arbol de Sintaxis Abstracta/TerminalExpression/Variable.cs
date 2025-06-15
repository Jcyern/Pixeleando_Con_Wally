using System.ComponentModel;
using Alcance;
using ArbolSintaxisAbstracta;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using TerminalesNode;
using UnityEngine;

namespace vars
{
    public class VariableNode : TerminalExpression
    {
        Token Identificador;

        public VariableNode(Token Identificador) : base(Identificador.value, Identificador.Pos)
        {
            this.Identificador = Identificador;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            Debug.Log("Chequeo Sem de Variable ");
            //verifica si la variable ya se le asigno un tipo 
            //verifica si la varible esta asignaada y ademas es del tipo pasado 
            if (Scope.ContainType(Identificador.value))
            {
                var type = GetTipo();

                if (type == tipo)
                    return true;

                else
                {
                    compilingError.Add(new ExpectedType(Identificador.Pos, tipo.ToString(), type.ToString()));
                    return false;
                }
            }
            else
            {
                compilingError.Add(new ExpectedType(Identificador.Pos, tipo.ToString(), ExpressionTypes.Null.ToString()));
                Debug.Log("La variable no esta definida");
                return false;
            }
        }

        public override object? Evaluate(Evaluator? evaluator = null)
        {
            //dame el valor de la variable 
            var result = Scope.GetVariable(Identificador.value);
            Debug.Log($"La Variable {Identificador.value} guarda {result}");
            return result;
        }

        public override ExpressionTypes GetTipo()
        {

            if (Scope.ContainType(Identificador.value))
            {
                return Scope.variable_tipo[Identificador.value];
            }

            else
                return ExpressionTypes.Null;
        }
    }


    public class VariableParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var v = parser.Current;
            parser.NextToken();
            return new VariableNode(v);
        }
    }
}