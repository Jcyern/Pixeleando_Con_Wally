using System.Diagnostics;
using Lector;
using lexer;

public class Program 
{
    //Ruta del  editor de texto ,q sera la entrada de texto
   //static string ruta = "/home/juanca/Documentos/Pixeleando_con_Wally/Logic/Lector_de_Texto/Editor.txt";
    
    static void Main(string[] args)
    {
        LectorText lector = new LectorText();

        if(lector.Lines!= null)
        {
            Lexer lexer = new Lexer(lector.Lines);
            lexer.Tokenizar();
            Debug.Print("Tokenizo");
            var tokens = lexer.tokens;
            var errores = lexer.errores;

            System.Console.WriteLine("Tokens");
            foreach( var token in tokens)
            {
                System.Console.WriteLine($"F:{token.fila} C:{token.columna} type:{token.type} value {token.value}");
            }
            if(errores.Count>0)
            System.Console.WriteLine("Errores");
            foreach( var error in errores )
            {
                error.ShowError();
            }
        }


    }
}