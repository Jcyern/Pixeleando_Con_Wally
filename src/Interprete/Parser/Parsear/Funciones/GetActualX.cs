
using Errores;
using Evalua;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class GetAlctualXNode : AstNode
    {
        Token get;

        public GetAlctualXNode(Token get)
        {
            this.get = get;
        }

        public override ExpressionTypes GetTipo()
        {
            return ExpressionTypes.Number;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            return true;
        }

        public override object? Evaluate(Evaluator? evaluador = null)
        {


            if (Wally.Pos.Fila != int.MaxValue)
            {
                if (evaluador != null)
                    evaluador.Move();

                System.Console.WriteLine($"Get X -- {Wally.Pos.Fila}");
                return Wally.Pos.Fila;
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



    public class GetActualXParse : IParse
        {
            public AstNode? Parse(Parser parser)
            {
                var get = parser.Current;

                parser.ExpectedTokenType(TypeToken.OpenParenthesis);
                parser.ExpectedTokenType(TypeToken.CloseParenthesis);

                parser.NextToken();

                return new GetAlctualXNode(get);
            }
        }
}