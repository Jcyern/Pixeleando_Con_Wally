
using System.Collections.Generic;
using ArbolSintaxisAbstracta;
using Errores;
using UnityEngine;

namespace Evalua
{
    public class Evaluator
    {
        List<AstNode> nodos;
        public List<Error> error_evaluate;
        public int CurrentLinea= 0;

        int CantCall = 0;

        const int Max_Call = 1000; //maximo de llamdas recursivas para q de el overflow

        public void RestartCalls()
        {
            CantCall = 0;
        }

        public void End()
        {
            CurrentLinea = nodos.Count;
        }
        public bool Call()
        {
            if (CantCall < Max_Call)
            {
                CantCall += 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        public Evaluator(List<AstNode> nodos)
        {
            this.nodos = nodos;
            error_evaluate = new List<Error>();
            this.CurrentLinea = 0;
        }

        public void GetErrores()
        {
            if (AstNode.compilingError.Count > 0)
            {
                foreach (var e in AstNode.compilingError)
                {
                    e.ShowError();
                }
            }
            else
            {
                Debug.Log("NO hay errores en el Evaluate ");
                return;
            }
        }

        public void AddError(Error e)
        {
            error_evaluate.Add(e);
        }



        public void Move()
        {

            CurrentLinea += 1;
        }
        public bool Evaluar()
        {
            //comenzar a evaluar todos los nodos 
            Debug.Log("Evaluar todos los nodos");

            while (CurrentLinea < nodos.Count)
            {

                Debug.Log($"Evaluar nodo {CurrentLinea}");
                //ir evaluando todos los nodos
                nodos[CurrentLinea].Evaluate(this);
            }

            if (error_evaluate.Count > 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}