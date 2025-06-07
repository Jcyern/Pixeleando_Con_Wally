
using Alcance;
using Evalua;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Semantic;

namespace ArbolSintaxisAbstracta
{
    public class Label : AstNode
    {
        Token token;

        public Label(Token token)
        {
            this.token = token;
        }



        public bool Asignar(Token value)
        {
            return Scope.AsignarLabel(value, Pos() );
        }

        public int Pos()
        {
            //para guardar la pos correcta en la lista de nodos
            return Semantico.Current();
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            return Asignar(token);
        }

        public override object? Evaluate(Evaluator? evaluador = null)
        {
            var pos = Scope.GetLabel(token.value);
            System.Console.WriteLine($"La etiqueta {token.value} esta en {pos}");

            //avanzar en el curretn del evaludador
            if (evaluador != null)
                evaluador.Move();

            return pos;
        }
    }



    public class LabelParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            //estoy en un identificador , verifca si es el unico en su linea 
            var ident = parser.Current;
            if (parser.GetNextToken().fila != ident.fila)
            {
                //para seguir analizando 
                parser.NextToken();
                //crear un nueva etiqueta 
                return new Label(ident);


            }
            else
            {
                return null;
            }
        }
    }
}