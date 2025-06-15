
using System;
using System.Collections.Generic;
using UnityEngine;
using Alcance;
using ArbolSintaxisAbstracta;
using Convertidor_Pos_Inf;
using Errores;
using Evalua;
using Expresion;
using ExpressionesTipos;
using IParseo;
using Parseando;

namespace Go
{
    public class GoToNode : AstNode
    {
        Token go;
        Expression? Condition;
        Token Label;


        public GoToNode(Token go, Expression? Condition, Token Label)
        {
            this.go = go;
            this.Condition = Condition;
            this.Label = Label;
        }


        public override bool CheckSemantic(ExpressionTypes tipo = ExpressionTypes.nothing)
        {
            if (Condition != null)
            {   
                Debug.Log("Chequeo Sem de la Condition ");
                //aqui se asume q las etiquetas existen por si se define lineas mas abajo del Goto 

                //solo verificar si la condition devuelve un valor booleano 
                return Condition.CheckSemantic(ExpressionTypes.Bool);
            }
            else
            {
                if (Condition == null)
                {
                    //agregar error de null 
                    compilingError.Add(new ExpectedType(go.Pos, ExpressionTypes.Bool.ToString(), ExpressionTypes.Null.ToString()));
                }
                if (Label == null)
                {
                    compilingError.Add(new ExpectedType(go.Pos, ExpressionTypes.Bool.ToString(), ExpressionTypes.Null.ToString()));
                }

                return false;
            }
            //agregar error de q condition esta null

        }


        public override object? Evaluate(Evaluator? evaluador = null)
        {
            if (evaluador != null)
            {
                Debug.Log("Goto");
                //verifica si el Label existe 
                if (Scope.Labels.ContainsKey(Label.value))
                {

                    var cond = Condition!.Evaluate();

                    //verifica si la condicion es verdadera
                    if (Convert.ToBoolean(cond))
                    {
                        Debug.Log("Codition verdadera");
                        //si es verdadera la condicion
                        //ir a la etiquea , en el caso de q la etiqueta este por debaja , no aumentar las calls 
                        var result = evaluador.Call();

                        if (result)
                        {
                            Debug.Log("se puede hacer llamada");
                            //SI SE PUEDE LLAMAR CAMBIAR LA POS DEL EVALUADOR 
                            var linea = Scope.Labels[Label.value];
                            Debug.Log($"Salto {linea}");

                            //no se debe reiniciar las llamadas cuando la lineas es mas para abajo pq puede provocar un desboradamiento de pila
                            evaluador.CurrentLinea = linea;
                            return 0;
                        }
                        else
                        {
                            //es un error de stack overflow //
                            //poner a la linea al final de los nodos para q no siga analizando 
                            evaluador.AddError(new GoToOverFlow(go.Pos));
                            //ir al end y romper 
                            evaluador.End();
                            return 0;

                        }
                    }
                    else
                    {
                        //en el caso de no serlo sigue por donde iba
                        evaluador.RestartCalls();
                        //mover pos en el evaluador 
                        if (evaluador != null)
                            evaluador.Move();
                        return 0;
                    }
                }
                else
                {
                    //sino dar error de evaludaodr que no existe el label
                    evaluador.AddError(new LabelError(go.Pos, Label.value));
                    evaluador.Move();
                    return 0;
                }
            }
            else
            {
                Debug.Log("El Evaluador es null");
                return 0;
            }
        }
    }

    #region  GoToParse
    public class GoToParse : IParse
    {
        public AstNode? Parse(Parser parser)
        {
            //el token actual es go to 
            var go = parser.Current;

            //ir al sig tiene q ser una \
            //definir el token [

            Debug.Log("Parsear go to ");


            parser.ExpectedTokenType(TypeToken.OpenBraze);
            parser.ExpectedTokenType(TypeToken.Identificador);  // el sig tiene q ser un Identificador pelado q seria la Label
            var Label = parser.Current;
            parser.ExpectedTokenType(TypeToken.CloseBraze);

            var result = parser.ExpectedTokenType(TypeToken.OpenParenthesis);
            if (result)
            {
                parser.NextToken();

                //seguir hasta q se encuentre el close parentesis 
                var tokens = new List<Token>();
                while (parser.Current.type != TypeToken.CloseParenthesis)
                {
                    //ve agregando el actual y asi 
                    tokens.Add(parser.Current);
                    parser.NextToken();
                }
                //el actual seria el parentesis 
                //sig para seguir analizando
                parser.NextToken();

                //convertir a exp 

                Expression? condition = Converter.GetExpression(tokens);

                return new GoToNode(go, condition, Label);
            }
            else
                return null;
        }
    }

    #endregion
}