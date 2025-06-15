
using Evalua;
using ExpressionesTipos;
using IParseo;
using metodos;
using Parseando;
using UnityEngine;

namespace ArbolSintaxisAbstracta
{
    public class FillNode : AstNode
    {
        Token fill;
        public FillNode(Token fill)
        {
            this.fill = fill;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            Debug.Log("El Fill siempre se define bien ");
            return true;

        }

        public override object? Evaluate(Evaluator? evaluador = null)
        {
            Debug.Log("Ejecutar Evaluate  Fill ");
            //metodo 
            Metodos.Fill();

            if (evaluador != null)
            {
                evaluador.Move();
            }

            return 0;
        }
    }



    public class FillParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            var fill = parser.Current;

            parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            parser.ExpectedTokenType(TypeToken.CloseParenthesis);
            
            //para seguir avanzando con la logica de los demas nodos 
            parser.NextToken();
        

            return new FillNode(fill);
        }
    }

}