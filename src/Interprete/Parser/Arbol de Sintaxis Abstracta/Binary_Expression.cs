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
using TerminalesNode;

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

                if (LeftExpression is TerminalExpression te )
                {
                    System.Console.WriteLine("Left es Terminal");
                    LeftExpression.type = te.GetTipo();
                }
                if(LeftExpression is BinaryExpression binaryexpression)
                {
                    System.Console.WriteLine(" Left es Binary Expression");
                    LeftExpression.type = binaryexpression.GetTipo();
                }
                if (LeftExpression is UnaryExpression unary)
                {
                    System.Console.WriteLine("Left es UnaryExpression");
                    LeftExpression.type = unary.GetTipo();
                }

                System.Console.WriteLine($"Es una operacion {Operator.value}");

                //Dar el tipo de la derecha


                if (RightExpression is BinaryExpression binaryExpression)
                {
                    System.Console.WriteLine("Right es binary Expression");
                    RightExpression.type = binaryExpression.GetTipo();
                }

                if (RightExpression is TerminalExpression terminal)
                {
                    System.Console.WriteLine("Right es una Expression");
                    RightExpression.type = terminal.GetTipo();
                }

                if (RightExpression is UnaryExpression ue)
                {
                    System.Console.WriteLine("Right es unary");
                    RightExpression.type = ue.GetTipo();
                }


                //cuando ya tenga los tipos veirifica si son iguales 
                System.Console.WriteLine($"Tipo Left {LeftExpression.type}     Tipo RIght {RightExpression.type}");
                if (LeftExpression.type == RightExpression.type)
                {

                    //en el caso de ser iguales da el valor 
                    return LeftExpression.type;
                }

                else
                {
                    System.Console.WriteLine("Son tipos diferentes ");
                    return ExpressionTypes.Invalid;
                }


            }
            return ExpressionTypes.Invalid;
        }

        public override bool CheckSemantic(ExpressionTypes tipo)
        {
            return base.CheckSemantic(tipo);
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