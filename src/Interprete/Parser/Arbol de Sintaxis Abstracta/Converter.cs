using System.Diagnostics;
using Alcance;
using AndNodo;
using ArbolSintaxisAbstracta;
using Boolean;
using Division;
using Expresion;
using ExpressionesBinarias;
using Iguales;
using INodeCreador;
using MayorIgualQue;
using MayorQue;
using MenorIgualque;
using Menorque;
using Multiplicacion;
using NoIguales;
using Numero;
using OrNodo;
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

        #region To --- Postfix 
        public static List<Token> PostfixExpression(List<Token> infix, Dictionary<string, int> precedencia)
        {
            List<Token> postfix = new();
            //crear un pila donde guardaremos los operadores por orden de precedencia  
            Stack<Token> operadores = new Stack<Token>();

            for (int i = 0; i < infix.Count; i++)
            {

                //cuando es un identificador ..verificar q esa variable existe y es un numero 
                //ojo
                //si es un numero agregarlo directamente a la salida 
                if (infix[i].type == TypeToken.Numero)
                {
                    System.Console.WriteLine($"Numero add {infix[i].value}");
                    postfix.Add(infix[i]);
                }
                //si es un bool meterlo en la pila 
                else if (infix[i].type == TypeToken.Boolean)
                {
                    System.Console.WriteLine($"Bool add {infix[i].value}");
                    postfix.Add(infix[i]);
                }
                else if (infix[i].type == TypeToken.String)
                {
                    System.Console.WriteLine($"String add {infix[i].value}");
                    postfix.Add(infix[i]);
                }
                else if (infix[i].type == TypeToken.Identificador)
                {
                    System.Console.WriteLine($"Identificador add {infix[i].value}");
                    postfix.Add(infix[i]);
                }
                //si es ( // meterlo en la pila
                else if (infix[i].type == TypeToken.OpenParenthesis)
                {
                    System.Console.WriteLine("Open ( meterlo");
                    operadores.Push(infix[i]);
                }
                //si es ) sacar todo lo de la pila hasta llegarg al open (
                else if (infix[i].type == TypeToken.CloseParenthesis)
                {

                    //mientras que el ultimo no sea ( en la pila de operadores
                    while (operadores.Peek().type != TypeToken.OpenParenthesis)
                    {
                        //agrega el operador a la pila
                        System.Console.WriteLine($"Add {operadores.Peek().value}");
                        postfix.Add(operadores.Pop());

                    }
                    Debug.Print("Se encontro el open ");
                    var result = operadores.Pop();
                    Debug.Print(result.value);


                }
                //ir metiendo y sacando en la pila por precedencia , la pila siempre queda organizada como que elm ult elemento es el de mayor precedencia
                else if (infix[i].type == TypeToken.Operador)
                {

                    if (operadores.Count == 0)
                    {
                        System.Console.WriteLine($"Operadores vacio meter {infix[i].value}");
                        //si no hay operadores metelo 
                        operadores.Push(infix[i]);
                    }
                    //ver si tiene mayor precedencia 

                    else if (precedencia[infix[i].value] >= precedencia[operadores.Peek().value])
                    {
                        //si es mayor su precedenci metelo 
                        System.Console.WriteLine("Meter ");
                        System.Console.WriteLine($" {infix[i].value} > precedencia {operadores.Peek().value}");
                        operadores.Push(infix[i]);
                    }
                    //si tiene menor precedencia saca las cosas hasta q el quede de mayor precednecia 
                    else if (precedencia[infix[i].value] < precedencia[operadores.Peek().value])
                    {
                        //mientras q tenga menor precedencia , saca los operadoresy agregalos a la infix list
                        while (operadores.Count > 0 && precedencia[infix[i].value] < precedencia[operadores.Peek().value])
                        {
                            System.Console.WriteLine($"agregar a postfix {operadores.Peek()}");
                            postfix.Add(operadores.Pop());
                        }

                        //cuando ya tenga mayor o iugal predencia o operadores se quede vacio , agregala
                        operadores.Push(infix[i]);
                    }
                }
            }

            //cuando llegue al final , si quedan operdores en la pila , agregalos a la salida

            while (operadores.Count > 0)
            {
                postfix.Add(operadores.Pop());
            }
            System.Console.WriteLine("Dentro ");
            foreach (var item in postfix)
                System.Console.WriteLine(item.value);

            return postfix;
        }

        #endregion

        //verifica el postfix


        //para hacerlo mas extensibles , es necesario definir las expresiones bases a las cuales ir creando algo asi como las terminales 

        #region  Creando Expression
        public static Expression? Aritmetic_Bool_Expression(List<Token> postfix, Dictionary<string, INodeCreator> operadores)
        {
            Stack<Expression> pila = new();
            System.Console.WriteLine("postfix");
            foreach (var item in postfix)
                System.Console.WriteLine(item.value);


            for (int i = 0; i < postfix.Count; i++)
            {
                //numero 
                if (postfix[i].type == TypeToken.Numero)
                {
                    pila.Push(new Number(postfix[i].value, postfix[i].Pos));
                }
                //booleano 
                else if (postfix[i].type == TypeToken.Boolean)
                {
                    pila.Push(new Bool(postfix[i].value, postfix[i].Pos));
                }
                //string
                else if (postfix[i].type == TypeToken.String)
                {
                    pila.Push(new Cadenas.String(postfix[i].value, postfix[i].Pos));
                }
                //variable
                else if (postfix[i].type == TypeToken.Identificador)
                {
                    System.Console.WriteLine($"Identificador add {postfix[i].value}");
                    pila.Push(new VariableNode(postfix[i]));
                }
                
                //operadores 
                else if (postfix[i].type == TypeToken.Operador) //es un operador
                {
                    var Right = pila.Count>0? pila.Pop():null;
                    var Left = pila.Count>0 ?pila.Pop():null;

                    if (operadores.ContainsKey(postfix[i].value))
                    {
                        pila.Push(
                            operadores[postfix[i].value].CreateNode(Left, postfix[i], Right)
                            !);
                    }
                    else
                    {
                        throw new Exception($"La operacion {postfix[i].value}no esta definida");
                    }


                }
            }


            //cuando se termine todo tiene q quedar una expresion  o no 
            if (pila.Count > 0)
                return pila.Pop();


            //si no hay nada retona null expression 
            return null;
        }
        


        #endregion


        public static Expression? GetExpression(List<Token> tokens, Dictionary<string, int>? predencia = null, Dictionary<string, INodeCreator>? operadores = null)
        {
            var op = operadores == null ? op_definidas : operadores;
            var p = predencia == null ? precedence : predencia;


            var post = PostfixExpression(tokens, p);

            return Aritmetic_Bool_Expression(post, op);
        }
}



}