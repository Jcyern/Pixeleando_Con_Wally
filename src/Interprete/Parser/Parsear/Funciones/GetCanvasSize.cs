
using Evalua;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class GetCanvasSizeNode : AstNode
    {
        Token get;


        public GetCanvasSizeNode(Token get)
        {
            this.get = get;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            //siempre hay un tamno fijo para  el canvas 
            return true;

        }


        public override object? Evaluate(Evaluator? evaluador = null)
        {
            System.Console.WriteLine($" Get Canvas {Wally.canvas.Item1} , {Wally.canvas.Item2}");
            if (evaluador != null)
            {
                evaluador.Move();
            }
            return Wally.canvas;
        }


        public override ExpressionTypes GetTipo()
        {
            return ExpressionTypes.TruplaNumber;
        }
    }




    public class GetCanvasSizeParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var get = parser.Current;

            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.ExpectedTokenType(TypeToken.CloseParenthesis);

            //para seguir la evaluacion
            parser.NextToken();

            return new GetCanvasSizeNode(get);
        }
    }
}