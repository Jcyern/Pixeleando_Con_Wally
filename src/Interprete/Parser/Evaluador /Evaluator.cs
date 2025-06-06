
using ArbolSintaxisAbstracta;
using Errores;

namespace Evalua
{
    public class Evaluator
    {
        List<AstNode> nodos = new List<AstNode>();
        List<Error> error_evaluate = new List<Error>();

        int CurrentLinea;

        const int Max_Call = 1000; //maximo de llamdas recursivas para q de el overflow

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
                System.Console.WriteLine("NO hay errores en el Evaluate ");
                return;
            }
        }

        public void AddError(Error e)
        {
            error_evaluate.Add(e);
        }


        public void Evaluar()
        {
            for (int i = CurrentLinea; i < nodos.Count; i++)
            {
                //ir evaluando todos los nodos
                nodos[i].Evaluate();
            }
        }
    }
}