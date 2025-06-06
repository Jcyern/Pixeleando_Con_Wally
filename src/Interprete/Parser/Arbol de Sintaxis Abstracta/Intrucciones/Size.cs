
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Pincel_;

namespace ArbolSintaxisAbstracta
{
    public class SizeNode : AstNode
    {
        public Token Size;
        public Expression? exp;


        public SizeNode(Token Size, Expression? exp)
        {
            this.Size = Size;
            this.exp = exp;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            System.Console.WriteLine("Chequeo de SizeNode");
            if (exp != null)
                return exp.CheckSemantic(ExpressionTypes.Number);
            else
            {
                //crear error
                compilingError.Add(new ExpectedType(Size.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                return false;
            }
        }

        public override object? Evaluate(Evaluator? evaluator = null)
        {
            System.Console.WriteLine("Evaluando Size");
            Pincel.ChangeSize(Convert.ToInt32(exp!.Evaluate()));

            return 0;
        }
    }


    public class SizeParse : IParse
    {
        public AstNode Parse(Parser parser)
        {
            //la primera debe ser el Token Size
            var size = parser.Current;
            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.NextToken();

            var token = new List<Token>();

            while (parser.Current.type != TypeToken.CloseParenthesis)
            {
                token.Add(parser.Current);
                parser.NextToken();
            }

            //seguir avanazando en los tokens 
            parser.NextToken();

            //comvertir a exp la lista de tokens 

            var exp = Converter.GetExpression(token);


            return new SizeNode(size, exp);
        }
    }
}