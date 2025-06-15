
using System;
using System.Collections.Generic;
using ArbolSintaxisAbstracta;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using metodos;
using Parseando;
using TerminalesNode;
using UnityEngine;

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

        public GetColorCountNode(Token get, Expression? Color, Expression? X1, Expression? Y1, Expression? X2, Expression? Y2) : base(get.value, get.Pos
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
            Debug.Log("Evaluate de  GetColorCount");
            Debug.Log("Aun no se ha definido ");
            string c = Color.Evaluate().ToString();
            int x1 = Convert.ToInt32(X1.Evaluate());
            int x2 = Convert.ToInt32(X2.Evaluate());
            int y1 = Convert.ToInt32(Y1.Evaluate());
            int y2 = Convert.ToInt32(Y2.Evaluate());

            return Metodos.GetColorCount(c, x1, y1, x2, y2);
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



    public class GetColorCountParse : IParse
    {
        public AstNode Parse(Parser parser)
        {

            Debug.Log("Parseando GetColorCount");
            var draw = parser.Current;

            var result = parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.NextToken();
            if (result)
            {
                //ahi empieza las expresiones 

                var token_color = new List<Token>();
                while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                {
                    Debug.Log($"Se agrega a Color{parser.Current}");
                    //ir agregando los tokens q seria las dist_X
                    token_color.Add(parser.Current);
                    parser.NextToken();
                }
                //exp de distancia X
                var color = Converter.GetExpression(token_color);

                #region  Color
                //verificar si es el close parentesis seria un error 
                if (parser.Current.type == TypeToken.Coma)
                {
                    parser.NextToken();
                    Debug.Log("Curretn es difrente de Close de Parentesis");
                    Debug.Log("Hay Color ver  lo demas ");

                    // tokens token dist Y
                    var token_x1 = new List<Token>();

                    while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                    {
                        Debug.Log($"Se agrega a DistY {parser.Current}");
                        token_x1.Add(parser.Current);
                        parser.NextToken();
                    }

                    #region  X1

                    var x1 = Converter.GetExpression(token_x1);
                    if (parser.Current.type == TypeToken.Coma)
                    {
                        parser.NextToken();
                        Debug.Log("Hay x1");
                        var token_y1 = new List<Token>();


                        while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                        {
                            Debug.Log($"Se agrego a Dist{parser.Current}");
                            token_y1.Add(parser.Current);
                            parser.NextToken();
                        }

                        var y1 = Converter.GetExpression(token_y1);

                        #region  Y1

                        if (parser.Current.type == TypeToken.Coma)
                        {
                            parser.NextToken();
                            Debug.Log("Hay y1");
                            var tokens_x2= new List<Token>();


                            while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                            {
                                Debug.Log($"Se agrego a ancho{parser.Current}");
                                tokens_x2.Add(parser.Current);
                                parser.NextToken();
                            }
                            var x2= Converter.GetExpression(tokens_x2);

                            #region X2

                            if (parser.Current.type == TypeToken.Coma)
                            {
                                parser.NextToken();
                                Debug.Log("Hay y2");

                                var tokens_y2 = new List<Token>();


                                while (parser.Current.type != TypeToken.CloseParenthesis)
                                {
                                    Debug.Log($"Se agrego a y2 {parser.Current.value}");
                                    tokens_y2.Add(parser.Current);
                                    parser.NextToken();
                                }
                                //para seguir el analisis
                                parser.NextToken();
                                #region  y2
                                var y2= Converter.GetExpression(tokens_y2);


                                return new GetColorCountNode(draw, color, x1, y1, x2, y2);


                                #endregion
                            }
                            else
                            {
                                parser.NextToken();
                                Debug.Log("es un error falta altura   ");
                                return new GetColorCountNode(draw, color, x1, y1, x2, null);
                            }


                            #endregion

                        }
                        else
                        {
                            parser.NextToken();
                            Debug.Log("es un error falta ancho ,altua   ");
                            return new GetColorCountNode(draw, color, x1, y1, null, null);
                        }


                        #endregion

                    }
                    else
                    {
                        parser.NextToken();
                        Debug.Log("es un error falta distancia, ancho , altura  ");
                        return new GetColorCountNode(draw, color, x1, null, null, null);
                    }


                    #endregion
                }

                else
                {
                    parser.NextToken();
                    Debug.Log("es un error falta diry ,  distancia , ancho , altura  ");
                    return new GetColorCountNode(draw, color, null, null, null, null);
                }


                #endregion
            }
            else
            return null;
        }
        
    }
}

