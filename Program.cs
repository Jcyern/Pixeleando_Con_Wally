
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


            Evaluate(nodos);



        }





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

        var parser = new Parser(tokens);
        var nodos = parser.Parseo();


        if (parser.errores_sintaxis.Count > 0)
        {
            System.Console.WriteLine("Hay errores sintacticos ");
            foreach (var e in parser.errores_sintaxis)
            {
                e.ShowError();
            }
            return null;
        }
        else
            return nodos;
    }



    //verificar la sintaxis de dichas estructuras 
    public static bool CheckSemantic(List<AstNode> nodos)
    {
        System.Console.WriteLine("//////////////////////");
        System.Console.WriteLine("//////////////////////");
        System.Console.WriteLine("Chequear Semanticamente");
        System.Console.WriteLine("//////////////////////");
        System.Console.WriteLine("//////////////////////");

        foreach (var node in nodos)
        {
            node.CheckSemantic();
        }

        if (AstNode.compilingError.Count > 0)
        {
            foreach (var item in AstNode.compilingError)
            {
                item.ShowError();
            }
            return false;
        }
        return true;
    }





    //evaluar las estructuras creadas
    public static void Evaluate(List<AstNode> nodos)
    {
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("////////Evaluando//////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");
        foreach (var nodo in nodos)
        {
            System.Console.WriteLine("Evaluando Nodo");
            nodo.Evaluate();
        }
    }

}


    
