using  IParseo;
using Errores;
using System.Diagnostics;
using System.Net.NetworkInformation;
using ExpressionesTipos;

namespace ArbolSintaxisAbstracta
{


public abstract class AstNode : IParse
{
    public (int fila ,int columna ) Location ;
    public static List<Error> compilingError = new List<Error>();
    public void Parse()
    {
    }

    //tiene que saberse chequearse semanticamnente 
    public virtual  bool CheckSemantic( )
    {
        //logica para ver los parametros de mi metodo 
        return false ;
    }
}

public abstract class Expression :AstNode
{
    public object? value ;

    public ExpressionTypes type ;
    public virtual object Evaluate ()
    {
        return "null";
    }

    public virtual ExpressionTypes GetTipo()
    {
        if(this is Number number)
        {
            return number.GetTipo();
        }

        else if(this is Bool @bool)
        {
            return @bool.GetTipo();
        }

        else if( this is String @string)
        {

            return @string.GetTipo();
        }


        else if( this is BinaryExpression expression)
        {
            // llamar a el GetTyoe de BinaryExpression
            System.Console.WriteLine(" Es binary Expression");
            return expression.GetTipo();
        }

        else 
        {
            return ExpressionTypes.Invalid;
        }
    }


        public override bool CheckSemantic()
        {
            // semanticas de las expresiones Generales 

            var type = this.GetTipo();

            if( type == ExpressionTypes.Invalid || type== ExpressionTypes.Null)
            {
                return false ;
            } 
            else 
            return true ;
        }
}
public  class Number : Expression
{
    public  Number (  object number  , (int,int)pos )
    {
        this.value  = number;
        this.Location = pos;
    }

        public override ExpressionTypes GetTipo()
        {
            if(this.value == null)
            {

                System.Console.WriteLine("es Null  ( Number) ");
                return ExpressionTypes.Null;
            }

            else 
            { 
                System.Console.WriteLine($" Es numero  - valor {this.value}");
                return ExpressionTypes.Number;
            }
        }



    public override object Evaluate()
    {
        if(value != null)
        return value;

        return 0;
    }
}

public class String :Expression
{

    public String ( object value , (int,int)Location)
    {
        this.value = value ;
        this.Location = Location;
    }

        public override ExpressionTypes GetTipo()
        {
            if(this.value == null )
            return  ExpressionTypes.Null;

            else 
            return ExpressionTypes.String;

        }

        public override object Evaluate()
        {
            if(value != null)
            return value;

            return"";
        }
}

public class Bool : Expression
{

    public Bool ( object value , (int,int ) Location)
    {
        this.value = value ;
        this.Location = Location;
    }


        public override ExpressionTypes GetTipo()
        {
            if(value == null)
            {
                System.Console.WriteLine("Null Bool");
                return ExpressionTypes.Null;
            }
            else 
            {
                System.Console.WriteLine($" Es bool - valor {value}");
                return ExpressionTypes.Bool;
            }
        }



    public override object Evaluate()
    {
        if(value != null)
        return value ;

        return false;
    }
}

public abstract class BinaryExpression : Expression
{
    public Expression? LeftExpression   {get;private set;}
    public Expression? RightExpression  {get;private set;}

    public Token  Operator  {get; private set;}


    public BinaryExpression(  Expression LeftExpression , Token Operator , Expression RightExpression) 
    {
        this.LeftExpression = LeftExpression;
        this.Operator = Operator;
        this.RightExpression = RightExpression ;
    }

        public override ExpressionTypes GetTipo()
        {
            if(LeftExpression!= null && RightExpression!= null)
            {
            
                //Dar el tipo de la izquierda

                if(LeftExpression is not BinaryExpression )
                {
                    System.Console.WriteLine("Left es Expression");
                    LeftExpression.type = ((Expression)LeftExpression).GetTipo();
                }
                if( LeftExpression is BinaryExpression expression)
                {
                    System.Console.WriteLine(" Left es Binary Expression");
                    LeftExpression.type =  expression.GetTipo();
                }

                System.Console.WriteLine($"Es una operacion {Operator.value}");

                //Dar el tipo de la derecha


                if(RightExpression is BinaryExpression)
                {
                    System.Console.WriteLine("Right es binary Expression");
                    RightExpression.type =  ((BinaryExpression)RightExpression).GetTipo();
                }

                if ( RightExpression is not BinaryExpression)
                {
                    System.Console.WriteLine("Right es una Expression");
                    RightExpression.type = ((Expression)RightExpression).GetTipo();
                }


                //cuando ya tenga los tipos veirifica si son iguales 
                System.Console.WriteLine($"Tipo Left {LeftExpression.type}     Tipo RIght {RightExpression.type}");
                if(LeftExpression.type == RightExpression.type)
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

            if(type == ExpressionTypes.Invalid)
            {
                //entocnes no es una expresion  valida , 
                return false ;
            }

            else
            return true ;
        }
        public override object Evaluate()
        {
            switch(Operator.value)
            {
                case "+":
                return (SumaParse)this.Evaluate();

                case "-":
                return (RestaParse)this.Evaluate();

                case "*":
                return (MultiplicationParse)this.Evaluate();
                case "**":
                return (PowParse)this.Evaluate();

                case"/":
                return (DivisionParse)this.Evaluate();

                case "%":
                return (RestoParse)this.Evaluate();

            
            }

            return "null";
        }
}




# region Asignaciones

public class AsignationNode : AstNode
{
    public Token? Identificador {get; private set;}

    public Token? AsignationOperator {get; private set;}

    public Expression? Expression {get;private set;}


    public  void Parse(Token Identificador , Token AsignationOperator , Expression expression )
    {
        this.Identificador = Identificador;
        this. AsignationOperator = AsignationOperator;
        this.Expression = expression;
    }

    //Hay que Hacer algo para guardar la variable  en un diccionario 

}


#endregion 



#region Aritmeticas



public class SumaParse : BinaryExpression
{
    public SumaParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
    {
    }



    public override object Evaluate()
    {             
            
            return Convert.ToInt32(LeftExpression!.Evaluate()) + Convert.ToInt32(RightExpression!.Evaluate()); 
        
    }
}


public class RestaParse : BinaryExpression
{
    public RestaParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
    {
    }
    

    public override object Evaluate()
    {
            return Convert.ToInt32(LeftExpression!.Evaluate()) - Convert.ToInt32(RightExpression!.Evaluate());

    }


}

public class MultiplicationParse : BinaryExpression
{
    public MultiplicationParse(Expression LeftExpression, Token  Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
    {
    }

    public override object Evaluate()
    {
            return Convert.ToInt32(LeftExpression!.Evaluate()) * Convert.ToInt32(RightExpression!.Evaluate());

    }

}

    public class DivisionParse : BinaryExpression
    {
        public DivisionParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.Evaluate()) / Convert.ToInt32(RightExpression!.Evaluate());
            
        }


    }


    public class PowParse : BinaryExpression
{
    public PowParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
    {

    }

    public override object Evaluate()
    {
            return Math.Pow(Convert.ToDouble(LeftExpression!.Evaluate()), Convert.ToDouble(RightExpression!.Evaluate()));
    }


    
}


    public class RestoParse : BinaryExpression
    {
        public RestoParse(Expression LeftExpression, Token Operator, Expression RightExpression) : base(LeftExpression, Operator, RightExpression)
        {
        }

        public override object Evaluate()
        {
            return Convert.ToInt32(LeftExpression!.value)   % Convert.ToInt32(RightExpression!.value); 
        }

    }

    //Converter de infix to posfix
public class Converter 
{
    private  static Dictionary<string , int> precedence = new()
    {
        ["+"]=1,
        ["-"]=1,
        ["*"]=2,
        ["/"]=2,
        ["%"]=2,
        ["**"]=3,
        ["("]=0  //en este caso la menor precedencia para q a la hora de q venga cualquier operador pueda entrar a la pila  y siga el trancurso de la con version sin  afectar el orden de las demas precedencias , ejemplo q tenga (5+4)   y a la hora de comparar + con (  no salga el (  y entre el mas a la pila ----- ya q en el lenguaje postfix no afecta el parentesis 
    };

    public static List<Token> PostfixExpression(  List<Token> infix)
    {
        List<Token> postfix = new ();
        //crear un pila donde guardaremos los operadores por orden de precedencia  
        Stack<Token> operadores = new Stack<Token>();

        for(int i =0 ; i<infix.Count; i ++)
        {

            //si es un numero agregarlo directamente a la salida 
            if(infix[i].type == TypeToken.Numero)
            {System.Console.WriteLine($"Numero add {infix[i].value}");
                postfix.Add(infix[i]);
            }
            //si es ( // meterlo en la pila
            else if (infix[i].type == TypeToken.OpenParenthesis)
            {
                System.Console.WriteLine("Open ( meterlo");
                operadores.Push(infix[i]);
            }
            else if ( infix[i].type == TypeToken.CloseParenthesis)
            {

                //mientras que el ultimo no sea ( en la pila de operadores
                while(operadores.Peek().type != TypeToken.OpenParenthesis)
                {
                    //agrega el operador a la pila
                    System.Console.WriteLine($"Add {operadores.Peek().value}");
                    postfix.Add(operadores.Pop());
                
                }
                Debug.Print("Se encontro el open ");
                var result = operadores.Pop();
                Debug.Print(result.value);
                

            }
            else if( infix[i].type == TypeToken.Operador )
            {


                if(operadores.Count==0)
                {
                    System.Console.WriteLine($"OPeradores vacio meter {infix[i].value}");
                    //si no hay operadores metelo 
                    operadores.Push(infix[i]);
                }
                //ver si tiene mayor precedencia 
                
                else if(precedence[infix[i].value]>= precedence[operadores.Peek().value])
                {
                    //si es mayor su precedenci metelo 
                    System.Console.WriteLine("Meter ");
                    System.Console.WriteLine($" {infix[i].value} > precedence {operadores.Peek()}");
                    operadores.Push(infix[i]);
                }
                //si tiene menor precedencia saca las cosas hasta q el quede de mayor precednecia 
                else if(precedence[infix[i].value] <precedence[operadores.Peek().value])
                {
                    //mientras q tenga menor precedencia , saca los operadoresy agregalos a la infix list
                    while (operadores.Count> 0 &&precedence[infix[i].value]<precedence[operadores.Peek().value])
                    {
                        System.Console.WriteLine($"agregar a postfix {operadores.Peek()}");
                        postfix.Add(operadores.Pop());
                    }

                    //cuando ya tenga mayor o iugal predencia o operadores se quede vacio , agregala
                    operadores.Push(infix[i]);
                }
            }
        }

        //cuando llegue al final , si quedan operdores en la pila , agregalos a la salida

        while(operadores.Count >0)
        {
            postfix.Add(operadores.Pop());
        }
        System.Console.WriteLine("Dentro ");
        foreach(var item in postfix)
        System.Console.WriteLine(item.value);

        return postfix;
    }


    public static Expression? AritmeticExpression (List<Token> postfix)
    {
        Stack<Expression> pila = new ();
        System.Console.WriteLine("postfix");
        foreach( var item in postfix)
        System.Console.WriteLine(item.value);


        for(int i = 0 ; i<postfix.Count ; i ++)
        {
            if(postfix[i].type== TypeToken.Numero)
            {
                pila.Push(new Number(postfix[i].value, postfix[i].Pos));
            }
            else if( postfix[i].type == TypeToken.Operador) //es un operador
            {
                var Right = pila.Pop();
                var Left = pila.Pop();

                // realizar la operacion correspondiente
                switch(postfix[i].value)
                {
                    case "+":
                    pila.Push(new SumaParse(Left,postfix[i] ,Right));
                    break;

                    case "-":
                    pila.Push(new  RestaParse(Left, postfix[i], Right));
                    break;

                    case "*":
                    pila.Push(new MultiplicationParse(Left, postfix[i], Right));
                    break;

                    case "**":
                    pila.Push(new PowParse(Left,postfix[i], Right));
                    break;

                    case "/":
                    pila.Push(new DivisionParse(Left, postfix[i], Right));
                    break;
                    case "%":
                    pila.Push(new RestoParse(Left,postfix[i],Right));
                    break;
                }
            }
        }


        //cuando se termine todo tiene q quedar una expresion  o no 
        if(pila.Count>0)
        return pila.Pop();


        //si no hay nada retona null expression 
        return null;
    }
}





#endregion




}