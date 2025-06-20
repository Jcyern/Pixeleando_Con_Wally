using Expresion;
using ExpressionesTipos;
using Numero;
using Cadenas;
using Boolean;
using ExpressionesBinarias;
using TerminalesNode;
using UnityEngine;

namespace ExpressionesUnarias
{
    #region UnaryExpression



    public abstract class UnaryExpression : Expression
    {
        public Token Operator;
        public Expression? expresion;
        public UnaryExpression(Expression? expresion, Token Operator, (int, int) Location)
        {
            this.expresion = expresion;
            this.Operator = Operator;
            this.Location = Location;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (tipo == ExpressionTypes.nothing)
                return expresion.CheckSemantic();

            return expresion.CheckSemantic(tipo);
        }



        public override ExpressionTypes GetTipo()
        {
            Debug.Log("Unary Expression Tipo");
            if (expresion != null)
            {
                Debug.Log("La unary no es null en su exp");
                if (expresion is BinaryExpression be)
                {
                    return be.GetTipo();
                }

                if (expresion is TerminalExpression te)
                {
                    return te.GetTipo();
                }

                if (expresion is UnaryExpression unary)
                {
                    return unary.GetTipo();
                }

            }

            Debug.Log(" la unary es null en su exp");


            return ExpressionTypes.Invalid;
        }
    }

    #endregion





}