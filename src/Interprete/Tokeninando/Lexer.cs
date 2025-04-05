
using Errores;
namespace lexer
{
    public class Lexer 
    {
        string [] lineas ;

        List<Error> errores ;
        List<Token> tokens ;

        int pos;
        int line ;
        char current ;  //altual caracter 



        public void   NextChar ()  //dame el siguiente caracter 
        {

            if(IsNext() == true )
            {
                pos+= 1; 
                current= lineas [line][pos];
            }

            //sino no hace nada 
        }

        public char GetNext ()
        {
            if(IsNext())
            {
                return lineas[line][pos+1];

            }
            return '?';
        }

        public bool IsNext() //hay siguiente 
        {
            if(pos+1 < lineas[line].Length)
            return true ;

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
            for(int i =0 ; i<lineas.Length ; i ++)
            {
                line =i;
                pos =0;
                //iterar por todas las lineas
                while ( pos<lineas[i].Length)
                {
                    current= lineas[i][pos];


                    if(char.IsWhiteSpace(current))  //ignora si es una pos en blanco 
                    {
                        pos++;
                    }


                    //Numbers
                    if(char.IsDigit(current))
                    {   
                        string value = "";
                        value += current;
                        
                        //mientras halla siguiente elemento  y  el sig no sea espacio en blanco 
                        while (  IsNext()  &&  !char.IsWhiteSpace(GetNext()) )
                        {
                            NextChar();
                            value+= current;
                        }

                        //cuando termine ,verificar si se puede convertir a un numero entero 
                        if(int.TryParse(value ,out int result))
                        {
                            //si es un numero crea un nuevo token 
                            tokens.Add( new Token(TypeToken.Numero,value,line,CalcularColumna(line)));
                        }
                        else
                        {
                            var token = new Token( TypeToken.InvalidToken,value ,line, CalcularColumna(line));
                            tokens.Add(token);
                            //Agregar error 
                            errores.Add(new InvalidNumberError(token));
                        }

                    }

                    //Identificadores 
                    if(char.IsLetter(current))
                    {
                        string value ="";
                        value+=current;
                        bool invalidtoken = false ;

                        //todo identificador es valido si NO comienza por numero ni  - , y la union de letras , numeros y - es valido
                        while (IsNext() &&  !char.IsWhiteSpace(GetNext()) )
                        {
                            NextChar();
                            //si no es ninguno de los indenticadores dados es un error 
                            if(!invalidtoken && !char.IsLetter(current) && !char.IsDigit(current) && current != '-' ) 
                            {
                                invalidtoken = true ;
                            }

                            value+= current;
                        }

                        if(invalidtoken)
                        {
                            var token = new Token(TypeToken.InvalidToken,value,line,CalcularColumna(line));

                            tokens.Add(token);

                            errores.Add(new InvalidWordError(token));
                        }
                        else
                        {
                            //si es valida la palabra verificar si es una palbra clave o sino se le tratara como etiqueta'
                            
                        }
                        


                    }
    
                }


            }
        }
    }
}