
using Lector;
using lexer;
using ArbolSintaxisAbstracta;
using System.Diagnostics;
using Numero;
using Suma;
using MayorQue;
using System.Dynamic;
using Expresion;
using Convertidor_Pos_Inf;
using System.Runtime.ConstrainedExecution;
using Parseando;
using Evalua;
using Semantic;


public class Program
{
    //Ruta del  editor de texto ,q sera la entrada de texto
    //static string ruta = "/home/juanca/Documentos/Pixeleando_con_Wally/Logic/Lector_de_Texto/Editor.txt";

    static void Main(string[] args)
    {
        var lexer = Tokenizar();

        if (lexer.errores.Count > 0)
        {
            foreach (var error in lexer.errores)
            {
                error.ShowError();
            }

        }
        else
        {
            //si no hay errores parsear 

            var nodos = Parseo(lexer.tokens);


            //si no hay nodos es q hay errores
            if (nodos == null)
                return;

            var result = CheckSemantic(nodos);
            System.Console.WriteLine("///////////////////////");
            System.Console.WriteLine($"Result Chequeo {result}");
            System.Console.WriteLine("////////////////////////");

            if (!result)
            {
                System.Console.WriteLine("Parar Ejecucion por Errores Semanticos ");
                return;
            }


            var evalute = Evaluate(nodos);

            if (evalute)
            {
                System.Console.WriteLine("/////////////////////////////////////");
                System.Console.WriteLine("/////////////////////////////////////");
                System.Console.WriteLine("/////////////////////////////////////");
                System.Console.WriteLine("Su codigo es valido Felicitaciones");
            }
            else
            {
                System.Console.WriteLine("???????????????????????????????????/");
                System.Console.WriteLine("Codigo Invalido Verifique los errores de la Evaluacion");
            }


        }
    }



    //evaluar las estructuras creadas
    public static bool Evaluate(List<AstNode> nodos)
    {
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("////////Evaluando//////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");


        Evaluator evaluator = new Evaluator(nodos);
        return evaluator.Evaluar();
    }

    public static bool CheckSemantic(List<AstNode> nodos)
    {
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("////////Chequeo Semantico //////////");

        Semantico semantico = new(nodos);
        return semantico.CheckSemantic();
    }



    

    //Paso 1 Dividir en Tokens
    public static Lexer Tokenizar()
    {
        LectorText lector = new LectorText();

        if (lector.Lines != null)
        {
            Lexer lexer = new Lexer(lector.Lines);
            lexer.Tokenizar();

            Debug.Print("Tokenizo");
            var tokens = lexer.tokens;
            var errores = lexer.errores;

            System.Console.WriteLine("Tokens");
            foreach (var token in tokens)
            {
                System.Console.WriteLine($"F:{token.fila} C:{token.columna} type:{token.type} value {token.value}");
            }
            if (errores.Count > 0)
            {
                System.Console.WriteLine("Errores");
                foreach (var error in errores)
                {
                    error.ShowError();
                }
            }

            return lexer;


        }

        throw new Exception("No hay  lineas escritas en el lector de texto ");


    }



    //Crear estructuras sintaxticas
    public static List<AstNode>? Parseo(List<Token> tokens)
    {
        System.Console.WriteLine("Parseando");

        Parser.errores_sintaxis = new List<Errores.Error>();
        var parser = new Parser(tokens);
        var nodos = parser.Parseo();


        if (Parser.errores_sintaxis.Count > 0)
        {
            System.Console.WriteLine("Hay errores sintacticos ");
            foreach (var e in Parser.errores_sintaxis)
            {
                e.ShowError();
            }
            return null;
        }
        else
            return nodos;
    }

}

