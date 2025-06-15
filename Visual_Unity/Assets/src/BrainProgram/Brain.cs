

//el cerebro de interprete 
using Alcance;
using ArbolSintaxisAbstracta;
using Errores;
using Evalua;
using lexer;
using Parseando;
using Semantic;
using System;
using System.Collections.Generic;
using UnityEngine;


public class Brain
{
    public bool InterpretarLenguaje(string[] codelines)
    {




        Show.On(false);
        Debug.Log("Tokenizar");
        var lexer = Tokenizar(codelines);

        Debug.Log("errores " + lexer.errores.Count);
        if (lexer.errores.Count > 0)
        {
            Debug.Log("hay errores a la hora de formar palabras ");
            //si hay errores lexicos imprimeros 
            Show.Clean();
            foreach (var e in lexer.errores)
                Show.ShowError(e.message);

            
            Show.On(true);
            return false;
        }
        else
        {
            Debug.Log("Parsear");
            var result = Parseo(lexer.tokens);

            if (result.Item2.Count > 0)
            {
                Debug.Log("hay errores en la sintaxis Parseo");
                //hay errores imprimirlos 
                Show.Clean();
                foreach (var e in result.Item2)
                    Show.ShowError(e.message);

                
                Show.On(true);
                return false;
            }
            else
            {
                Debug.Log("Analisis Semantico ");
                //analizar semanticamente 
                var check_semantic = CheckSemantic(result.Item1!);
                if ( check_semantic== false )
                {
                    Debug.Log("Hay errores en  el analisis semantico ");

                    Show.Clean();
                    foreach (var e in AstNode.compilingError)
                        Show.ShowError(e.message);

                    //imprimir los  errres del compiling error ast node 
                    //AstNode.compilingError
                    Show.On(true);
                    return false;
                }
                else 
                {
                    Debug.Log("Evaluar las expresiones");
                    //evaluar 
                    var evaluador = Evaluate(result.Item1!);

                    if (evaluador.error_evaluate.Count > 0)
                    {

                        Debug.Log("Hay error en evaluar las expresiones");
                        Show.Clean();
                        foreach (var e in evaluador.error_evaluate)
                            Show.ShowError(e.message);

                        //imprimir los errores 
                        Show.On(true);
                        return false;
                    }
                    else
                    {
                        Debug.Log("Todo termino oka");

                        Debug.Log("Termino de Evaluar ");
                        Debug.Log("Interpreto correctamente");
                        Show.On(false);
                        return true;
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
        Debug.Log("Parseando");



        var parser = new Parser(tokens);
        var nodos = parser.Parseo();


        return (nodos, Parser.errores_sintaxis);
    }

    public static bool CheckSemantic(List<AstNode> nodos)
    {
        Debug.Log("///////////////////////////");
        Debug.Log("///////////////////////////");
        Debug.Log("////////Chequeo Semantico //////////");

        Semantico semantico = new(nodos);
        return semantico.CheckSemantic();
    }
    


        public static Evaluator Evaluate(List<AstNode> nodos)
    {
        Debug.Log("///////////////////////////");
        Debug.Log("///////////////////////////");
        Debug.Log("////////Evaluando//////////");
        Debug.Log("///////////////////////////");
        Debug.Log("///////////////////////////");


        Evaluator evaluator = new Evaluator(nodos);
        evaluator.Evaluar();
        return evaluator;
    }
}