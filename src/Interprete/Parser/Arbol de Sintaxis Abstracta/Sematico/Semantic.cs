

using ArbolSintaxisAbstracta;

namespace Semantic
{


    public class Semantico
    {
        List<AstNode> nodos;
        static int i = 0;

        public Semantico(List<AstNode> nodos)
        {
            this.nodos = nodos;
        }


        //verificar la sintaxis de dichas estructuras 
        public  bool CheckSemantic()
        {
            System.Console.WriteLine("//////////////////////");
            System.Console.WriteLine("//////////////////////");
            System.Console.WriteLine("Chequear Semanticamente");
            System.Console.WriteLine("//////////////////////");
            System.Console.WriteLine("//////////////////////");

            while (i < nodos.Count)
            {
                nodos[i].CheckSemantic();
                i += 1;
            }

            if (AstNode.compilingError.Count > 0)
            {
                foreach (var item in AstNode.compilingError)
                {
                    item.ShowError();
                }
                return false;
            }
            return true;
        }

        public static int Current()
        {
            return i;
        }
    }


}



    
