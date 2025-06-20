using UnityEngine;
using Alcance;

using ArbolSintaxisAbstracta;
using Boolean;
using Cadenas;

using Errores;
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;

using INodeCreador;
using IParseo;

using Numero;

using Paleta_Colores;
using Parseando;

using TerminalesNode;
using vars;
using System;
using System.Collections;
using System.Collections.Generic;
using Arbol_de_Sintaxis_Abstracta;


namespace Convertidor_Pos_Inf
{
    //Converter de infix to posfix
    public class Converter
    {
        #region  Dic Precedencia
        public static Dictionary<string, int> precedence = new()
        {

            ["("] = -1,
            ["||"] = 1,  // OR lógico (↑ precedencia)
            ["&&"] = 2,  // AND lógico (↑ precedencia)
            ["=="] = 3,  // (↑ precedencia)
            ["!="] = 3,
            [">"] = 4,   // (↑ precedencia)
            ["<"] = 4,
            [">="] = 4,
            ["<="] = 4,
            ["+"] = 5,   // (igual)
            ["-"] = 5,
            ["*"] = 6,   // (↑ precedencia)
            ["/"] = 6,
            ["%"] = 6,
            ["**"] = 7,  // (↑ precedencia)

        };

        #endregion


        #region Dic Nodos 

        public static Dictionary<string, INodeCreator> op_definidas = new()
        {
            ["||"] = new OrNodeCreator(),
            ["&&"] = new AndNodeCreator(),
            ["=="] = new EqualNodeCreator(),
            ["!="] = new NotEqualsNodeCreator(),
            [">"] = new BiggerThanNodeCreator(),
            ["<"] = new LessThanNodeCreator(),
            [">="] = new BiggerEqualThanNodeCreator(),
            ["<="] = new LessEqualThanNodeCreator(),
            ["+"] = new SumaNodeCreator(),
            ["-"] = new RestaNodeCreator(),
            ["*"] = new MultiplicacionNodeCreator(),
            ["/"] = new DivisionNodeCreator(),
            ["%"] = new RestoNodeCreator(),
            ["**"] = new PowNodeCreator(),
        };

        #endregion



        #region  Dic Expressiones
        //diccinari0 de los postfix 
        public static Dictionary<TypeToken, List<IParse>> parsea = new()
        {
            [TypeToken.GetActualX] = new List<IParse>() { new GetActualXParse() },
            [TypeToken.GetActualX] = new List<IParse>() { new GetActualXParse() },
            [TypeToken.GetActualY] = new List<IParse>() { new GetActualYParse() },
            [TypeToken.GetColorCount]= new List<IParse>() { new GetColorCountParse()},
            [TypeToken.GetCanvasSize] = new List<IParse>() { new GetCanvasSizeParse() },
            [TypeToken.Boolean] = new List<IParse>() { new BoolParse() },
            [TypeToken.Numero] = new List<IParse>() { new NumberParse() },
            [TypeToken.String] = new List<IParse>() { new StringParse() },
            [TypeToken.color] = new List<IParse>() { new ColorParse() },
            [TypeToken.Operador] = new List<IParse>() { new OperadorParse(), new NegativeParse() },
            [TypeToken.OpenParenthesis] = new List<IParse>() { new ParentesisParse() },
            [TypeToken.CloseParenthesis] = new List<IParse>() { new ParentesisParse() },
            [TypeToken.Identificador] = new List<IParse>() { new VariableParse() },
            [TypeToken.IsBrushSize] = new List<IParse>() { new IsBrushSizeParse() },
            [TypeToken.IsBrushColor] = new List<IParse>() { new IsBrushColorParse()},
        };


        #endregion



        //parsea en expresione todo sin organizarlo en Postfix
        private static List<AstNode> ParsearInfix(List<Token> tokens)
        {
            Debug.Log("Parsear el infix");
            //parsear los  tokens en infix 

            Parser parser = new Parser(tokens, parsea);

            var nodos = parser.Parseo();

            Debug.Log("Nodos");
            Debug.Log(nodos.Count);


            return nodos;
        }







        // organiza las expresiones en postfix

        #region To --- Postfix 
        private static List<Expression> PostfixExpression(List<AstNode> infix, Dictionary<string, int> precedencia)
        {
            Debug.Log("converter to postfix ");
            List<Expression> postfix = new();
            //crear un pila donde guardaremos los operadores por orden de precedencia  

            Stack<Expression> operadores = new();
            for (int i = 0; i < infix.Count; i++)
            {
                //logica para ordenar los operadores en orden de precedencia 
                if (infix[i].GetTipo() == ExpressionTypes.Operator)
                {
                    Debug.Log("Operador");
                    if (operadores.Count == 0)
                    {
                        Debug.Log($"Operadores vacio meter {infix[i].Evaluate()}");
                        //si no hay operadores metelo 
                        operadores.Push((Expression)infix[i]);
                    }
                    //ver si tiene mayor precedencia 

                    else
                    {
                        var op = (Token)infix[i]!.Evaluate()!;
                        var peek = (Token)operadores.Peek().Evaluate()!;

                        if (precedencia[op.value] >= precedencia[peek.value])
                        {
                            //si es mayor su precedenci metelo 
                            Debug.Log("Meter ");
                            Debug.Log($" {op.value} > precedencia {peek.value}");
                            operadores.Push((Expression)infix[i]);
                        }
                        //si tiene menor precedencia saca las cosas hasta q el quede de mayor precednecia  // siempre y cuando sea diferene del parentesis
                        else if (precedencia[op.value] < precedencia[peek.value])
                        {
                            //mientras q tenga menor precedencia , saca los operadoresy agregalos a la infix list && q no se esta comparando con el parenteis 
                            while (operadores.Count >0   && precedencia[op.value] < precedencia[peek.value])
                            {
                                Debug.Log($"agregar a postfix {operadores.Peek()}");
                                postfix.Add(operadores.Pop());
                                //actualizar el peek
                                if(operadores.Count >0)
                                peek =(Token) operadores.Peek().Evaluate();
                            }

                            //cuando ya tenga mayor o iugal predencia o operadores se quede vacio , agregala
                            operadores.Push((Expression)infix[i]);
                        }
                    }
                }

                //logica de asimilacion de parentesis 
                else if (infix[i].GetTipo() == ExpressionTypes.OpenParent)
                {
                    Debug.Log("Open ( meterlo");
                    operadores.Push((Expression)infix[i]);
                }
                //si es ) sacar todo lo de la pila hasta llegarg al open (
                else if (infix[i].GetTipo() == ExpressionTypes.CloseParent)
                {
                    //mientras que el ultimo no sea ( en la pila de operadores
                    Debug.Log("operadores");
                    foreach (var item in operadores)
                    {
                        var tok =(Token)item.Evaluate();
                        Debug.Log(tok.value);
                    }

                        while (operadores.Count > 0 && operadores.Peek().GetTipo() != ExpressionTypes.OpenParent)
                        {
                            //agrega el operador a la pila
                            Debug.Log($"Add  pila {operadores.Peek().value}");
                            postfix.Add(operadores.Pop());


                        }
                        //Sacar el parentesis
                        Debug.Log("Se encontro el open ");
                        var result = operadores.Pop();
                        // Debug.Log(result.Evaluate()!.ToString());
                    

                }
                else
                {
                    //es cualquier cosa de las terminal expression meterlo a la pila 
                    //Debug.Log($"exp add {infix[i].Evaluate()}");
                    postfix.Add((Expression)infix[i]);
                }
            }

            //cuando llegue al final , si quedan operdores en la pila , agregalos a la salida

            while (operadores.Count > 0)
            {
                postfix.Add(operadores.Pop());
            }
            Debug.Log("Dentro ");

            foreach (var item in postfix)
            {
                Debug.Log(item);
            }

            return postfix;
        }

        #endregion

        //verifica el postfix


        //para hacerlo mas extensibles , es necesario definir las expresiones bases a las cuales ir creando algo asi como las terminales 

        #region  Creando Expression
        private  static Expression? Aritmetic_Bool_Expression(List<Expression> postfix, Dictionary<string, INodeCreator> operadores)
        {
            Debug.Log("creando del postfix a una expressio");
            Stack<Expression> pila = new();
            
                


            for (int i = 0; i < postfix.Count; i++)
            {
                
                //operadores 
                if (postfix[i].GetTipo() == ExpressionTypes.Operator) //es un operador
                {
                    var Right = pila.Count > 0 ? pila.Pop() : null;
                    var Left = pila.Count > 0 ? pila.Pop() : null;
                    var tok = (Token)postfix[i]!.Evaluate()!;
                    if (operadores.ContainsKey(tok.value))
                    {

                        pila.Push(
                            operadores[tok.value].CreateNode(Left, tok, Right)
                            !);
                    }
                    else
                    {
                        throw new Exception($"La operacion {tok.value}no esta definida");
                    }


                }//cualquier otra cosa es una expresion meterla a la pila 
                else
                {
                    pila.Push(postfix[i]);
                }
            }


            //cuando se termine todo tiene q quedar una expresion  o no 
            if (pila.Count > 0)
            {
                var exp = pila.Pop();


                if (pila.Count > 0)
                {
                    Debug.Log("Se retorno una exp nula ");
                    //hay elementos aun en la pila es una null expresion 
                    return null;
                }
                else
                {
                    Debug.Log("se retorno una exp");
                    return exp;
                }
            }

            //si no hay nada retona null expression 
            return null;
        }
        


        #endregion


        public static Expression? GetExpression(List<Token> tokens, Dictionary<string, int>? predencia = null, Dictionary<string, INodeCreator>? operadores = null)
        {
            var op = operadores == null ? op_definidas : operadores;
            var p = predencia == null ? precedence : predencia;

            var expressiones = ParsearInfix(tokens);
            var post = PostfixExpression(expressiones, p);

            return Aritmetic_Bool_Expression(post, op);
        }
}



}