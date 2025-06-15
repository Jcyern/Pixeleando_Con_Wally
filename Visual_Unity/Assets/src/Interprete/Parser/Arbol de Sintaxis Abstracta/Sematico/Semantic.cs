

using System.Collections.Generic;
using ArbolSintaxisAbstracta;
using UnityEngine;

namespace Semantic
{


    public class Semantico
    {
        List<AstNode> nodos;
        static int i = 0;

        public Semantico(List<AstNode> nodos)
        {
            this.nodos = nodos;
            //reiniciar el contador para volver a analizar sintacticamente 
            i = 0;
        }


        //verificar la sintaxis de dichas estructuras 
        public  bool CheckSemantic()
        {

            Debug.Log("//////////////////////");
            Debug.Log("//////////////////////");
            Debug.Log("Chequear Semanticamente");
            Debug.Log("//////////////////////");
            Debug.Log("//////////////////////");

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



    
