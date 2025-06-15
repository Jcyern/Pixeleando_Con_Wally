

using System;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using metodos;
using Parseando;
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

                bool rangex = Metodos.InRange(x, -1, 1);
                bool rangey = Metodos.InRange(y, -1, 1);

                if (rangex && rangey)
                {
                    //retornar el valor correcto 
                    System.Console.WriteLine($"Dibujar circulo de {Wally.Pos.Fila},{Wally.Pos.Columna} en dir {x},{y}  en un radio  de {rad}");
                    //llamar al metodo de unitu 
                    Metodos.DrawCircle(x, y, rad);

                    if (evaluator != null)
                        evaluator.Move();

                    return 0;
                }
                else
                {
                    System.Console.WriteLine("hay direcciones fuera de rango");
                    if (!rangex)
                    {
                        //crear error de fuera de rango 
                        if (evaluator != null)
                        {
                            evaluator.AddError(new DirectOutOfRange(Circle.Pos, (-1, 1), x));
                        }

                    }
                    if (!rangey)
                    {
                        //crear error de rango 
                        if(evaluator!=null)
                        evaluator.AddError(new DirectOutOfRange(Circle.Pos, (-1, 1), y));
                    }

                    if (evaluator != null)
                        evaluator.Move();

                    return 0;
                }
            }
            else
            {
                System.Console.WriteLine("erorr pq el wally no esta posicionado");
                if (evaluator != null)
                {
                    evaluator.AddError(new IsntSpawn(Circle.Pos));
                    evaluator.Move();
                }

                return 0;
            }
        }
    }



    public class DrawCircleParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            System.Console.WriteLine("Parseando DrawLine");
            var circle = parser.Current;
            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.NextToken();
            //ahi empieza las expresiones 

            var token_disx = new List<Token>();
            while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
            {
                System.Console.WriteLine($"Se agrega a DisX {parser.Current}" );
                //ir agregando los tokens q seria las dist_X
                token_disx.Add(parser.Current);
                parser.NextToken();
            }
            //exp de distancia X
            var distX = Converter.GetExpression(token_disx);
            

            //verificar si es el close parentesis seria un error 
            if (parser.Current.type == TypeToken.Coma)
            {
                parser.NextToken();
                System.Console.WriteLine("Curretn es difrente de Close de Parentesis");
                System.Console.WriteLine("Hay distX ver  lo demas ");

                // tokens token dist Y
                var token_disy = new List<Token>();

                while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                {
                    System.Console.WriteLine($"Se agrega a DistY {parser.Current}");
                    token_disy.Add(parser.Current);
                    parser.NextToken();
                }

                var distY = Converter.GetExpression(token_disy);
                

                if (parser.Current.type == TypeToken.Coma)
                {
                    parser.NextToken();
                    System.Console.WriteLine("Hay dist y");
                    var tokens_distancia = new List<Token>();


                    while (parser.Current.type != TypeToken.CloseParenthesis)
                    {
                        System.Console.WriteLine($"Se agrego a Dist{parser.Current}");
                        tokens_distancia.Add(parser.Current);
                        parser.NextToken();
                    }
                    parser.NextToken();

                    var radius = Converter.GetExpression(tokens_distancia);
                    
                    return new DrawLineNode(circle, distX, distY, radius);

                }
                else
                {
                    parser.NextToken();
                    System.Console.WriteLine("es un error falta distancia  ");
                    return new DrawLineNode(circle, distX, distY, null);
                }
            }

            else
            {
                parser.NextToken();
                System.Console.WriteLine("es un error falta diry y  distancia  ");
                return new DrawLineNode(circle, distX, null, null);
            }
        }

        
    }
}