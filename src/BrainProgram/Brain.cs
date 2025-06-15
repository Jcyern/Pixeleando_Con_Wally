

//el cerebro de interprete 
using ArbolSintaxisAbstracta;
using Errores;
using Evalua;
using lexer;
using Parseando;
using Semantic;

public class Brain
{
    public void InterpretarLenguaje(string[] codelines)
    {
        var lexer = Tokenizar(codelines);

        if (lexer.errores.Count > 0)
        {
            //si hay errores lexicos imprimeros 
        }
        else
        {
            var result = Parseo(lexer.tokens);

            if (result.Item2.Count > 0)
            {
                //hay errores imprimirlos 
            }
            else
            {
                //analizar semanticamente 
                if (!CheckSemantic(result.Item1!))
                {
                    //imprimir los  errres del compiling error ast node 
                    //AstNode.compilingError
                }
                else
                {
                    //evaluar 
                    var evaluador = Evaluate(result.Item1!);

                    if (evaluador.error_evaluate.Count > 0)
                    {
                        //imprimir los errores 
                    }
                    else
                    {

                        System.Console.WriteLine("Termino de Evaluar ");
                        System.Console.WriteLine("Interpreto correctamente");
                    }
                }
            }
        }
    }


    public Lexer Tokenizar(string[] lines)
    {
        Lexer lexer = new Lexer(lines);
        lexer.Tokenizar();
        return lexer;
    }

    //parsear las expresiones 

    //Crear estructuras sintaxticas
    public static (List<AstNode>?, List<Error>) Parseo(List<Token> tokens)
    {
        System.Console.WriteLine("Parseando");

        Parser.errores_sintaxis = new List<Errores.Error>();
        var parser = new Parser(tokens);
        var nodos = parser.Parseo();


        return (nodos, Parser.errores_sintaxis);
    }

    public static bool CheckSemantic(List<AstNode> nodos)
    {
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("////////Chequeo Semantico //////////");

        Semantico semantico = new(nodos);
        return semantico.CheckSemantic();
    }
    


        public static Evaluator Evaluate(List<AstNode> nodos)
    {
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("////////Evaluando//////////");
        System.Console.WriteLine("///////////////////////////");
        System.Console.WriteLine("///////////////////////////");


        Evaluator evaluator = new Evaluator(nodos);
        evaluator.Evaluar();
        return evaluator;
    }
}