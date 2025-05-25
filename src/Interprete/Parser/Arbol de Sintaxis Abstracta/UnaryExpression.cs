using Expresion;
using ExpressionesTipos;
using Numero;
using Cadenas;
using Boolean;
using ExpressionesBinarias;
using TerminalesNode;

namespace ExpressionesUnarias
{
    #region UnaryExpression



    public abstract class UnaryExpression : Expression
    {
        public Token Operator;
        public Expression expresion;
        public UnaryExpression(Expression expresion, Token Operator, (int, int) Location)
        {
            this.expresion = expresion;
            this.Operator = Operator;
            this.Location = Location;
        }


        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            return base.CheckSemantic(tipo);
        }



        public override ExpressionTypes GetTipo()
        {
            if (expresion != null)
            {
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


            return ExpressionTypes.Invalid;
        }
    }

    #endregion





}