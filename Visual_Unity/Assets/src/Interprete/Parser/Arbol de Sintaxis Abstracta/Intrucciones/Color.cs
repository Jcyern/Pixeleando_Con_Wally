
using System.Collections.Generic;
using ArbolSintaxisAbstracta;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using metodos;
using Parseando;
using PincelUnity_;
using UnityEngine;

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
                Debug.Log("Si es color");
                //si es un color
                return true;
            }
            //la expression no es un string 
            //no es un color 

            Debug.Log("No es un color");
            compilingError.Add(new ExpectedType(Color.Pos, ExpressionTypes.Color.ToString(), color.GetTipo().ToString()));
            return false;
        }


        public override object? Evaluate(Evaluator? evaluador = null)
        {
            Debug.Log("Evaluate Color ");
            var c = color!.Evaluate()!.ToString()!;
            //cambiar color del pincel 
            Pincel.ChangeColor(c);
            //metodo  para cmabiar color desde unity 
            
                        //mover pos en el evaluador 
            if (evaluador != null)
                evaluador.Move();
                
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
                Debug.Log($"Add {parser.Current.value}");
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