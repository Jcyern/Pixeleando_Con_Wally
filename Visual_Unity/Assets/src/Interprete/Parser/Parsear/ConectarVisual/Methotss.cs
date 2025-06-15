
using UnityEngine;
using Unity.Collections;
using Unity.VisualScripting;
using Utiles;
using PincelUnity_;

namespace metodos
{
    public class Metodos
    {
        public static bool InRange(int valor, int min, int max)
        {
            return valor >= min && valor <= max;
        }




        //metodos para conectar con los visualles de unity

        public static int GetActualX_U()
        {
            //metodo de unity  
            return 0;
        }

        public static int GetActualY_U()
        {
            //metodo de UNity 
            return 0;
        }


        public static int GetColorCount(string color, int x1, int y1, int x2, int y2)
        {
            int count = 0;
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    if (Wally.tablero[i, j] == color)
                        count += 1;

                }
            }

            return count;
        }


        public static void DrawLine(int dirx, int diry, int distance)
        {
            (int, int) start = Wally.Pos;
            (int, int)  end  = (Wally.Pos.Fila + (dirx * distance), Wally.Pos.Columna - (diry * distance));
            //metodo de uniy
            Methots.BresenhamLine(new Vector2Int(start.Item1,start.Item2), new Vector2Int(end.Item1,end.Item2));
        }

        public static void DrawCircle(int dirx, int diry, int radius)
        {
            var centerx = Wally.Pos.Fila + (dirx*radius);
            var centery = Wally.Pos.Columna - (diry*radius);
            int size = Pincel.Size ;

            if (Methots.IsInRange((centerx, centery)))
            {
                //actualizar la pos del Wally 
                Wally.Pos.Fila = centerx;
                Wally.Pos.Columna = centery;
                Debug.Log($"Centro del circulo {centerx} {centery}");

                Methots.DrawBresenhamCircle(centerx, centery, radius,size);
            }
            //metood de unity 
        }

        public static void DrawRectangle(int dirx, int diry, int d, int largo, int ancho)
        {
            var centerx = Wally.Pos.Fila + (dirx * d);
            var centery = Wally.Pos.Columna - (diry*d);

            Methots.DrawRectangle(centerx, centery, ancho , largo);
        }


        public static void Fill()
        {
            //metodo  de unity 
        }


        public static void Spawn((int x, int y) pos)
        {
            //metood de unity 
        }
    }
}