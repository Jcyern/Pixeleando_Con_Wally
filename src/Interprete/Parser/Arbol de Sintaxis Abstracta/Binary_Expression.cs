using System;
using Expresion;
using ArbolSintaxisAbstracta;
using ExpressionesTipos;
using ExpressionesUnarias;
using Suma;
using Resta;
using Multiplicacion;
using Pow;
using Division;
using Resto;
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

        #region  Get Tipo

        //find type
        public override ExpressionTypes GetTipo()
        {
            if (LeftExpression != null && RightExpression != null)
            {

                //Dar el tipo de la izquierda

                if (LeftExpression is TerminalExpression te)
                {
                    System.Console.WriteLine("Left es Terminal");
                    LeftExpression.type = te.GetTipo();

                    System.Console.WriteLine($"Left Expression {LeftExpression.type}");
                }
                if (LeftExpression is BinaryExpression binaryexpression)
                {
                    System.Console.WriteLine(" Left es Binary Expression");
                    LeftExpression.type = binaryexpression.GetTipo();
                    System.Console.WriteLine($"Left Expression {LeftExpression.type}");
                }
                if (LeftExpression is UnaryExpression unary)
                {
                    System.Console.WriteLine("Left es UnaryExpression");
                    LeftExpression.type = unary.GetTipo();
                    System.Console.WriteLine($"Left Expression {LeftExpression.type}");
                }
                

                System.Console.WriteLine($"Es una operacion {Operator.value}");

                //Dar el tipo de la derecha


                if (RightExpression is BinaryExpression binaryExpression)
                {
                    System.Console.WriteLine("Right es binary Expression");
                    RightExpression.type = binaryExpression.GetTipo();
                    System.Console.WriteLine($"RightExpression { RightExpression.type}");
                }

                if (RightExpression is TerminalExpression terminal)
                {
                    System.Console.WriteLine("Right es una  Terminal Expression");
                    RightExpression.type = terminal.GetTipo();
                    System.Console.WriteLine($"RightExpression { RightExpression.type}");
                }

                if (RightExpression is UnaryExpression ue)
                {
                    System.Console.WriteLine("Right es unary");
                    RightExpression.type = ue.GetTipo();
                    System.Console.WriteLine($"RightExpression { RightExpression.type}");
                }


                //cuando ya tenga los tipos veirifica si son iguales 
                System.Console.WriteLine($"Tipo Left {LeftExpression.type}     Tipo RIght {RightExpression.type}");
                if (LeftExpression.type == RightExpression.type)
                {
                    System.Console.WriteLine("Iguales");
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

        #endregion

        #region  Semantica

        public override bool CheckSemantic(ExpressionTypes tipo= ExpressionTypes.nothing)
        {
            
            //verifica q ambas expresiones son del mismo tipo y en casos especificos si coincide con el tipo pasado 

            //una expresiones binarias en genral casi siempre se puede proceder si son del mismo tipo que cada quien implemente sus casos particulares

            if (LeftExpression != null && RightExpression != null)
            {
                var r = RightExpression.GetTipo();
                var l = LeftExpression.GetTipo();

                //son iguale y ninguna invalida 
                if (r == l && l != ExpressionTypes.Invalid)
                {
                    //en el caso de q pasen tipo verificar si es el pedido 
                    if (tipo == ExpressionTypes.nothing)
                    {
                        return true;
                    }
                    //sino es nothing el tipo verifica q sea el tipo pedido 

                    else if (r == tipo)
                    {
                        return true;
                    }

                    //en este caso no seria el tipo demandado 
                    return false;
                }
                //caso de q no sean iguales o invalidas
                //agregar errores 

                return false;

            }
            return false;
        }


        #endregion




        #region  Evaluate 
        public override object Evaluate()
        {
            switch (Operator.value)
            {
                case "+":
                    return (SumaNode)this.Evaluate();

                case "-":
                    return (RestaNode)this.Evaluate();

                case "*":
                    return (MultiplicationNode)this.Evaluate();
                case "**":
                    return (PowNode)this.Evaluate();

                case "/":
                    return (DivisionNode)this.Evaluate();

                case "%":
                    return (RestoNode)this.Evaluate();


            }

            return "null";
        }

        

        #endregion
    }





    #endregion

}