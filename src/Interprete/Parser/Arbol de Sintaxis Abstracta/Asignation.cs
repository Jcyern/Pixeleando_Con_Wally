
using Alcance;
using ArbolSintaxisAbstracta;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;

namespace AsignacionNodo
{
    public class AsignationNode : Expression
    {
        public Token Identificador;

        public Token Operador;

        public Expression? Value;

        public AsignationNode(Token Identificador, Token Operador, Expression? Value)
        {
            this.Identificador = Identificador;
            this.Operador = Operador;
            this.Value = Value;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            System.Console.WriteLine("Check Semantic Asignacion");
            //verifica q
            //se asiga la variable en el chequeo semantico  si la exp esta bien semanticamente
            if (Value != null)
            {
                System.Console.WriteLine("El valor de la asignacion no es null ");
                //si la exp es valida sem verificar si no hay var de ese tipo y si la hay verificar q sea el mismo tipo pasado
                if (Value.CheckSemantic())
                {
                    System.Console.WriteLine($"Asignar {Identificador.value}");
                    return Scope.AsignarType(Identificador, Value);
                }

                //la exp no esta bien semanticamente 
                else
                {
                    System.Console.WriteLine("Se chequea bien ");


                    return false;
                }
            }
            else
            {
                System.Console.WriteLine($"No se puede crea la variable {Identificador.value}   pq se le pasa un nulll ,o nada");
                compilingError.Add(new NullAsignationError(Identificador.Pos, Identificador.value));
                return false;
            }
        }


        public override object? Evaluate(Evaluator? evaluador = null)
        {
            //asignar el valor de la variable en el dicc de la expression
            //para q no se pueda seguir bien la ejecucion
            Scope.Asignar(Identificador.value, Value!);
            var result = Scope.GetVariable(Identificador.value);
            System.Console.WriteLine($"Asignacion {Identificador.value} <- {result}");
            //mover pos en el evaluador 
            if (evaluador != null)
                evaluador.Move();
            return result;
        }
    }

    
        public class AsignacionParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            //por si  hay q volver atras en el caso de q no sea una asignacion
            var primarypos = parser.current;
            System.Console.WriteLine("Parseando asignacion");
            System.Console.WriteLine("Parsear Asignacion");
            if (parser.Current.type == TypeToken.Identificador && parser.GetNextToken().type == TypeToken.Asignacion && parser.GetNextToken().fila == parser.Current.fila)
            {
                //crear nodo asignacion 
                var name = parser.Current;
                System.Console.WriteLine($"Var namme {name.value}  pos = {name.fila}, {name.columna}");

                var operador = parser.NextToken();
                

                //lo que siguen q se mantenga en la misma linea lo crearemos como una expresion
                //con el converter 
                var lista = new List<Token>();
                while (parser.Current.type != TypeToken.Fin && parser.GetNextToken().fila == operador.fila)
                {

                    lista.Add(parser.NextToken());
                    System.Console.WriteLine($"Lista add {parser.Current.value}");
                }
                //y avanzamos en el parser para q no se quede con esa ultima pos q pertenece a la asignacion y no de error sintaxtico 
                parser.NextToken();

                if (lista.Count > 0)
                {
                    System.Console.WriteLine("Se creo nodo asignacion ");
                    var expression = Converter.GetExpression(lista);
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
    
}