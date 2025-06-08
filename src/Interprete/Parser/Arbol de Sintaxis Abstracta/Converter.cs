using System.Diagnostics;
using Alcance;
using AndNodo;
using ArbolSintaxisAbstracta;
using Boolean;
using Cadenas;
using Division;
using Errores;
using Expresion;
using ExpressionesBinarias;
using ExpressionesTipos;
using Iguales;
using INodeCreador;
using IParseo;
using MayorIgualQue;
using MayorQue;
using MenorIgualque;
using Menorque;
using Multiplicacion;
using NoIguales;
using Numero;
using OrNodo;
using Paleta_Colores;
using Parseando;
using Pow;
using Resta;
using Resto;
using Suma;
using TerminalesNode;
using vars;


namespace Convertidor_Pos_Inf
{
    //Converter de infix to posfix
    public class Converter
    {
        #region  Dic Precedencia
        public static Dictionary<string, int> precedence = new()
        {
            ["("] = -1,//en este caso la menor precedencia para q a la hora de q venga cualquier operador pueda entrar a la pila  y siga el trancurso de la con version sin  afectar el orden de las demas precedencias , ejemplo q tenga (5+4)   y a la hora de comparar + con (  no salga el (  y entre el mas a la pila ----- ya q en el lenguaje postfix no afecta el parentesis 
            ["||"] = 0, //booleanos
            ["&&"] = 1, //booleanos
            ["=="] = 2,
            ["!="] = 2,
            [">"] = 3,
            ["<"] = 3,
            [">="] = 3,
            ["<="] = 3,  //comparaciones 
            ["+"] = 4,
            ["-"] = 4,
            ["*"] = 5,
            ["/"] = 5,
            ["%"] = 5,
            ["**"] = 6,

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
            [TypeToken.GetCanvasSize] = new List<IParse>() { new GetCanvasSizeParse() },
            [TypeToken.Boolean] = new List<IParse>() { new BoolParse() },
            [TypeToken.Numero] = new List<IParse>() { new NumberParse() },
            [TypeToken.String] = new List<IParse>() { new StringParse() },
            [TypeToken.color] = new List<IParse>() { new ColorParse() },
            [TypeToken.Operador] = new List<IParse>() { new OperadorParse() },
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
            System.Console.WriteLine("Parsear el infix");
            //parsear los  tokens en infix 
            Parser parser = new Parser(tokens, parsea);

            return parser.Parseo();
        }







        // organiza las expresiones en postfix

        #region To --- Postfix 
        private static List<Expression> PostfixExpression(List<AstNode> infix, Dictionary<string, int> precedencia)
        {
            System.Console.WriteLine("converter to postfix ");
            List<Expression> postfix = new();
            //crear un pila donde guardaremos los operadores por orden de precedencia  
            Stack<Expression> operadores = new();
            for (int i = 0; i < infix.Count; i++)
            {
                //logica para ordenar los operadores en orden de precedencia 
                if (infix[i].GetTipo() == ExpressionTypes.Operator)
                {
                    System.Console.WriteLine("Operador");
                    if (operadores.Count == 0)
                    {
                        System.Console.WriteLine($"Operadores vacio meter {infix[i].Evaluate()}");
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
                            System.Console.WriteLine("Meter ");
                            System.Console.WriteLine($" {op.value} > precedencia {peek.value}");
                            operadores.Push((Expression)infix[i]);
                        }
                        //si tiene menor precedencia saca las cosas hasta q el quede de mayor precednecia 
                        else if (precedencia[op.value] < precedencia[peek.value])
                        {
                            //mientras q tenga menor precedencia , saca los operadoresy agregalos a la infix list
                            while (operadores.Count > 0 && precedencia[op.value] < precedencia[peek.value])
                            {
                                System.Console.WriteLine($"agregar a postfix {operadores.Peek()}");
                                postfix.Add(operadores.Pop());
                            }

                            //cuando ya tenga mayor o iugal predencia o operadores se quede vacio , agregala
                            operadores.Push((Expression)infix[i]);
                        }
                    }
                }

                //logica de asimilacion de parentesis 
                else if (infix[i].GetTipo() == ExpressionTypes.OpenParent)
                {
                    System.Console.WriteLine("Open ( meterlo");
                    operadores.Push((Expression)infix[i]);
                }
                //si es ) sacar todo lo de la pila hasta llegarg al open (
                else if (infix[i].GetTipo() == ExpressionTypes.CloseParent)
                {

                    //mientras que el ultimo no sea ( en la pila de operadores
                    while (operadores.Peek().GetTipo() != ExpressionTypes.OpenParent)
                    {
                        //agrega el operador a la pila
                        System.Console.WriteLine($"Add {operadores.Peek().value}");
                        postfix.Add(operadores.Pop());

                    }
                    Debug.Print("Se encontro el open ");
                    var result = operadores.Pop();
                   // Debug.Print(result.Evaluate()!.ToString());


                }
                else
                {
                    //es cualquier cosa de las terminal expression meterlo a la pila 
                    //System.Console.WriteLine($"exp add {infix[i].Evaluate()}");
                    postfix.Add((Expression)infix[i]);
                }
            }

            //cuando llegue al final , si quedan operdores en la pila , agregalos a la salida

            while (operadores.Count > 0)
            {
                postfix.Add(operadores.Pop());
            }
            System.Console.WriteLine("Dentro ");
            return postfix;
        }

        #endregion

        //verifica el postfix


        //para hacerlo mas extensibles , es necesario definir las expresiones bases a las cuales ir creando algo asi como las terminales 

        #region  Creando Expression
        private  static Expression? Aritmetic_Bool_Expression(List<Expression> postfix, Dictionary<string, INodeCreator> operadores)
        {
            System.Console.WriteLine("creando del postfix a una expressio");
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
            {System.Console.WriteLine("se retorno una exp");
                return pila.Pop();
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