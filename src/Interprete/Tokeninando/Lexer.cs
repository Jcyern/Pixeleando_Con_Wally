
using System.Diagnostics;
using Errores;
namespace lexer
{
    public class Lexer 
    {
        string [] lineas ;

        public List<Error> errores ;
        public List<Token> tokens ;



        Dictionary<string , TypeToken> keywords = new Dictionary<string, TypeToken>
        {
            ["Spawn"] = TypeToken.Spawn,
            ["Color"] = TypeToken.Color,
            ["Size"] = TypeToken.Size,
            ["DrawLine"] = TypeToken.DrawLine,
            ["DrawCircle"] = TypeToken.DrawCircle,
            ["DrawRectangle"] = TypeToken.DrawRectangle,
            ["Fill"] = TypeToken.Fill,
            ["GetActualX"] = TypeToken.GetActualX,
            ["GetActualY"] = TypeToken.GetActualY,
            ["GetCanvasSize"] = TypeToken.GetCanvasSize,
            ["GetColorCount"] = TypeToken.GetColorCount,
            ["IsBrushColor"] = TypeToken.IsBrushColor,
            ["IsBrushSize"] = TypeToken.IsBrushSize,
            ["IsCanvasColor"] = TypeToken.IsCanvasColor,
            ["GoTo"] = TypeToken.GoTo,
            ["true"] = TypeToken.True,
            ["false"] = TypeToken.False
        };

        int pos;
        int line ;
        char current ;  //altual caracter 



        public void   NextChar ()  //dame el siguiente caracter 
        {

            if(IsNext() == true )
            {
                pos+= 1; 
                current= lineas [line-1][pos];
            }
            else
            {
                pos+=1;
                current = '?';
            }
            //sino no hace nada 
        }

        public char GetNext (int position = -1)
        {  
            if(position == -1)
            {
                if(IsNext())
                {
                    return lineas[line-1][pos+1];

                }
            }
            else if (IsNext(position))
            {
                return lineas[line-1][position+1];
            }

            return '?';
        }

        public bool IsNext(int position = -1) //hay siguiente 
        { 
            if(position == -1)
            {    
                if(pos+1 < lineas[line-1].Length)
                return true ;
            }
            else if(position+1 <lineas[line-1].Length)
            {
                return true ;
            }
        
            return false ;
        }


        private int CalcularColumna(int fila )
        {
            if( tokens.Count== 0 ||tokens[tokens.Count-1].fila != fila)
            {  //sino hay elementos , es la fila 0, columna 0
                return 1;
            }

            else  //si estan la misma fila , sumale uno a la columna 
            {
                return tokens[tokens.Count-1].columna+1;
            }
        }
        



        //Builder
        public Lexer( string []lineas  )
        {
            this.lineas = lineas;
            tokens = new List<Token>();
            errores = new List<Error>();
        }



        public void Tokenizar ()
        {
            Debug.Print($"Lineas {lineas.Length}");
            for(int i =0 ; i<lineas.Length ; i ++)
            {
                line =i+1;
                pos =0;
                int leftParant = 0;
                int rightParant = 0;

                //iterar por todas las lineas
                while ( pos<lineas[i].Length)
                {
                    current= lineas[i][pos];


                    if(char.IsWhiteSpace(current))  //ignora si es una pos en blanco 
                    {
                        pos+=1;
                    }


        #region PARENTESIS
                    //Parentesis 
                    else if (current == '(')
                    {
                        leftParant+=1;
                        tokens.Add(new Token(TypeToken.OpenParenthesis,current.ToString() , line , CalcularColumna(line)));
                        NextChar();
                    }
                    else if(current == ')')
                    {
                        rightParant+=1;
                        tokens.Add(new Token(TypeToken.CloseParenthesis,current.ToString() , line , CalcularColumna(line)));
                        NextChar();
                    }

        #endregion





        #region S T R I N G
                    //Cadena de Texto
                    else if( current == '"')
                    {
                        //si es una cadena recorrela hasta q encuentr el final , o sino esta cerrada , hasta q se acaben los caracteres
                        var value = "";
                        NextChar(); // para no anadir a ' " '
                        while ( current!= '?'  && GetNext()!= '"' )
                        {
                            value += current;
                            NextChar();
                        }
                        NextChar(); //pos si llego al final del "

                        if(current == '"')
                        {
                            tokens.Add(new Token (TypeToken.String, value , line , CalcularColumna(line)));
                            NextChar();
                        }
                        else
                        {
                            //significa q no se cerro la cadena 
                            var token = new Token(TypeToken.InvalidToken , value, line, CalcularColumna(line));

                            tokens.Add(token);

                            errores.Add(new DontCloseStringError(token));
                        }
                        
                    }
        #endregion

        #region Oper Aritmeticos
        //potencia o multiplicacion 
                    else if(current == '*')
                    {
                        NextChar();

                        if(current== '*' &&   (char.IsWhiteSpace(GetNext() )|| GetNext()=='?' || char.IsDigit(GetNext()))  ) //es potencia 
                        {
                            tokens.Add( new Token (TypeToken.Operador ,"**" , line , CalcularColumna(line)));
                            NextChar();
                        }
                        else if(char.IsWhiteSpace(current)  || char.IsDigit(current) )
                        {
                            tokens.Add(new Token(TypeToken.Operador , "*" , line, CalcularColumna(line)));
                        }
                    }

                    //Operadores Aritmeticos
                    else if((current == '+' || current == '-' || current == '/' || current == '%' ) && (char.IsWhiteSpace(GetNext() )|| GetNext()=='?' || char.IsDigit(GetNext())) )
                    {
                        tokens.Add( new Token(TypeToken.Operador, current.ToString(), line, CalcularColumna(line)));
                        NextChar();
                    }

                    //Operadores Booleanos
                    else if (  ( (current == '|' && GetNext()== '|')  || (current == '&' && GetNext()== '&' ) ) && (char.IsWhiteSpace(GetNext(pos+1)) || GetNext(pos+1)== '?') ) 
                    {
                        string  value = "";
                        while(!char.IsWhiteSpace(current)  && current != '?')
                        {
                            value += current ;
                            NextChar();
                        }
                        if(value == "||")
                        tokens.Add(new Token(TypeToken.Or , value , line, CalcularColumna(line)));
                        else
                        tokens.Add(new Token(TypeToken.And,value,line,CalcularColumna(line)));

                    }


        #endregion




        #region  N U M E R O S
                    //Numbers
                    else if(char.IsDigit(current))
                    {   
                        Debug.Print("Numero");
                        string value = "";
                        value += current;
                        NextChar();
                        
                        //mientras halla siguiente elemento  y  el sig no sea espacio en blanco 
                        while (  current != '?'  &&  !char.IsWhiteSpace(current)  &&  char.IsDigit(current) )
                        {
                            value+= current;
                            NextChar();
                        }

                        //cuando termine ,verificar si se puede convertir a un numero entero 
                        if(int.TryParse(value ,out int result))
                        { 
                            Debug.Print("es numero");
                            //si es un numero crea un nuevo token 
                            tokens.Add( new Token(TypeToken.Numero,value,line,CalcularColumna(line)));
                        }
                        else
                        {
                            Debug.Print("No es numero");
                            var token = new Token( TypeToken.InvalidToken,value ,line, CalcularColumna(line));
                            tokens.Add(token);
                            //Agregar error 
                            errores.Add(new InvalidNumberError(token));
                        }

                    }

        #endregion




        #region  IDENTIFICADOR


                    //Identificadores 
                    else if(char.IsLetter(current))
                    {
                        string value ="";
                        value += current;
                        bool invalidtoken = false ;
                        NextChar();

                        //todo identificador es valido si NO comienza por numero ni  - , y la union de letras , numeros y - es valido
                        while (current!= '?' &&  !char.IsWhiteSpace(current) && current!= '(' && current!= ')' )
                        {
                            
                            //si no es ninguno de los indenticadores dados es un error 
                            if(!invalidtoken && !char.IsLetter(current) && !char.IsDigit(current) && current != '_' ) 
                            {
                                Debug.Print("invalido");
                                invalidtoken = true ;
                            }

                            value+= current;
                            NextChar();
                        }

                        
                        if (invalidtoken)
                        {
                            var token = new Token(TypeToken.InvalidToken, value, line, CalcularColumna(line));

                            tokens.Add(token);

                            errores.Add(new InvalidWordError(token));
                        }
                        else
                        {
                            //si es valida la palabra verificar si es una palbra clave o sino se le tratara como etiqueta'
                            if (keywords.ContainsKey(value))
                            {
                                //si  contiene el valor 
                                tokens.Add(new Token(keywords[value], value, line, CalcularColumna(line)));
                            }
                            else
                            {
                                //agregarlo como un identificador '
                                tokens.Add(new Token(TypeToken.Identificador, value, line, CalcularColumna(line)));
                            }
                            //quiero hacer una especie de advertencia en el caso de  q la palabra se asimile a las guardadass dentro de los keywords
                        }
                        
                    }
    


        #endregion 






        #region OTHERS INVALID

                    else 
                    {
                        Debug.Print("else");
                        //cualquier otro carater agregarlo como invalido

                        var value = "";

                        while(current!= '?' && !char.IsWhiteSpace(current))
                        {
                            value+= current;
                            NextChar();
                        }

                        var token = new Token(TypeToken.InvalidToken, value, line , CalcularColumna(line));

                        tokens.Add(token);

                        errores.Add(new InvalidWordError(token));
                    }
                }

                
                //Si cuandon se termine la linea y la cant de parentesis izq y derechos no son iguales da error

                if(leftParant != rightParant)
                {
                    errores.Add(new ParentesisError(line));
                }
            }
        }

        #endregion
    }


    
}