using System.Data.Common;
using System.Runtime.CompilerServices;
using ArbolSintaxisAbstracta;
using ColorFunc;
using Convertidor_Pos_Inf;
using Errores;
using Expresion;
using ExpressionesTipos;
using IParseo;
using NodosParser;

namespace Parseando
{
    public class Parser
    {
        public List<Error> errores_sintaxis;
        public List<Token> tokens;

        public int line;

        public int current = 0;

        public Token Current => tokens[current];

        //da el siguiente y se mueve 
        public Token NextToken()
        {
            if (IsNext())
            {
                current += 1;
                return tokens[current];
            }

            return new Token(TypeToken.InvalidToken, "null", tokens[current].fila, tokens[current].columna);

        }
        //te da el siguiente sin cambiar el current  
        public Token GetNextToken()
        {
            if (IsNext())
            {
                return tokens[current + 1];
            }

            return new Token(TypeToken.InvalidToken, "null", tokens[current].fila, tokens[current].columna);

        }
        //te da el siguiente de esa  posicion sin cmabiar el current 
        public Token GetNextToken(int pos)
        {
            if (IsNext(pos))
                return tokens[pos + 1];

            return new Token(TypeToken.InvalidToken, "null", tokens[pos].fila, tokens[pos].columna);
        }

        //verifica si al siguientes elementos
        public bool IsNext()
        {
            if (current + 1 < tokens.Count)
            {
                return true;
            }
            return false;
        }

        //veirfica si en esa pos hay un siguiente elemento 
        public bool IsNext(int actualpos)
        {
            if (actualpos + 1 < tokens.Count)
                return true;

            return false;

        }

        public Parser(List<Token> tokens, Dictionary<TypeToken, List<IParse>>? structure = null)
        {
            this.tokens = tokens;
            this.errores_sintaxis = new();

            if (structure != null)
            {
                this.structure = structure;
            }
        }

        public bool ExpectedTokenType(TypeToken tipo)
        {
            //verifica si la sig pos tiene ese token o sino crea  un error
            if (GetNextToken().type == tipo)
            {
                //si es asi
                //pasar a sig token 
                NextToken();
                return true;
            }
            else
            {
                //avanazar y guardar error 
                errores_sintaxis.Add(new ExpectedType(Current.Pos, tipo.ToString(), Current.type.ToString()));
                //luego avanzar 
                NextToken();
                return false;
            }
        }




        //dicionario de expresiones boolenas 


        //aqui se guardaran el parseo de los nodos 
        Dictionary<TypeToken, List<IParse>> structure = [];
        public void RegisterParser()
        {
            System.Console.WriteLine("Registrando parseos");
            structure[TypeToken.Identificador] = new List<IParse>() { new AsignacionParse() };
            structure[TypeToken.Spawn] = new List<IParse>() { new SpawnParser() };
            structure[TypeToken.Color] = new List<IParse>() { new ColorParse() };
            structure[TypeToken.Size] = new List<IParse>() { new SizeParse() };
        }



        //logica para parsear 
        public List<AstNode> Parseo()
        {
            RegisterParser();
            var arbol = new List<AstNode>();
            //mientras que no se acabe los tokens 
            while (Current.type != TypeToken.Fin)
            {


                if (structure.ContainsKey(Current.type))
                {
                    //si contiene la structura  parsealo
                    var parseos = structure[Current.type];
                    AstNode? nodo = null;
                    foreach (var p in parseos)
                    {
                        nodo = p.Parse(this);
                        if (nodo != null)
                        {
                            break;
                        }
                    }

                    if (nodo == null)
                    {
                        errores_sintaxis.Add(new SintaxisError(Current.Pos, Current.value));
                        NextToken();
                    }
                    else
                        arbol.Add(nodo);

                }
                else
                {
                    if (Current.type != TypeToken.Fin)
                    {
                        System.Console.WriteLine(Current.value);
                        //consideralo un error 
                        errores_sintaxis.Add(new SintaxisError(Current.Pos, Current.value));
                        NextToken();

                    }
                }
            }

            return arbol;
        }

        //Parsear que se le pasan los tokens los parsea y devuelve nodos del arbol de sintaxis abstracta 

    }


}