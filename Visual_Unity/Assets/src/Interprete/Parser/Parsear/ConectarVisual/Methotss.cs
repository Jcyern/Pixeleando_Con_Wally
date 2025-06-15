
using UnityEngine;
using Unity.Collections;
using Unity.VisualScripting;
using Utiles;
using PincelUnity_;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;

namespace metodos
{
    public class Metodos
    {
        public static bool InRange(int valor, int min, int max)
        {
            return valor >= min && valor <= max;
        }

        public static List<(int, int)> direcciones = new List<(int, int)>() { (1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (1, -1), (-1, 1), (-1, -1) };


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
            (int, int) end = (Wally.Pos.Fila + (dirx * distance), Wally.Pos.Columna - (diry * distance));
            //metodo de uniy
            Methots.BresenhamLine(new Vector2Int(start.Item1, start.Item2), new Vector2Int(end.Item1, end.Item2));
        }

        public static void DrawCircle(int dirx, int diry, int radius)
        {
            var centerx = Wally.Pos.Fila + (dirx * radius);
            var centery = Wally.Pos.Columna - (diry * radius);
            int size = Pincel.Size;

            if (Methots.IsInRange((centerx, centery)))
            {
                //actualizar la pos del Wally 
                Wally.Pos.Fila = centerx;
                Wally.Pos.Columna = centery;
                Debug.Log($"Centro del circulo {centerx} {centery}");

                Methots.DrawBresenhamCircle(centerx, centery, radius, size);
            }
            //metood de unity 
        }

        public static void DrawRectangle(int dirx, int diry, int d, int largo, int ancho)
        {
            var centerx = Wally.Pos.Fila + (dirx * d);
            var centery = Wally.Pos.Columna - (diry * d);

            Methots.DrawRectangle(centerx, centery, ancho, largo);
        }


        public static void Fill()
        {
            //metodo  de unity 


            //hacer metodo recursivo para ir pintando 
            //mientras q halla gente en cola por buscar 

            //buscar direcciones disponibles en cada lado y analizar 

            //es valido si entra entre los limites del canvas y no ha sido recorrido 
            //matriz booleana para ir cogiendo lo ya buscado 
            //si es del mismo color , pintar del color por el que hay que cmabiar 
            Debug.Log(Wally.Pos.Fila + " " + Wally.Pos.Columna);
            Queue<(int, int)> cola = new Queue<(int, int)>();
            cola.Enqueue(Wally.Pos);
            Fill(cola, new bool[Wally.canvas.x, Wally.canvas.y], Wally.tablero[Wally.Pos.Fila, Wally.Pos.Columna], Wally.tablero, (0, 0));


        }
        private static  List<(int, int)> Direciones((int x, int y) pos)
        {
            List<(int, int)> vecinos = new();

            foreach (var item in direcciones)
            {
                if (Methots.IsInRange((pos.x + item.Item1, pos.y -item.Item2)))
                {
                    vecinos.Add((pos.x + item.Item1, pos.y - item.Item2));
                }
            }

            return vecinos;
        }

        private static void Fill(Queue<(int, int)> analice, bool[,] mask, string color, string[,] canvas, (int x, int y) actual)
        {
            if (analice.Count == 0)
            {
                //no hay elementos q analizar
                return;
            }

            //se saca un elemento de la cola y lo analiza 
            actual = analice.Dequeue();
            Debug.Log("actual " + actual.x + " " + actual.y);


            if (mask[actual.x, -actual.y] == false && analice.Contains(actual) == false && canvas[actual.x, -actual.y] == color)
            {
                //de ser asi 
                //anadelo a pintar 
                Methots.Draw(actual);
                mask[actual.x, -actual.y] = true;

                //find vecinos 
                foreach (var vecino in Direciones(actual))
                {
                    Debug.Log($"anadir {vecino.Item1}  {vecino.Item2}");
                    analice.Enqueue(vecino);
                }

                //seguir analizando por el elemento que sigue en la cola 
                Fill(analice, mask, color, canvas, actual);

            }
            else
            Fill(analice, mask, color, canvas, actual);
        }


    }
}