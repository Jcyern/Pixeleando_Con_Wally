using ArbolSintaxisAbstracta;
using Expresion;

public class Parse
{



    public List<Token> tokens;

    public int line;

    public int current = 0;

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

    public bool IsNext(int actualpos)
    {
        if (actualpos + 1 < tokens.Count)
            return true;

        return false;

    }

    public Parse(List<Token> tokens)
    {
        this.tokens = tokens;
    }


    //dicionario de expresiones boolenas 






    //Parsear que se le pasan los tokens los parsea y devuelve nodos del arbol de sintaxis abstracta 

    public List<AstNode>? Parseo()
    {
        List<AstNode> nodos = new();


        //recorriendon la lista de tokens ya creada 
        while (current < tokens.Count)
        {
            //asignacion
            if (tokens[current].type == TypeToken.Identificador && GetNextToken().type == TypeToken.Asignacion)
            {
                //crear logica 
            }

            //viendo expresines numericas || booleanas  en casos como 
            //q empiece por un numero
            // q empiece por un ( y luego le tiene q seguir un numero 

            //se supone q cuando asignemos un numero luego detras de el no venga mas nada 
            ///por ende la logica es ir poniendo numeros , parentesis , operadores , hasta que halla un cambio de linea ( que es cuando se termina la expresion)
            if (tokens[current].type == TypeToken.Numero || tokens[current].type == TypeToken.OpenParenthesis && GetNextToken().type == TypeToken.Numero)
            {
                //vamos a pensar q solo hay numeros , en el caso de que haya operadores booleanos y la convertiremos en booleana 

                var exp = new List<Token>() { tokens[current] };

                //verifca si hay sig y forma parte de la instruccion 
                while (IsNext() && GetNextToken().fila == exp[^1].fila)
                {
                    exp.Add(NextToken());
                }
                
                //luego de eso crear la expresion Aritmetica - Booleana 


            }
        }





        return null;
    }



    public AstNode Arit_Bool_Parse(List<Token> tokens)
    {
        //logica para devolver xpresionn aritemtic 
        return null;
    }
}