
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using Pincel_;

namespace ArbolSintaxisAbstracta
{
    public class IsBrushSize : AstNode
    {
        Token brush;

        Expression? Size;

        public IsBrushSize(Token brush, Expression? Size)
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
}