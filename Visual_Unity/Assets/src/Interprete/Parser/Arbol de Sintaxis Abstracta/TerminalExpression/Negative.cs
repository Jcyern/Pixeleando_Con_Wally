
using System;
using System.Collections.Generic;

using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using ExpressionesUnarias;
using IParseo;
using Parseando;
using TerminalesNode;
using UnityEditor.Search;
using UnityEngine;

namespace ArbolSintaxisAbstracta
{
    public class NegativeNode : UnaryExpression
    {
        Token negative;
        Expression expression;
        public NegativeNode(Token negative, Expression? exp) : base(exp, negative, negative.Pos)
        {
            expression = exp;
            this.negative = negative;

        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            Debug.Log("Cheque semantico negacion");
            if (expression != null)
            {
                Debug.Log("No es nula la expresion de negacion");
                return expression.CheckSemantic(ExpressionTypes.Number);
            }
            else
            {   Debug.Log("es null la negaaaaa");
                compilingError.Add(new ExpectedType(negative.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                return false;
            }
        }


        public override ExpressionTypes GetTipo()
        {
            Debug.Log("Negative Get Tipo");
            if (expression != null)
            {
                Debug.Log("No es nula la exp");
                return expression.GetTipo();
            }
            else
            {
                Debug.Log("es nula la exp");
                return ExpressionTypes.Invalid;
            }
        }


        public override object Evaluate(Evaluator evaluador = null)
        {
            return -Convert.ToInt32(expression.Evaluate());
        }
    }


    public class NegativeParse : IParse
    {
        public AstNode Parse(Parser parser)
        {
            Debug.Log("Negative Parse");
            var negative = parser.Current;

            if (parser.ExpectedTokenType(TypeToken.OpenParenthesis))
            {

                var exp = new List<Token>();       
                //esta en el parenteiss avanzar y analizar la exp negativa
                parser.NextToken();
                while (parser.Current.type != TypeToken.CloseParenthesis)
                {
                    Debug.Log($"add negative exp {parser.Current.value}");
                    exp.Add(parser.Current);
                    parser.NextToken();
                }

    

                var exp_convert = Converter.GetExpression(exp);
                Debug.Log("negativo grande");
                parser.NextToken();
                //crear nodo negstivo 
                Debug.Log(parser.Current.value);
                return new NegativeNode(negative, exp_convert);

            }
            else
            {
                //sig el numero 
                var num = new List<Token>
                {
                    parser.Current
                };

                //avanzr para seguir analizando  
                var exp = Converter.GetExpression(num);
            
                return new NegativeNode(negative, exp);
            }
        }
    }
}