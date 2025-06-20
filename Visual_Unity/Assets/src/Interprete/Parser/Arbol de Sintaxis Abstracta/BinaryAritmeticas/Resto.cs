using ExpressionesBinarias;
using Expresion;
using ExpressionesTipos;
using INodeCreador;
using Evalua;
using System;

namespace Arbol_de_Sintaxis_Abstracta
{


    public class RestoNode : BinaryExpression
    {
        public RestoNode(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            return base.CheckSemantic(ExpressionTypes.Number);
        }
        public override object Evaluate(Evaluator? evaluator = null)
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) % Convert.ToInt32(RightExpression!.Evaluate());
        }

        public override ExpressionTypes GetTipo()
        {
            var tipo = base.GetTipo();

            if (tipo == ExpressionTypes.Number)
            {
                return ExpressionTypes.Number;
            }
            else
            {
                return tipo;
            }
        }

    }


    public class RestoNodeCreator : INodeCreator
    {
        public Expression? CreateNode(Expression? Left, Token Operator, Expression? Right)
        {
            if (Left != null && Right != null)
                return new RestoNode(Left, Operator, Right);

            return null;
        }
    }

}