using System;
using Expresion;
using ArbolSintaxisAbstracta;
using ExpressionesTipos;
using ExpressionesUnarias;
using SumaNode;
using RestaNode;
using MultiplicacionNode;
using PowNode;
using DivisionNode;
using RestoNode;

namespace ExpressionesBinarias
{

    #region BinaryExpression
    public abstract class BinaryExpression : Expression
    {
        public Expression? LeftExpression { get; private set; }
        public Expression? RightExpression { get; private set; }

        public Token Operator { get; private set; }


        //builder
        public BinaryExpression(Expression LeftExpression, Token Operator, Expression RightExpression)
        {
            this.LeftExpression = LeftExpression;
            this.Operator = Operator;
            this.RightExpression = RightExpression;
        }


        //find type
        public override ExpressionTypes GetTipo()
        {
            if (LeftExpression != null && RightExpression != null)
            {

                //Dar el tipo de la izquierda

                if (LeftExpression is UnaryExpression @unary)
                {
                    System.Console.WriteLine("Left es UnaryExpression");
                    LeftExpression.type = @unary.GetTipo();
                }
                if (LeftExpression is BinaryExpression binaryexpression)
                {
                    System.Console.WriteLine(" Left es Binary Expression");
                    LeftExpression.type = binaryexpression.GetTipo();
                }

                System.Console.WriteLine($"Es una operacion {Operator.value}");

                //Dar el tipo de la derecha


                if (RightExpression is BinaryExpression binaryExpression)
                {
                    System.Console.WriteLine("Right es binary Expression");
                    RightExpression.type = binaryExpression.GetTipo();
                }

                if (RightExpression is UnaryExpression unaryExpression)
                {
                    System.Console.WriteLine("Right es una Expression");
                    RightExpression.type = unaryExpression.GetTipo();
                }


                //cuando ya tenga los tipos veirifica si son iguales 
                System.Console.WriteLine($"Tipo Left {LeftExpression.type}     Tipo RIght {RightExpression.type}");
                if (LeftExpression.type == RightExpression.type)
                {
                    return LeftExpression.type;
                }

                else
                {
                    System.Console.WriteLine("Son tipos diferentes ");
                    return ExpressionTypes.Invalid;
                }


            }
            return ExpressionTypes.Null;
        }

        public override bool CheckSemantic()
        {
            var type = this.GetTipo();

            if (type == ExpressionTypes.Invalid)
            {
                //entocnes no es una expresion  valida , 
                return false;
            }

            else
                return true;
        }
        public override object Evaluate()
        {
            switch (Operator.value)
            {
                case "+":
                    return (SumaParse)this.Evaluate();

                case "-":
                    return (RestaParse)this.Evaluate();

                case "*":
                    return (MultiplicationParse)this.Evaluate();
                case "**":
                    return (PowParse)this.Evaluate();

                case "/":
                    return (DivisionParse)this.Evaluate();

                case "%":
                    return (RestoParse)this.Evaluate();


            }

            return "null";
        }
    }





    #endregion

}