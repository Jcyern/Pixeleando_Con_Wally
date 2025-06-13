
using System.Diagnostics;
using Aparecer;
using ArbolSintaxisAbstracta;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;
using Utiles;

namespace Aparecer
{
    public class SpawnNode : AstNode
    {
        //pos de aparicion del wally 
        Expression? fila;
        Expression? columna;

        Token spawn;

        public SpawnNode(Token spawn, Expression? fila, Expression? columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.spawn = spawn;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (fila != null && columna != null)
            {
                var tipo_fila = fila.GetTipo();
                var tipo_columna = columna.GetTipo();
                System.Console.WriteLine("Chequeo de Spawn");
                if (tipo_fila == tipo_columna && tipo_fila == ExpressionTypes.Number)
                {
                    System.Console.WriteLine("ambas expresiones son numericas");
                    return true;
                }
                if (tipo_fila != ExpressionTypes.Number)
                {
                    System.Console.WriteLine("La expression fila no devuelve un numero ");
                    //agregar a los errores
                    compilingError.Add(new ExpectedType(fila.Location, ExpressionTypes.Number.ToString(), tipo_fila.ToString()));
                }

                if (tipo_columna != ExpressionTypes.Number)
                {
                    System.Console.WriteLine("La expression columna no devuelve un numero ");
                    //agregar error 
                    compilingError.Add(new ExpectedType(columna.Location, ExpressionTypes.Number.ToString(), tipo_columna.ToString()));
                }
                return false;
            }

            if (fila == null)
            {
                //agregar error de q Spawn el lado izq es null
                compilingError.Add(new ExpectedType(spawn.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
            }
            if (columna == null)
            {
                //dar error que el lado derecho de Spawn es null 
                compilingError.Add(new ExpectedType(spawn.Pos, ExpressionTypes.Number.ToString(), ExpressionTypes.Null.ToString()));
            }

            return false;
        }


        public override object? Evaluate(Evaluator? evaluador = null)
        {
            var f = Convert.ToInt32(fila!.Evaluate());
            var c = Convert.ToInt32(columna!.Evaluate());

            //verifica que el Spawn esta dentro de las dimensines del tablero 
            var isf = f < Wally.Pos.Fila && f >= 0;
            var isc = c < Wally.Pos.Columna && c >= 0;


            if (isf && isc)
            {
                System.Console.WriteLine($"se coloco al wally en la pos fila: {f}  columna: {c}");
                Wally.Pos.Fila = f; Wally.Pos.Columna = c;
            }

            else
            {
                //dar error de fuera de rango del tablero 
                if (evaluador != null)
                {
                    evaluador.AddError(new SpawnOutOfRange(spawn.Pos, f, c));
                }
            }

            //metodo para pos al wally en la parte visual 

            //mover pos en el evaluador 
            if (evaluador != null)
                evaluador.Move();

            return 0;
            
        }
    }
}



//definienfo el Parser de Spawn

public class SpawnParser : IParse
{
    public AstNode Parse(Parser parser)
    {
        Debug.Print("Parseando Spawn");
        //se supone q la palabra actual es Spawn es un Key Word
        var spawn = parser.Current;
        //esperar un parentesis 
        parser.ExpectedTokenType(TypeToken.OpenParenthesis);
        parser.NextToken();
        //recorrer hasta q encuentra un parentesis o una coma 
        var fila_exp = new List<Token>();

        Expression? exp_izq = null;
        Expression? exp_der = null;

        while (parser.Current.type != TypeToken.CloseParenthesis && parser.Current.type != TypeToken.Coma)
        {
            System.Console.WriteLine($"Agregando izq a {parser.Current.value}");
            //ir metiendo los tokens 
            fila_exp.Add(parser.Current);
            parser.NextToken();
        }
        exp_izq = Converter.GetExpression(fila_exp);

        //luego de la expresion debe venir una coma 
        if (parser.Current.type == TypeToken.Coma)
        {
            var columna_exp = new List<Token>();
            parser.NextToken();
            //construye la expresion derecha
            //retornarmel nodo creado de spawn 
            while (parser.Current.type != TypeToken.CloseParenthesis)
            {
                System.Console.WriteLine($"Agregando a der a {parser.Current.value}");
                columna_exp.Add(parser.Current);
                parser.NextToken();

            }

            exp_der = Converter.GetExpression(columna_exp);
            parser.NextToken(); //para seguir evaluando lo demas tokens 
            System.Console.WriteLine("Retornar nodo de Spawn");
            return new SpawnNode(spawn, exp_izq, exp_der);
        }
        else
        {
            //es un error ya q termino en parenteiss en teoria solo hayb una sola expresion
            //agregar erorr de spwan 
            //returnra null y las exp faltants las dara como error
            parser.NextToken(); //para seguir evaluando las expresiones
            System.Console.WriteLine("Retorna nodo de Spawn"); 
            return new SpawnNode(spawn, exp_izq, exp_der);
        }

        
    }
}