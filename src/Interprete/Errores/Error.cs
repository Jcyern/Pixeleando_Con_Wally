
using System.Runtime.InteropServices;
using ExpressionesTipos;

namespace Errores
{
    public abstract class Error
    {
        protected string message;

        int fila;
        int columna;

        public Error(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.message += $"Fila:{fila} Columna: {columna} ";
        }

        public virtual void ShowError()
        {
            System.Console.WriteLine(message);
        }
    }



    public class InvalidNumberError : Error
    {
        public InvalidNumberError(Token token) : base(token.fila, token.columna)
        {
            //no es un numero entero , 
            this.message += $"InvalidNumberError , the expresion {token.value} is not a whole number";
        }

        public InvalidNumberError(string value, (int fila, int columna) pos) : base(pos.fila, pos.columna)
        {
            //no es un numero entero , 
            this.message += $"InvalidNumberError , the expresion {value} is not a whole number";
        }
    }

    public class InvalidWordError : Error
    {
        public InvalidWordError(Token token) : base(token.fila, token.columna)
        {
            message += $"InvalidWordError , the value {token.value} is not a word (can not begin with  numbers or the symbol -) and (just can content letters , numbers and the char -)";
        }
    }

    public class DontCloseStringError : Error
    {
        public DontCloseStringError(Token token) : base(token.fila, token.columna)
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



    public class NullExpressionError : Error
    {
        public NullExpressionError((int fila, int columna) pos, string exp) : base(pos.fila, pos.columna)
        {
            message += $"Es  nula la expression {exp}";
        }
    }
    //Errores de Sintaxis 
    public class ExpectedAritmeticExpressionError : Error
    {
        public ExpectedAritmeticExpressionError(int fila, int columna) : base(fila, columna)
        {
            message += "Expected a AritmeticExpression ";
        }
    }

    public class InvalidType : Error
    {
        public InvalidType((int fila, int columna) pos) : base(pos.fila, pos.columna)
        {
            message += "The Type is Invalid";
        }
    }


    public class ExpectedType : Error
    {
        public ExpectedType((int fila, int columna) pos, string expected, string type) : base(pos.fila, pos.columna)
        {
            message += $"Se esperaba tipo {expected}  y se paso {type}";
        }
    }

    public class DifferentTypesError : Error
    {
        public DifferentTypesError((int fila, int columna) pos, string t1, string t2) : base(pos.fila, pos.columna)
        {
            message += $"Son de diferentes tipos Left: {t1} Right {t2}";
        }
    }


    #region Errores de Asignacion

    public class AsignationTypeError : Error
    {
        public AsignationTypeError((int fila, int columna) pos, string name, ExpressionTypes guardado, ExpressionTypes pasado) : base(pos.fila, pos.columna)
        {
            message += $"The variable: {name} is assigned with the type: {guardado}   and you pass a type {pasado}";
        }
    }

    public class AsignationInavalidTypeError : Error
    {
        public AsignationInavalidTypeError((int fila, int columna) pos, string name) : base(pos.fila, pos.columna)
        {
            message += $"You cant create the variable {name} because  you assigned a InvalidExpression ";
        }
    }


    public class NullAsignationError : Error
    {
        public NullAsignationError((int fila, int columna) pos, string name) : base(pos.fila, pos.columna)
        {
            message += $"We can't assigned a the variable {name} with a null value ";
        }
    }



    #endregion


    #region NotSintaxis

    public class SintaxisError : Error
    {
        public SintaxisError((int fila, int columna) pos, string value) : base(pos.fila, pos.columna)
        {
            message += $"This token {value}  not correspond a sintaxis structure";
        }

    }

    #endregion





}