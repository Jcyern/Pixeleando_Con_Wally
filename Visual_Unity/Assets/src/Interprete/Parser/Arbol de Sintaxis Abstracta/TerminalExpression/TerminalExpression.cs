using Boolean;
using Expresion;
using ExpressionesTipos;
using Numero;
using Cadenas;
using vars;
using Paleta_Colores;
using UnityEngine;
using ArbolSintaxisAbstracta;


namespace TerminalesNode
{
    public abstract class TerminalExpression : Expression
    {
        public TerminalExpression(object? value, (int, int) Location)
        {
            this.value = value;
            this.Location = Location;
        }

        public override ExpressionTypes GetTipo()
        {
            if (this is Number number)
            {
                Debug.Log("Type Numero");
                return number.GetTipo();
            }

            else if (this is Bool @bool)
            {
                Debug.Log("Type Bool");
                return @bool.GetTipo();
            }

            else if (this is Cadenas.String @string)
            {
                Debug.Log("Type String");
                return @string.GetTipo();
            }

            else if (this is VariableNode vars)
            {
                Debug.Log("Type Variable");
                return vars.GetTipo();
            }
            else if (this is Paleta_Colores.Color color)
            {
                Debug.Log("Type Color");
                return color.GetTipo();
            }



            else
            {
                Debug.Log("TYpe INalid");
                return ExpressionTypes.Invalid;
            }

        }



    }
}