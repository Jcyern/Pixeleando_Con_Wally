using Errores;
using Evalua;
using ExpressionesTipos;
using IParseo;
using Parseando;
using TerminalesNode;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class GetAlctualYNode : TerminalExpression
    {
        Token get;

        public GetAlctualYNode(Token get) : base(get.value, get.Pos)
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

                System.Console.WriteLine($"Get Y -- {Wally.Pos.Columna}");
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


        public override ExpressionTypes GetTipo()
        {
            return ExpressionTypes.Number;
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