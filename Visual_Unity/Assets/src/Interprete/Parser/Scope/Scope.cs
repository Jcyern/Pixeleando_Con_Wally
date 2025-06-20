
using UnityEngine;
using ArbolSintaxisAbstracta;
using ExpressionesTipos;
using Expresion;
using Errores;
using System.Collections.Generic;
namespace Alcance 
{
    public static class Scope
    {
        //guarda la variable y su tipo 
        public static Dictionary<string, ExpressionTypes> variable_tipo = new();

        //guarda la variable y su valor 
        public static Dictionary<string, object> variable_expresion = new();

        //dado un label te da la linea 
        public static Dictionary<string, int> Labels = new();


        public static void ReiniciarScope()
        {
            variable_tipo.Clear();
            variable_expresion.Clear();
            Labels.Clear();
        }

        public static object? GetVariable(string value)
        {
            if (Contain(value))
            {
                Debug.Log($"La variable {value} existe ");
                //si lo contine dar el valor 
                return variable_expresion[value];
            }
            else
            {
                Debug.Log($"No existe la vairable {value}");
                return null;
            }
        }

        public static bool ContainType(string name)
        {
            return variable_tipo.ContainsKey(name);
        }
        public static bool Contain(string name)
        {
            return variable_expresion.ContainsKey(name);
        }

        public static bool AsignarType(Token name, Expression value)
        {
            //ver primero el tipo 
            if (value.type == ExpressionTypes.Null)
            {
                Debug.Log("Tipo null");
                value.type = value.GetTipo();
            }
            //de existir && es el mismo tipo  
            if (ContainType(name.value))
            {
                if (variable_tipo[name.value] == value.type)
                {
                    //si  es igual al tipo 
                    Debug.Log($"Es del mismo tipo  {value.type} se podra asignar ");
                    ///es valida la asignacion ,, no se guarda el valor aun hasta expression
                    return true;
                }
                Debug.Log($"No es del mismo tipo sera error de asignacion type {variable_tipo[name.value]}  type pas {value.type}");
                AstNode.compilingError.Add(new AsignationTypeError(value.Location, name.value, variable_tipo[name.value], value.type));
                return false;
            }
            else
            {
                Debug.Log($"Try asociar tipo a la variable => {name.value}");
                //agrega la variable 
                //antes de agregarlo verifica si no es invalida la expresion
                if (value.type != ExpressionTypes.Invalid && value.type != ExpressionTypes.Null)
                {
                    Debug.Log($"la variable {name.value} se asigno el tipo {value.type} ");
                    variable_tipo[name.value] = value.type;
                    return true;
                }
                else if (value.type == ExpressionTypes.Invalid)
                {
                    Debug.Log("No se creo pq se le asigna invalid expression");
                    AstNode.compilingError.Add(new AsignationInavalidTypeError(name.Pos, name.value));
                    return false;
                }
                else if (value.type == ExpressionTypes.Null)
                {
                    Debug.Log("No se creo pq se le asigna una null expression");
                    AstNode.compilingError.Add(new NullAsignationError(name.Pos, name.value));
                    return false;
                }

                return false;
            }
        }

        //Asignar para el Evaluate 

        public static void Asignar(string name, Expression value)
        {
            //en este punto ya verificamos q son buenas las expresiones ya pasamos el Semantico 
            //verifica si la variable existe 
            if (Contain(name))
            {
                Debug.Log($"Se cambio el valor de la variable {name}");
                variable_expresion[name] = value.Evaluate()!;
            }

            else
            {
                Debug.Log($"Se creo la variable {name}");
                variable_expresion[name] = value.Evaluate()!;
            }

        }

        public static bool AsignarLabel(Token label, int pos)
        {
            if (Labels.ContainsKey(label.value))
            {
                Debug.Log("ese label ya existe no puede haber dos label con el mismo nombre ");
                return false;
            }

            else
            {
                //asignarle la linea si no esta 
                Labels[label.value] = pos;
                return true;
            }
        }


        public static int GetLabel(string value)
        {
            if (Labels.ContainsKey(value))
            {
                return Labels[value];
            }
            else
            {
                return int.MaxValue;
            }
        }
    }
}