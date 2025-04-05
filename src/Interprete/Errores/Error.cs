namespace Errores
{
    public abstract class Error
    {
        protected string message ;

        int fila ;
        int columna;

        public Error ( int fila , int columna )
        {
            this.fila = fila;
            this.columna = columna ;
            this.message += $"Fila:{fila} Columna: {columna} ";
        }

        public virtual  void  ShowError()
        {
            System.Console.WriteLine(message);
        }
    }



    public class InvalidNumberError : Error
    {
        public InvalidNumberError( Token token ) : base( token.fila, token.columna)
        {
            //no es un numero entero , 
            this.message +=$"InvalidNumberError , the expresion {token.value} is not a whole number";
        }
    }

    public class InvalidWordError : Error
    {
        public InvalidWordError(Token token ) : base(token.fila, token.columna)
        {
            message+= $"InvalidWordError , the value {token.value} is not a word (can not begin with  numbers or the symbol -) and (just can content letters , numbers and the char -)"; 
        }
    }

    public class DontCloseStringError : Error
    {
        public DontCloseStringError(Token  token ) : base(token.fila, token.columna)
        {
            message += $"DontCloseStringError  -you open a string but you never close it-";
        }
    }

    public class ParentesisError : Error
    {
        public ParentesisError(int fila) : base(fila, 0)
        {
            message = $"Fila:{fila} ParentesisError -you open a parant and dont close it- or -you close a parant but you never open it";
        }
    }


}