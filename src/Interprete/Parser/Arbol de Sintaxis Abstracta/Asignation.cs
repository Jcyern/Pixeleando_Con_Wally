
using Alcance;
using ArbolSintaxisAbstracta;
using Errores;
using Expresion;
using ExpressionesTipos;

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
            //se asiga la variable en el chequeo semantico 
            if (Value != null)
                return Scope.AsignarType(Identificador.value, Value);
            else
            {
                System.Console.WriteLine($"No se puede crea la variable {Identificador.value}   pq se le pasa un nulll ,o nada");
                compilingError.Add(new NullAsignationError(Identificador.Pos, Identificador.value));
                return false;
            }
        }


        public override object? Evaluate()
        {
            //asignar el valor de la variable en el dicc de la expression
            //para q no se pueda seguir bien la ejecucion
            Scope.Asignar(Identificador.value, Value!);
            var result = Scope.GetVariable(Identificador.value);
            System.Console.WriteLine($"Asignacion {Identificador.value} <- {result}");
            return result;
        }
    }
}