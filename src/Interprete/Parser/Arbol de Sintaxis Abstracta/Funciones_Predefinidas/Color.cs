
using ArbolSintaxisAbstracta;
using Convertidor_Pos_Inf;
using Errores;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Pincel_;

namespace ColorFunc
{
    public class ColorNode : AstNode
    {
        Token Color;   //para saber en la pos q estamos a la hora de mandar los errores 
        Expression? color;    //saber el color que tendra el pincel

        public ColorNode(Token Color, Expression? color)
        {
            this.Color = Color;
            this.color = color;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {


            if (color == null)
            {
                //crear error de expected type 
                compilingError.Add(new ExpectedType(Color.Pos, ExpressionTypes.Color.ToString(), ExpressionTypes.Null.ToString()));
                return false;
            }
            else if (color.CheckSemantic(ExpressionTypes.Color))
            {
                //si es un color
                return true;
            }
            //la expression no es un string 
            return false;
        }


        public override object? Evaluate()
        {
            System.Console.WriteLine("Evaluate Color ");

            //cambiar color del pincel 
            Pincel.ChangeColor(color!.Evaluate()!.ToString()!);
            return 0;
        }



    }

    #region ColorParse

    public class ColorParse : IParse
    {
        public AstNode Parse(Parser parser)
        {
            var color = parser.Current;

            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.NextToken();

            //llenar hasta q venga un parentesis 
            var tokens = new List<Token>();

            while (parser.Current.type != TypeToken.CloseParenthesis)
            {
                System.Console.WriteLine($"Add {parser.Current.value}");
                tokens.Add(parser.Current);
                parser.NextToken();
            }

            //luego avanzar al sig  tokens para seguir avanzando en el analisis 
            parser.NextToken();

            var exp = Converter.GetExpression(tokens);

            return new ColorNode(color, exp);
        }
    }

}



#endregion