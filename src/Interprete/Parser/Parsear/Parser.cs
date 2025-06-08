using System.Data.Common;
using System.Runtime.CompilerServices;
using ArbolSintaxisAbstracta;
using AsignacionNodo;
using ColorFunc;
using Convertidor_Pos_Inf;
using Errores;
using Expresion;
using ExpressionesTipos;
using Go;
using IParseo;


namespace Parseando
{
    public class Parser
    {
        public static List<Error> errores_sintaxis = new();
        public List<Token> tokens;

        public Dictionary<TypeToken, List<IParse> >estructura;
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

        public Parser(List<Token> tokens, Dictionary<TypeToken, List<IParse>>? stru = null)
        {

            this.tokens = tokens;
            this.tokens.Add(new Token(TypeToken.Fin, "FIn", int.MaxValue, int.MaxValue));


            if (stru != null)
            {
                this.estructura = stru;
            }
            else
            {
                this.estructura = structure;
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
                errores_sintaxis.Add(new ExpectedType(Current.Pos, tipo.ToString(), GetNextToken().type.ToString()));
                //luego avanzar 
                NextToken();
                return false;
            }
        }




        //dicionario de expresiones boolenas 


        //aqui se guardaran el parseo de los nodos 
        Dictionary<TypeToken, List<IParse>> structure = new()
        {
            [TypeToken.Identificador] = new List<IParse>() { new LabelParse(), new AsignacionParse() },
            [TypeToken.Spawn] = new List<IParse>() { new SpawnParser() },
            [TypeToken.Color] = new List<IParse>() { new ColorParse() },
            [TypeToken.Size] = new List<IParse>() { new SizeParse() },
            [TypeToken.DrawLine] = new List<IParse>() { new DrawLineParse() },
            [TypeToken.GoTo] = new List<IParse>() { new GoToParse() },
            [TypeToken.DrawCircle] = new List<IParse>() { new DrawCircleParse() },
            [TypeToken.Fill] = new List<IParse>() { new FillParse() },
            [TypeToken.DrawRectangle] = new List<IParse>() { new DrawRectangleParse() },

            //funciones

        };



        //logica para parsear 
        public List<AstNode> Parseo()
        {
            var arbol = new List<AstNode>();
            //mientras que no se acabe los tokens 
            while (Current.type != TypeToken.Fin)
            {


                if (estructura.ContainsKey(Current.type))
                {
                    System.Console.WriteLine(Current.type);
                    //si contiene la structura  parsealo
                    var parseos = estructura[Current.type];
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