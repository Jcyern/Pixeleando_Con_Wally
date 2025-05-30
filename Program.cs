
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


public class Program
{
    //Ruta del  editor de texto ,q sera la entrada de texto
    //static string ruta = "/home/juanca/Documentos/Pixeleando_con_Wally/Logic/Lector_de_Texto/Editor.txt";

    static void Main(string[] args)
    {
        var lexer = Tokenizar();
        
        var result = Converter.PostfixExpression(lexer.tokens, Converter.precedence);

        System.Console.WriteLine("resultado");
        foreach (var item in result)
        {
            System.Console.WriteLine(item.value);
        }
    }





    public static void PruebaBigger()
    {
        //verificar el cheque semantico  del bigger 
        
        Expression r = new SumaNode(new Number(2, (0, 0)), new Token(TypeToken.Operador, "+", 0, 0), new Number(5, (0, 0)));


        var result = r.Evaluate();
        
        System.Console.WriteLine(result);
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


    
