using ArbolSintaxisAbstracta;

public class Parse
{
    public List<Token> tokens;

    public int current =0 ;

    public Token? NextToken()
    {
        if(IsNext())
        {
            return tokens[current+1];
        }

        return null ;

    }
    public Token? NextToken(int pos )
    {
        if(IsNext(pos))
        return tokens[pos+1];

        return null;
    }
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



    public List<AstNode>? Parseo ()
    {
        return null ;
    }
}