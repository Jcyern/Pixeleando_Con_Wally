using System;
using Evalua;
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;
using UnityEngine;

namespace Arbol_de_Sintaxis_Abstracta
{
    public class AndNode : BinaryExpression
    {
        public AndNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {

        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            //verifica si son de tipo bool  con lo que opera

            return base.CheckSemantic(ExpressionTypes.Bool);
        }


        public override object Evaluate(Evaluator? evaluator = null)
        {
            //Evaluando and 
            var left = Convert.ToBoolean(LeftExpression!.Evaluate());
            Debug.Log($"Left And {left}");
            var right = Convert.ToBoolean(RightExpression!.Evaluate());
            Debug.Log($"Right And {right}");


            return left && right;
        }

        public override ExpressionTypes GetTipo()
        {
            //verifica si opera con tipo bool 
            var tipo = base.GetTipo();

            if (tipo == ExpressionTypes.Bool)
            {
                //devuelve bool 
                return ExpressionTypes.Bool;
            }
            else
            {
                // no opera con bool , no puede ejecutarse 
                return tipo;
            }
        }
        

    }

    public class AndNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new AndNode(Left, Operator, Right);

            return null;
        }
    }
}