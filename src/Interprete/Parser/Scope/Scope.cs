
using System.Diagnostics;
using ArbolSintaxisAbstracta;
using ExpressionesTipos;
using Expresion;
using Errores;
namespace Alcance 
{
    public static  class Scope 
    {
        //guarda la variable y su tipo 
        public  static Dictionary<string , ExpressionTypes> variable_tipo = new ();

        //guarda la variable y su valor 
        public static Dictionary<string, object> variable_expresion = new();

        public  static object? GetVariable(string value)
        {
            if (Contain(value))
            {
                System.Console.WriteLine($"La variable {value} existe ");
                //si lo contine dar el valor 
                return variable_expresion[value];
            }
            else
            {   System.Console.WriteLine($"No existe la vairable {value}");
                return null;
            }
        }

        public static bool ContainType(string name )
        {
            return variable_tipo.ContainsKey(name);
        }
        public static bool Contain(string name)
        {
            return variable_expresion.ContainsKey(name);
        }

        public static bool AsignarType(string name, Expression value)
        {
            //ver primero el tipo 
            if (value.type == ExpressionTypes.Null)
                value.type = value.GetTipo();

            //de existir && es el mismo tipo  
            if (ContainType(name))
            {
                if (variable_tipo[name] == value.type)
                {
                    //si  es igual al tipo 
                    System.Console.WriteLine($"Es del mismo tipo  {value.type} se podra asignar ");
                    ///es valida la asignacion ,, no se guarda el valor aun hasta expression
                    return true;
                }
                System.Console.WriteLine($"No es del mismo tipo sera error de asignacion type {variable_tipo[name]}  type pas {value.type}");
                AstNode.compilingError.Add(new AsignationTypeError(value.Location, name, variable_tipo[name], value.type));
                return false;
            }
            else
            {
                System.Console.WriteLine($"Try asociar tipo a la variable => {name}");
                //agrega la variable 
                //antes de agregarlo verifica si no es invalida la expresion
                if (value.type != ExpressionTypes.Invalid)
                {
                    System.Console.WriteLine($"la variable {name} se asigno el tipo {value.type} ");
                    variable_tipo[name] = value.type;
                    return true;
                }
                else
                {
                    System.Console.WriteLine("No se creo pq se le asigna invalid expression");
                    AstNode.compilingError.Add(new AsignationInavalidTypeError(value.Location, name));
                    return false;
                }
            }
        }
        
        //Asignar para el Evaluate 

        public static void  Asignar(string name, Expression value)
        {
            //en este punto ya verificamos q son buenas las expresiones ya pasamos el Semantico 
            //verifica si la variable existe 
            if (Contain(name))
            {
                System.Console.WriteLine($"Se cambio el valor de la variable {name}");
                variable_expresion[name] = value.Evaluate()!;
            }

            else
            {
                System.Console.WriteLine($"Se creo la variable {name}");
                variable_expresion[name] = value.Evaluate()!;
            }

        }
    }
}