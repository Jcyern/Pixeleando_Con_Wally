using  IParseo;
using Errores;
using UnityEngine;
using System.Net.NetworkInformation;
using ExpressionesTipos;
using Alcance;
using Expresion;
using Evalua;
using Ievalua;
using System.Collections.Generic;

namespace ArbolSintaxisAbstracta
{
    using UnityEngine;

    public abstract class AstNode  : IEvaluate
    {
        public (int fila, int columna) Location;
        public static List<Error> compilingError = new List<Error>();

        public virtual object? Evaluate( Evaluator? evaluador = null )
        {
            Debug.Log("Evaluate Ast");
            return 0;
        }
        public virtual ExpressionTypes GetTipo()
        {
            Debug.Log("es el nodo ast ");
            return ExpressionTypes.Null;
        }


        //tiene que saberse chequearse semanticamnente 
        public virtual bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (this is Expression exp)
            {
                Debug.Log("es una expression chek semantic expression");
                //return exp.CheckSemantic();
            }

            return false;
        }
    }


}