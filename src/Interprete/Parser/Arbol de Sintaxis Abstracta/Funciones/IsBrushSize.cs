
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Pincel_;
using TerminalesNode;

namespace ArbolSintaxisAbstracta
{
    public class IsBrushSizeNode : TerminalExpression
    {
        Token brush;

        Expression? Size;

        public IsBrushSizeNode(Token brush, Expression? Size) : base(brush.value, brush.Pos)
        {
            this.brush = brush;
            this.Size = Size;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (Size != null)
            {
                return Size.CheckSemantic(ExpressionTypes.Number);
            }
            else
            {
                compilingError.Add(new ExpectedType(brush.Pos, ExpressionTypes.Color.ToString(), ExpressionTypes.Null.ToString()));
                return false;
            }
        }

        public override ExpressionTypes GetTipo()
        {
            return ExpressionTypes.Bool;
        }

        public override object? Evaluate(Evaluator? evaluador = null)
        {
            var size = Convert.ToInt32(Size!.Evaluate());

            return Pincel.Size == size;
        }
    }

    public class IsBrushSizeParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            //actual brush 
            var brush = parser.Current;

            //tiene qeu venir un parentesis 
            parser.ExpectedTokenType(TypeToken.OpenParenthesis);

            parser.NextToken();

            //empezar a leer las expresiones hasta que venga un parenteiss de clausura 
            var tokens = new List<Token>();
            while (parser.Current.type != TypeToken.CloseParenthesis)
            {
                tokens.Add(parser.Current);
                parser.NextToken();
            }

            //avanzar luego del close parentesis para seguir analizando 

            parser.NextToken();

            var exp = Converter.GetExpression(tokens);

            return new IsBrushSizeNode(brush, exp);
        }
    }
}
