using  IParseo;
using Errores;
using System.Diagnostics;
using System.Net.NetworkInformation;
using ExpressionesTipos;
using Alcance;
using Expresion;

namespace ArbolSintaxisAbstracta
{

    public abstract class AstNode : IParse
    {
        public Scope scope = new();
        public (int fila, int columna) Location;
        public static List<Error> compilingError = new List<Error>();
        public void Parse()
        {
        }

        //tiene que saberse chequearse semanticamnente 
        public virtual bool CheckSemantic(ExpressionTypes tipo )
        {
            //logica para ver los parametros de mi metodo 
            return false;
        }
    }


}