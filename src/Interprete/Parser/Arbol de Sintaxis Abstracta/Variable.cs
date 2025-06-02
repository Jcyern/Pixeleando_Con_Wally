using System.ComponentModel;
using Alcance;
using ArbolSintaxisAbstracta;
using Expresion;
using ExpressionesTipos;
using TerminalesNode;

namespace vars
{
    public class VariableNode : TerminalExpression
    {
        Token Identificador;

        public VariableNode(Token Identificador) : base(Identificador.value, Identificador.Pos)
        {
            this.Identificador = Identificador;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            System.Console.WriteLine("Chequeo Sem de Variable ");
            //verifica si la variable ya se le asigno un tipo 
            return Scope.ContainType(Identificador.value);
        }

        public override object? Evaluate()
        {
            //dame el valor de la variable 
            var result =Scope.GetVariable(Identificador.value);
            System.Console.WriteLine($"La Variable {Identificador.value} guarda {result}");
            return result;
        }

        public override ExpressionTypes GetTipo()
        {
            
            if (Scope.ContainType(Identificador.value))
            {
                return Scope.variable_tipo[Identificador.value];
            }

            else
                return ExpressionTypes.Null;
        }
    }

}