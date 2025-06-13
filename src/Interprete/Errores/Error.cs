
using System.Runtime.InteropServices;
using ExpressionesTipos;
using Utiles;

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
    public class BrazesError : Error
    {
        public BrazesError(int fila) : base(fila, 0)
        {
            message = $"Fila:{fila} Error -you open a braze and dont close it- or -you close a braze but you never open it";
        }
    }


    #region NotLabel

    public class LabelError : Error
    {
        public LabelError((int fila, int columna) pos, string label) : base(pos.fila, pos.columna)
        {
            message += $"The label {label} isnt define in your code ";
        }

    }
    #endregion



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



    #region Errores Evaluate 


    public class DirectOutOfRange : Error
    {
        public DirectOutOfRange((int fila, int columna) pos, (int min, int max) rango, int number) : base(pos.fila, pos.columna)
        {
            message += $"El numero {number} no esta acotado {rango.min} y {rango.max}";
        }
    }

    public class IsntSpawn : Error
    {
        public IsntSpawn((int fila, int columna) pos) : base(pos.fila, pos.columna)
        {
            message += $"El Wally no se le ha hecho Spawn";
        }
    }


    public class GoToOverFlow : Error
    {
        public GoToOverFlow((int fila, int columna) pos) : base(pos.fila, pos.columna)
        {
            message += "Stack Overflow";
        }
    }



    public class ZeroDivitionError : Error
    {
        public ZeroDivitionError((int fila, int columna) pos) : base(pos.fila, pos.columna)
        {
            message += "The Division by Zero is not define ";
        }
    }



    //errores de Spawn de fuera de rango 
    public class SpawnOutOfRange : Error
    {
        public SpawnOutOfRange((int fila, int columna) pos, int x, int y) : base(pos.fila, pos.columna)
        {
            message += $"The position {x},{y} is out of range ,   0 <= {x} < {Wally.canvas.Item1} && 0 <= {y} < {Wally.canvas.Item2}";
        }
    }

    #endregion


}