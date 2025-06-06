

using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using metodos;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class DrawCircleNode : AstNode
    {
        Token Circle;
        Expression? dirX;
        Expression? dirY;
        Expression? radius;
        public DrawCircleNode(Token Circle, Expression? dirX, Expression? dirY, Expression? radius)
        {
            this.Circle = Circle;
            this.dirX = dirX;
            this.dirY = dirY;
            this.radius = radius;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            bool chek_x = true;
            bool chek_y = true;
            bool chek_r = true;
            if (dirX == null)
            {
                //agregar error de compilacion 
                compilingError.Add(new ExpectedType(Circle.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_x = false;
            }
            else
            {
                chek_x = dirX.CheckSemantic(ExpressionTypes.Number);
            }

            if (dirY == null)
            {
                compilingError.Add(new ExpectedType(Circle.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_y = false;
            }
            else
            {
                chek_y = dirY.CheckSemantic(ExpressionTypes.Number);
            }

            if (radius == null)
            {
                compilingError.Add(new ExpectedType(Circle.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_r = false;
            }
            else
            {
                chek_r = radius.CheckSemantic(ExpressionTypes.Number);
            }


            if (!chek_x || !chek_y || !chek_r)
            {
                return false;
            }
            else
                return true;

        }

        public override object? Evaluate(Evaluator? evaluator = null)
        {
            //verificar primero si hay un Spawn por donde empezar

            if (Wally.Pos.Fila != int.MaxValue)
            {
                //evaluar las dir y ver si esta en  rango 
                var x = Convert.ToInt32(dirX!.Evaluate());
                var y = Convert.ToInt32(dirY!.Evaluate());
                var rad = Convert.ToInt32(radius!.Evaluate());

                bool rangex = Methots.InRange(x, -1, 1);
                bool rangey = Methots.InRange(y, -1, 1);

                if (rangex && rangey)
                {
                    //retornar el valor correcto 
                    System.Console.WriteLine($"Dibujar circulo de {Wally.Pos.Fila},{Wally.Pos.Columna} en dir {x},{y}  en un radio  de {rad}");

                    return 0;
                }
                else
                {
                    System.Console.WriteLine("hay direcciones fuera de rango");
                    if (!rangex)
                    {
                        //crear error de fuera de rango 
                        compilingError.Add(new DirectOutOfRange(Circle.Pos, (-1, 1), x));

                    }
                    if (!rangey)
                    {
                        //crear error de rango 
                        compilingError.Add(new DirectOutOfRange(Circle.Pos, (-1, 1), y));
                    }


                    return 0;
                }
            }
            else
            {
                System.Console.WriteLine("erorr pq el wally no esta posicionado");
                compilingError.Add(new IsntSpawn(Circle.Pos));
                return 0;
            }
        }
    }
}