
using Alcance;
using ArbolSintaxisAbstracta;
using AsignacionNodo;
using Convertidor_Pos_Inf;
using Errores;
using Expresion;
using IParseo;
using Parseando;

namespace NodosParser
{
    #region Asignatin
    public class AsignacionParse : IParse
    {
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        //Parsear una asignacion
        public AstNode? Parse(Parser parser)
        {
            //por si  hay q volver atras en el caso de q no sea una asignacion
            var primarypos = parser.current;
            System.Console.WriteLine("Parseando asignacion");
            if (parser.Current.type == TypeToken.Identificador && (parser.GetNextToken().type == TypeToken.Asignacion && parser.GetNextToken().fila == parser.Current.fila))
            {
                //crear nodo asignacion 
                var name = parser.Current;

                var operador = parser.GetNextToken();

                //lo que siguen q se mantenga en la misma linea lo crearemos como una expresion
                //con el converter 
                var lista = new List<Token>();
                while (parser.GetNextToken().type != TypeToken.InvalidToken && parser.GetNextToken().fila == operador.fila)
                {
                    lista.Add(parser.NextToken());
                }
                //y avanzamos en el parser para q no se quede con esa ultima pos q pertenece a la asignacion y no de error sintaxtico 
                parser.NextToken();

                if (lista.Count > 0)
                {
                    System.Console.WriteLine("Se creo nodo asignacion ");
                    Expression? expression = Converter.GetExpression(lista);
                    //crear la asignacion con la expression creada 
                    return new AsignationNode(name, operador, expression);
                }
                else
                {
                    System.Console.WriteLine("Se creo nodo asignacion con expression null");
                    return new AsignationNode(name, operador, null);
                }
            }
            else
                return null;
        }
    }
    

#endregion


}