
using Lector;
using lexer;
using ArbolSintaxisAbstracta;
using System.Diagnostics;
using Numero;
using Suma;
using MayorQue;


public class Program
{
    //Ruta del  editor de texto ,q sera la entrada de texto
    //static string ruta = "/home/juanca/Documentos/Pixeleando_con_Wally/Logic/Lector_de_Texto/Editor.txt";

    static void Main(string[] args)
    {



        PruebaBigger();
    }





    public static void PruebaBigger()
    {
        //verificar el cheque semantico  del bigger 

        var l = new Cadenas.String("2", (0, 0));
        var r = new SumaNode(new Number(2, (0, 0)), new Token(TypeToken.Operador, "+", 0, 0), new Number(2, (0, 0)));
        var bigger = new BiggerThan(l, new Token(TypeToken.Operador, ">", 0, 0), r);

        var semantic = bigger.CheckSemantic();
        System.Console.WriteLine(semantic);




    }


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

}


    
