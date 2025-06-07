using Errores;
using Evalua;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class GetAlctualYNode : AstNode
    {
        Token get;

        public GetAlctualYNode(Token get)
        {
            this.get = get;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            return true;
        }

        public override object? Evaluate(Evaluator? evaluador = null)
        {

            if (Wally.Pos.Columna != int.MaxValue)
            {
                if (evaluador != null)
                    evaluador.Move();

                System.Console.WriteLine($"Y = {Wally.Pos.Columna}");
                return Wally.Pos.Columna;
            }
            else
            {
                //erorr
                if (evaluador != null)
                {
                    evaluador.AddError(new IsntSpawn(get.Pos));
                    evaluador.Move();
                }
                return 0;
            }
        }
    }




    public class GetActualYParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var get = parser.Current;

            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.ExpectedTokenType(TypeToken.CloseParenthesis);

            parser.NextToken();

            return new GetAlctualYNode(get);
        }
    } 

}