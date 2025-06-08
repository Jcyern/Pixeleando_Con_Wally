using Evalua;
using ExpressionesTipos;
using IParseo;
using Parseando;
using TerminalesNode;

namespace ArbolSintaxisAbstracta
{
    public class ParentesisNode : TerminalExpression
    {
        Token parentesis;
        public ParentesisNode(Token value) : base(value.value, value.Pos)
        {
            parentesis = value;
        }


        public override ExpressionTypes GetTipo()
        {
            if (parentesis.value == "(")
            {
                return ExpressionTypes.OpenParent;
            }
            else
            {
                return ExpressionTypes.CloseParent;
            }
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            return true;
        }

        public override object? Evaluate(Evaluator? evaluator = null)
        {
            return parentesis;
        }
    }

    public class ParentesisParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var parent = parser.Current;
            parser.NextToken();

            return new ParentesisNode(parent);
        }
    }

}