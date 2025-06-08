
using ArbolSintaxisAbstracta;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using TerminalesNode;

namespace Arbol_de_Sintaxis_Abstracta
{
    public class GetColorCountNode : TerminalExpression
    {
        Token get;
        Expression? Color;
        Expression? X1;
        Expression? X2;

        Expression? Y1;
        Expression? Y2;

        public GetColorCountNode(Token get, Expression? Color, Expression? X1, Expression? Y1, Expression? X2, Expression? Y2) : base (get.value,get.Pos
        )
        {
            this.get = get;
            this.Color = Color;
            this.X1 = X1;
            this.Y1 = Y1;
            this.X2 = X2;
            this.Y2 = Y2;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            bool chek_x1 = true;
            bool chek_y1 = true;
            bool chek_x2 = true;
            bool chek_y2 = true;
            bool chek_color = true;
            if (X1 == null)
            {
                //agregar error de compilacion 
                compilingError.Add(new ExpectedType(get.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_x1 = false;
            }
            else
            {
                chek_x1 = X1.CheckSemantic(ExpressionTypes.Number);
            }

            if (Y1 == null)
            {
                compilingError.Add(new ExpectedType(get.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_y1 = false;
            }
            else
            {
                chek_y1 = Y1.CheckSemantic(ExpressionTypes.Number);
            }

            if (X2 == null)
            {
                compilingError.Add(new ExpectedType(get.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_x2 = false;
            }
            else
            {
                chek_x2 = X2.CheckSemantic(ExpressionTypes.Number);
            }
            if (Y2 == null)
            {
                compilingError.Add(new ExpectedType(get.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_y2 = false;
            }
            else
            {
                chek_y2 = Y2.CheckSemantic(ExpressionTypes.Number);
            }
            if (Color == null)
            {
                compilingError.Add(new ExpectedType(get.Pos, ExpressionTypes.Color.ToString(), ExpressionTypes.Null.ToString()));
                chek_color = false;
            }
            else
            {
                chek_color = Color.CheckSemantic(ExpressionTypes.Color);
            }


            if (!chek_x1 || !chek_y1 || !chek_x2 || !chek_y2 || !chek_color)
            {
                return false;
            }
            else
                return true;


        }

        public override object? Evaluate(Evaluator? evaluador = null)
        {
            System.Console.WriteLine("Evaluate de  GetColorCount");
            System.Console.WriteLine("Aun no se ha definido ");
            return 0;
        }

        public override ExpressionTypes GetTipo()
        {
            if (Color != null && X1 != null && X2 != null && Y1 != null && Y2 != null)
            {
                var c = Color.GetTipo();
                var x1 = X1.GetTipo();
                var x2 = X2.GetTipo();
                var y1 = Y1.GetTipo();
                var y2 = Y2.GetTipo();

                if (x1 == ExpressionTypes.Number && x2 == ExpressionTypes.Number && y1 == ExpressionTypes.Number && y2 == ExpressionTypes.Number && c == ExpressionTypes.Color)
                {
                    return ExpressionTypes.Number;
                }
                else
                    return ExpressionTypes.Invalid;
            }
            return ExpressionTypes.Null;
        }
    }
}