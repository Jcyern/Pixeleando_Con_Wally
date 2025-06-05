
using Convertidor_Pos_Inf;
using Errores;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Numero;
using Parseando;
using Utiles;

namespace ArbolSintaxisAbstracta
{
    public class DrawLineNode : AstNode
    {
        Token Draw;
        Expression? dirX;
        Expression? dirY;

        Expression? distancia;


        public DrawLineNode(Token Draw, Expression? dirX, Expression? dirY, Expression? distance)
        {
            this.Draw = Draw;
            this.dirX = dirX;
            this.dirY = dirY;
            this.distancia = distance;
        }

        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (dirX == null || dirY == null || distancia == null)
            {
                if (dirX == null)
                {
                    compilingError.Add(new ExpectedType(Draw.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                }
                if (dirY == null)
                {
                    compilingError.Add(new ExpectedType(Draw.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                }
                if (distancia == null)
                {
                    compilingError.Add(new ExpectedType(Draw.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
                }
                return false;
            }

            if (dirX!.CheckSemantic(ExpressionTypes.Number) && dirY!.CheckSemantic(ExpressionTypes.Number) && distancia!.CheckSemantic(ExpressionTypes.Number))
                {
                    return true;
                }


            return false;

        }
        public override object? Evaluate()
        {
            System.Console.WriteLine("Verificar si es una Direccion Valida");

            var x = Convert.ToInt32(dirX!.Evaluate());
            var y = Convert.ToInt32(dirY!.Evaluate());
            var d = Convert.ToInt32(distancia!.Evaluate());

            if (Wally.Pos.Fila != int.MaxValue)
            {
                var range_x = InRange(x, -1, 1);
                var range_y = InRange(y, -1, 1);

                if (range_x && range_y)
                {
                    System.Console.WriteLine($"Mover una Linea en la dir {x},{y} desde {Wally.Pos.Fila}, {Wally.Pos.Columna}  distancia {d} ");
                    return 0;
                }
                if (!range_x)
                {
                    //crear error de rango de x
                    System.Console.WriteLine("error de rango de x");
                }
                if (!range_y)
                {
                    System.Console.WriteLine("error de rango de y ");
                    //crear error de rango de y
                }
                return 0;
            }
            else
            {
                //dar error de q no hay spawn 
                System.Console.WriteLine("El wally no tiene una pos");
            }
            return 0;
            
        }

        public static bool InRange(int valor, int min, int max)
        {
            return valor >= min && valor <= max;
        }


    }


    public class DrawLineParse : IParse
    {
        public AstNode Parse(Parser parser)
        {
            System.Console.WriteLine("Parseando DrawLine");
            var draw = parser.Current;
            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.NextToken();
            //ahi empieza las expresiones 

            var token_disx = new List<Token>();
            while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
            {
                System.Console.WriteLine($"Se agrega a DisX {parser.Current}" );
                //ir agregando los tokens q seria las dist_X
                token_disx.Add(parser.Current);
                parser.NextToken();
            }
            //exp de distancia X
            var distX = Converter.GetExpression(token_disx);

            //verificar si es el close parentesis seria un error 
            if (parser.Current.type == TypeToken.Coma)
            {
                parser.NextToken();
                System.Console.WriteLine("Curretn es difrente de Close de Parentesis");
                System.Console.WriteLine("Hay distX ver  lo demas ");

                // tokens token dist Y
                var token_disy = new List<Token>();

                while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
                {
                    System.Console.WriteLine($"Se agrega a DistY {parser.Current}");
                    token_disy.Add(parser.Current);
                    parser.NextToken();
                }

                var distY = Converter.GetExpression(token_disy);

                if (parser.Current.type == TypeToken.Coma)
                {
                    parser.NextToken();
                    System.Console.WriteLine("Hay dist y");
                    var tokens_distancia = new List<Token>();
                    

                    while (parser.Current.type != TypeToken.CloseParenthesis)
                    {
                        System.Console.WriteLine($"Se agrego a Dist{parser.Current}");
                        tokens_distancia.Add(parser.Current);
                        parser.NextToken();
                    }
                    parser.NextToken();

                    var dist = Converter.GetExpression(tokens_distancia);
                    return new DrawLineNode(draw, distX, distY, dist);
                    
                }
                else
                {
                    parser.NextToken();
                    System.Console.WriteLine("es un error falta distancia  ");
                    return new DrawLineNode(draw, distX, distY, null);
                }
            }

            else
            {
                parser.NextToken();
                System.Console.WriteLine("es un error falta diry y  distancia  ");
                return new DrawLineNode(draw, distX, null, null);
            }
        }
    }
}