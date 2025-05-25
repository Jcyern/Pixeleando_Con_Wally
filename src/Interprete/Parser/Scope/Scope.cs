
using System.Diagnostics;
using ArbolSintaxisAbstracta;
using ExpressionesTipos;
using Expresion;
namespace Alcance 
{
    public class Scope 
    {
        //guarda la variable y su tipo 
        public Dictionary<string , ExpressionTypes> variable_tipo = new ();

        //guarda la variable y su valor 
        public Dictionary<string, Expression> variable_expresion = new();

        public Expression? GetVariable( string value )
        {
            return null ;
        } 


        public bool Asignar (string name , Expression value )
        {
            return false ;
        }
    }
}