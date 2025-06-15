using System;
using System.Collections.Generic;
using ArbolSintaxisAbstracta;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using TerminalesNode;
using UnityEngine;
using Utiles;

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
            var x = Convert.ToInt32(vertical.Evaluate());
            var y = Convert.ToInt32(horizontal.Evaluate());
            var c = color.Evaluate().ToString();
            Debug.Log("Evaluate de IsCanvasColor");
            Debug.Log("Aun no se ha hecho ");

            if (Methots.IsInRange((x, y)))
                return Wally.tablero[x, y] == c;

            //si esta fuera de los limtes
            return false;
        }
    }


    public class IsCanvasColorParse : IParse
    {
        public AstNode Parse(Parser parser)
        {
            Debug.Log("Parseando IsCanvasColor");
            var canvas = parser.Current;
            var result = parser.ExpectedTokenType(TypeToken.OpenParenthesis);

            if (result)
            {
                parser.NextToken();
                //ahi empieza las expresiones 

                var token_color= new List<Token>();
                while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                {
                    Debug.Log($"Se agrega a DisX {parser.Current}");
                    //ir agregando los tokens q seria las dist_X
                    token_color.Add(parser.Current);
                    parser.NextToken();
                }
                //exp de distancia X
                var color = Converter.GetExpression(token_color);


                //verificar si es el close parentesis seria un error 
                if (parser.Current.type == TypeToken.Coma)
                {
                    parser.NextToken();
                    Debug.Log("Curretn es difrente de Close de Parentesis");
                    Debug.Log("Hay distX ver  lo demas ");

                    // tokens token dist Y
                    var token_vertical = new List<Token>();

                    while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                    {
                        Debug.Log($"Se agrega a vertical {parser.Current}");
                        token_vertical.Add(parser.Current);
                        parser.NextToken();
                    }

                    var vertical = Converter.GetExpression(token_vertical);

                    if (parser.Current.type == TypeToken.Coma)
                    {
                        parser.NextToken();
                        Debug.Log("Hay dist horizontal");
                        var tokens_horizontal= new List<Token>();


                        while (parser.Current.type != TypeToken.CloseParenthesis)
                        {
                            Debug.Log($"Se agrego a Dist{parser.Current}");
                            tokens_horizontal.Add(parser.Current);
                            parser.NextToken();
                        }
                        parser.NextToken();

                        var horizontal = Converter.GetExpression(tokens_horizontal);
                        return new IsCanvasColorNode(canvas, color, vertical, horizontal);

                    }
                    else
                    {
                        parser.NextToken();
                        Debug.Log("es un error falta distancia  ");
                        return new IsCanvasColorNode(canvas, color, vertical, null);
                    }
                }
                else
                {
                    parser.NextToken();
                    Debug.Log("es un error falta diry y  distancia  ");
                    return new IsCanvasColorNode(canvas, color, null, null);
                }
            }
            else
                return null;
        }

    }
}