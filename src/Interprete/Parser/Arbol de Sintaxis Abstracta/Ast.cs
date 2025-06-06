using  IParseo;
using Errores;
using System.Diagnostics;
using System.Net.NetworkInformation;
using ExpressionesTipos;
using Alcance;
using Expresion;
using Evalua;
using Ievalua;

namespace ArbolSintaxisAbstracta
{

    public abstract class AstNode  : IEvaluate
    {
        public (int fila, int columna) Location;
        public static List<Error> compilingError = new List<Error>();

        public virtual object? Evaluate( Evaluator? evaluador = null )
        {
            System.Console.WriteLine("Evaluate Ast");
            return 0;
        }
        public virtual ExpressionTypes GetTipo()
        {
            System.Console.WriteLine("es el nodo ast ");
            return ExpressionTypes.Null;
        }


        //tiene que saberse chequearse semanticamnente 
        public virtual bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (this is Expression exp)
            {
                System.Console.WriteLine("es una expression chek semantic expression");
                //return exp.CheckSemantic();
            }

            return false;
        }
    }


}