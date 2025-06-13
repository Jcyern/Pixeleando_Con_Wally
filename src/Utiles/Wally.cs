
using ArbolSintaxisAbstracta;

namespace Utiles
{
    public class Wally
    {
        public static (int, int) canvas = (50, 50);
        public static (int Fila, int Columna) Pos = (int.MaxValue, int.MaxValue);

        public static int size_canvas;



        public static void  GetCanvasSizeNode(int fila, int columna)
        {
            System.Console.WriteLine($"Se redimensiono el canvas {fila }, {columna}");
            canvas = (fila, columna);
        }
    }
}