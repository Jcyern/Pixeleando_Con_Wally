using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using TerminalesNode;

namespace Arbol_de_Sintaxis_Abstracta
{
    public class IsCanvasColorNode : TerminalExpression
    {
        Token Canvas;
        Expression? color;
        Expression? vertical;
        Expression? horizontal;

        public IsCanvasColorNode(Token canvas, Expression? color, Expression? vertical, Expression? horizontal) : base(canvas.value, canvas.Pos)
        {
            Canvas = canvas;
            this.color = color;
            this.vertical = vertical;
            this.horizontal = horizontal;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            bool chek_color = true;
            bool chek_v = true;
            bool chek_h = true;
            if (color == null)
            {
                //agregar error de compilacion 
                compilingError.Add(new ExpectedType(Canvas.Pos, ExpressionTypes.Color.ToString(), ExpressionTypes.Null.ToString()));
                chek_color = false;
            }
            else
            {
                chek_color = color.CheckSemantic(ExpressionTypes.Color);
            }

            if (vertical == null)
            {
                compilingError.Add(new ExpectedType(Canvas.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_v = false;
            }
            else
            {
                chek_v = vertical.CheckSemantic(ExpressionTypes.Number);
            }

            if (horizontal == null)
            {
                compilingError.Add(new ExpectedType(Canvas.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_h = false;
            }
            else
            {
                chek_h = horizontal.CheckSemantic(ExpressionTypes.Number);
            }


            if (!chek_color || !chek_v || !chek_h)
            {
                return false;
            }
            else
                return true;


        }

        public override ExpressionTypes GetTipo()
        {
            if (color != null && vertical != null && horizontal != null)
            {
                var c = color.GetTipo();
                var v = vertical.GetTipo();
                var h = horizontal.GetTipo();

                if (c == ExpressionTypes.Color && v == ExpressionTypes.Number && h == ExpressionTypes.Number)
                {
                    return ExpressionTypes.Bool;
                }
            }
            return ExpressionTypes.Null;
        }

        public override object? Evaluate(Evaluator? evaluator = null)
        {
            System.Console.WriteLine("Evaluate de IsCanvasColor");
            System.Console.WriteLine("Aun no se ha hecho ");

            return 0;
        }
    }

}