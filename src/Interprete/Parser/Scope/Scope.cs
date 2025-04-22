using System.Diagnostics;
using System.Linq.Expressions;
using ArbolSintaxisAbstracta;
using Expression = ArbolSintaxisAbstracta.Expression;
namespace Alcance 
{
    public class Scope 
    {
        public Dictionary<string , Expression> variables = new ();

        public Expression? GetVariable( string value )
        {
            if( variables.ContainsKey(value))
            {
                return variables [value ];
            }
            else 
            return null;
        } 


        public bool Asignar (string name , Expression value )
        {
            if(!variables.ContainsKey(name))
            {
                Debug.Print($"Se asigno la variable {name } valor: {value.Evaluate() }");
                variables[name ]= value ;

                return true;
            }

            else
            {   
                System.Console.WriteLine("La variable ya existe en el alcance");
                System.Console.WriteLine("No pueden haber dos variables con iguales nombres");
                return false ;
            }
        }
    }
}