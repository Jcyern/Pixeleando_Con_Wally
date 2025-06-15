
using System;
using System.Collections.Generic;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using metodos;
using Numero;
using Parseando;
using UnityEngine;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class DrawLineNode : AstNode
    {
        Token Draw;
        Expression? dirX;
        Expression? dirY;

        Expression? distancia;


        public DrawLineNode(Token Draw, Expression? dirX, Expression? dirY, Expression? distance)
        {
            this.Draw = Draw;
            this.dirX = dirX;
            this.dirY = dirY;
            this.distancia = distance;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            bool chek_x = true;
            bool chek_y = true;
            bool chek_d = true;
            if (dirX == null)
            {
                //agregar error de compilacion 
                compilingError.Add(new ExpectedType(Draw.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_x = false;
            }
            else
            {
                chek_x = dirX.CheckSemantic(ExpressionTypes.Number);
            }

            if (dirY == null)
            {
                compilingError.Add(new ExpectedType(Draw.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_y = false;
            }
            else
            {
                chek_y = dirY.CheckSemantic(ExpressionTypes.Number);
            }

            if (distancia == null)
            {
                compilingError.Add(new ExpectedType(Draw.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                chek_d = false;
            }
            else
            {
                chek_d = distancia.CheckSemantic(ExpressionTypes.Number);
            }


            if (!chek_x || !chek_y || !chek_d)
            {
                return false;
            }
            else
                return true;


        }
        public override object? Evaluate(Evaluator? evaluador = null)
        {
            Debug.Log("Verificar si es una Direccion Valida");

            var x = Convert.ToInt32(dirX!.Evaluate());
            var y = Convert.ToInt32(dirY!.Evaluate());
            var d = Convert.ToInt32(distancia!.Evaluate());

            if (Wally.Pos.Fila != int.MaxValue)
            {
                var range_x =Metodos.InRange(x, -1, 1);
                var range_y = Metodos.InRange(y, -1, 1);

                if (range_x && range_y)
                {
                    Debug.Log($"Mover una Linea en la dir {x},{y} desde {Wally.Pos.Fila}, {Wally.Pos.Columna}  distancia {d} ");
                    //lllamar al metodo de dibujar en unity
                    Metodos.DrawLine(x, y, d);
                                //mover pos en el evaluador 
                    if (evaluador != null)
                        evaluador.Move();

                    return 0;
                }
                if (!range_x)
                {
                    //crear error de rango de x
                    Debug.Log("error de rango de x");
                }
                if (!range_y)
                {
                    Debug.Log("error de rango de y ");
                    //crear error de rango de y
                }

                            //mover pos en el evaluador 
                if (evaluador != null)
                evaluador.Move();
                return 0;
            }
            else
            {
                //dar error de q no hay spawn 
                Debug.Log("El wally no tiene una pos");
                            //mover pos en el evaluador 
                if (evaluador != null)
                evaluador.Move();
                
            }
            return 0;
            
        }




    }


    public class DrawLineParse : IParse
    {
        public AstNode Parse(Parser parser)
        {
            Debug.Log("Parseando DrawLine");
            var draw = parser.Current;
            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.NextToken();
            //ahi empieza las expresiones 

            var token_disx = new List<Token>();
            while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
            {
                Debug.Log($"Se agrega a DisX {parser.Current}" );
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
                Debug.Log("Curretn es difrente de Close de Parentesis");
                Debug.Log("Hay distX ver  lo demas ");

                // tokens token dist Y
                var token_disy = new List<Token>();

                while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                {
                    Debug.Log($"Se agrega a DistY {parser.Current}");
                    token_disy.Add(parser.Current);
                    parser.NextToken();
                }

                var distY = Converter.GetExpression(token_disy);

                if (parser.Current.type == TypeToken.Coma)
                {
                    parser.NextToken();
                    Debug.Log("Hay dist y");
                    var tokens_distancia = new List<Token>();


                    while (parser.Current.type != TypeToken.CloseParenthesis)
                    {
                        Debug.Log($"Se agrego a Dist{parser.Current}");
                        tokens_distancia.Add(parser.Current);
                        parser.NextToken();
                    }
                    parser.NextToken();

                    var dist = Converter.GetExpression(tokens_distancia);
                    return new DrawLineNode(draw, distX, distY, dist);

                }
                else
                {
                    parser.NextToken();
                    Debug.Log("es un error falta distancia  ");
                    return new DrawLineNode(draw, distX, distY, null);
                }
            }

            else
            {
                parser.NextToken();
                Debug.Log("es un error falta diry y  distancia  ");
                return new DrawLineNode(draw, distX, null, null);
            }
        }
    }
}