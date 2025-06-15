using System.Collections.Generic;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using PincelUnity_;
using TerminalesNode;
using UnityEngine;

namespace ArbolSintaxisAbstracta
{
    public class IsBrushColorNode : TerminalExpression
    {
        Token brush;
        Expression? color;
        public IsBrushColorNode(Token brush, Expression? color) : base(brush.value, brush.Pos)
        {
            this.color = color;
            this.brush = brush;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (color != null)
            {
                return color.CheckSemantic(ExpressionTypes.Color);
            }
            else
            {
                compilingError.Add(new ExpectedType(brush.Pos, ExpressionTypes.Color.ToString(), ExpressionTypes.Null.ToString()));
                return false;
            }
        }

        public override object? Evaluate(Evaluator? evaluator = null)
        {
            return Pincel.Color == color!.Evaluate()!.ToString()!;
        }

        public override ExpressionTypes GetTipo()
        {
            return ExpressionTypes.Bool;
        }
    }


        public class IsBrushColorParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            Debug.Log("Parseando is Brush collor ");
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

            return new IsBrushColorNode(brush, exp);
        }

    }
}