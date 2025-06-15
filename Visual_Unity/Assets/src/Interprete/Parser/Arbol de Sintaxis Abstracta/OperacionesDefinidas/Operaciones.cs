
using System;
using System.Collections.Generic;
using Arbol_de_Sintaxis_Abstracta;
using Expresion;
using ExpressionesBinarias;

namespace Operaciones
{
    public class Operations
    {
        //en esta clase tendremos un diccionario donde etan definidas todas las operaciones 

        //y metodos por si quermeos anadir una nueva , verificar si existe 

        public static  object EvaluateAs<T>(Expression Node) where T : Expression
        {
            return (T)Node.Evaluate()!;
        }
        public  static Dictionary<string, Func<Expression, object>> operaciones = new()
        {
            ["||"] = EvaluateAs<OrNode> ,
            ["&&"] = EvaluateAs<AndNode>,
            ["=="] = EvaluateAs<EqualNode>,
            ["!="] = EvaluateAs<NotEqualsNode>,
            [">"]  = EvaluateAs<BiggerThanNode>,
            ["<"]  = EvaluateAs<LessThanNode>,
            [">="] = EvaluateAs<BiggerEqualThanNode>,
            ["<="] = EvaluateAs<LessEqualThanNode>,
            ["+"]  = EvaluateAs<SumaNode>,
            ["-"]  = EvaluateAs<RestaNode>,
            ["*"]  = EvaluateAs<MultiplicationNode>,
            ["/"]  = EvaluateAs<DivisionNode>,
            ["%"]  = EvaluateAs<RestoNode>,
            ["**"] = EvaluateAs<PowNode>
        };
    }
}