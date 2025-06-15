
using ArbolSintaxisAbstracta;
using Unity.VisualScripting;
using UnityEngine;

namespace Utiles
{
    public class Wally
    {
        public static (int x , int y) canvas = (50, 50);
        public static (int Fila, int Columna) Pos = (int.MaxValue, int.MaxValue);

        public static int size_canvas;

        public static string[,] tablero;

        public static void SetCanvasSizeNode(int fila, int columna)
        {
            if (fila <= 256 && columna <= 256 && fila >= 0 && columna >= 0)
            {
                //asocia el taman0
                Debug.Log($"Se redimensiono el canvas {fila}, {columna}");
                canvas = (fila, columna);
                tablero = new string[canvas.x, canvas.y];
            }


            //sino lanzar error en pantalla 
            
        }

        public static (int, int) GetCanvasSizeNode()
        {
            return canvas;
        }

        public static void CrearTablero()
        {
            
            tablero = new string[canvas.Item1, canvas.Item2];

            for (int i = 0; i < tablero.GetLength(0); i ++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = "White";
                }
            }
        }



    }
}