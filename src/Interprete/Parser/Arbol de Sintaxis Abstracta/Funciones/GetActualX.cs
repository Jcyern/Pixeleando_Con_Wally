
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using TerminalesNode;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class GetAlctualXNode : TerminalExpression
    {
        Token get;

        public GetAlctualXNode(Token get) : base (get.value,get.Pos)
        {
            this.get = get;
        }

        public override ExpressionTypes GetTipo()
        {
            return ExpressionTypes.Number;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {   
            //siempre est bien nunca  se le pasa nada 
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
                System.Console.WriteLine("Parsear GetActualX");
                var get = parser.Current;

                parser.ExpectedTokenType(TypeToken.OpenParenthesis);
                parser.ExpectedTokenType(TypeToken.CloseParenthesis);

                parser.NextToken();

                return new GetAlctualXNode(get);
            }
        }
}