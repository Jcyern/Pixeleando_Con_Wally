using ArbolSintaxisAbstracta;
using Expresion;

public class Parse
{
    public List<Token> tokens;

    public int line ;

    public int current =0 ;

    //da el siguiente y se mueve 
    public Token NextToken()
    {
        if(IsNext())
        {
            current+=1;
            return tokens[current];
        }

        return new Token(TypeToken.InvalidToken, "null",tokens[current].fila, tokens[current].columna);

    }
    //te da el siguiente sin cambiar el current  
    public Token  GetNextToken()
    {
        if(IsNext())
        {
            return tokens[current+1];
        }

        return new Token(TypeToken.InvalidToken, "null",tokens[current].fila, tokens[current].columna);

    }
    public Token GetNextToken(int pos )
    {
        if(IsNext(pos))
        return tokens[pos+1];

        return new Token(TypeToken.InvalidToken, "null",tokens[pos].fila, tokens[pos].columna);
    }

    //verifica si al siguientes elementos
    public bool IsNext()
    {
        if(current+1 < tokens.Count)
        {
            return true ;
        }
        return false ;
    }

    public bool IsNext  (int actualpos  )
    {
        if(actualpos +1<tokens.Count )
        return true ;

        return false ;

    }

    public Parse(List<Token> tokens )
    {
        this.tokens = tokens;
    }


    //Parsear que se le pasan los tokens los parsea y devuelve nodos del arbol de sintaxis abstracta 

    public List<AstNode>? Parseo ()
    {
        List<AstNode> nodos = new();

        while ( current < tokens.Count )
        {
            if(tokens[current].type == TypeToken.Identificador && GetNextToken().type == TypeToken.Asignacion)
            {
                var name = tokens[current];
                var identificador = NextToken();

               // Expression? expression ;

                //hay q buscar la expression q se da



                //si es string 




                //si es un bool 


                //si es una expresion numerica
                if(GetNextToken().type == TypeToken.Numero)
                {
                    //puede coger tokens hasta q el siguiente q venga sea el salto de linea 
                    
                    //agregar al numero actual
                    List<Token> toks =  [NextToken()];
                    while (GetNextToken().type!= TypeToken.InvalidToken && GetNextToken().fila == tokens[current].fila)
                    {
                        toks.Add(NextToken());
                    }

                    //pasarle lso tokens para ver si los puedes covertir en una expresion aritmetica 
                }
                
            }
        }





        return null ;
    }
}