

using Evalua;
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using INodeCreador;
using UnityEngine;

namespace Arbol_de_Sintaxis_Abstracta
{
    public class NotEqualsNode : BinaryExpression
    {
        public NotEqualsNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //verifica solo q sean del mismo tipo
            return base.CheckSemantic(tipo);
        }
        
        public override ExpressionTypes GetTipo()
        {
            var tipo = base.GetTipo();
            if (tipo != ExpressionTypes.Invalid && tipo != ExpressionTypes.Null)
            {
                return ExpressionTypes.Bool;
            }
            else
                return tipo;
        }

        public override object Evaluate(Evaluator? evaluator = null)
        {
            var left = LeftExpression!.Evaluate();
            Debug.Log($"NotEquals Left {left}");
            var right = RightExpression!.Evaluate();
            Debug.Log($"NotEquals Right {right} ");

            Debug.Log($"resultado Not equals {left != right}");

            return left!.ToString() != right!.ToString();
        }
    }


    public class NotEqualsNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new NotEqualsNode(Left, Operator, Right);

            return null;
        }
    }
}