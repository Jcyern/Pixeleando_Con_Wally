using System;
using System.Collections;
using System.Collections.Generic;
using PincelUnity_;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Tilemaps;
using Utiles;


public class Methots : MonoBehaviour
{

    //Dibujar una linea recta desde la pos en la q se encuentra hasta la pos indicada 

    //dado una direccion me de las adyacentes 

    static Dictionary<(int, int), List<(int, int)>> adyacentes = new Dictionary<(int, int), List<(int, int)>>();
    static Dictionary<(int, int), List<(int, int)>> circle = new();
    static Dictionary<(int, int), List<(int, int)>> drawc = new();

    public static Queue<((int, int) pos, string color)> cola = new();


    public static void ReinicarMethots()
    {
        cola.Clear();
    }

    public static bool IsInRange((int x, int y) position)
    {
        if (position.x >= 0 && position.y <= 0 && position.x < Wally.canvas.x && position.y > -Wally.canvas.y)
            return true;
        else
            return false;
    }

    public static void Draw((int, int) pos, string color = null)
    {
        if (IsInRange(pos))
        {
            if (color == null)
            {
                //guardar en una pila la instruccion a pintar 
                //pos con color // para luego pintarlo de una forma bonito y peculiar 
                //canvas.SetTile(new Vector3Int(pos.Item1, pos.Item2, 0), GameObject.Find("CanvasTablero").GetComponent<Colores>().GetColor(Pincel.Color));
                cola.Enqueue(((pos.Item1, pos.Item2), Pincel.Color));
                //agregar el color a donde el  verifica con sus metodos 
                Wally.tablero[pos.Item1, -pos.Item2] = Pincel.Color;
            }
            else
            {
                //canvas.SetTile(new Vector3Int(pos.Item1, pos.Item2, 0), GameObject.Find("CanvasTablero").GetComponent<Colores>().GetColor(color));
                cola.Enqueue(((pos.Item1, pos.Item2), color));

                Wally.tablero[pos.Item1, -pos.Item2] = color;
            }
            Debug.Log($"pintado {pos.Item1} , {pos.Item2}");
        }
    }




    private static Vector3Int NormalizeDirection(Vector3Int delta)
    {
        return new Vector3Int(Mathf.Clamp(delta.x, -1, 1), Mathf.Clamp(delta.y, -1, 1), 0);
    }


    public static void BresenhamLine(Vector2Int start, Vector2Int end)
    {

        //tomaar el grozor del pincel 
        int halfThickness = Pincel.Size / 2; // Radio del grosor

        // --- PASO 2: Calcular dirección principal y perpendicular ---
        var delta = end - start;
        Vector3Int primaryDir = NormalizeDirection(new Vector3Int(delta.x, delta.y, 0)); // Normaliza a (-1, 0, 1)
        Vector3Int perpendicularDir = new Vector3Int(-primaryDir.y, primaryDir.x, 0); // Rotación 90°


        int x0 = start.x, y0 = start.y;
        int x1 = end.x, y1 = end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        // Condición de salida clara
        while (x0 != x1 || y0 != y1) // Mientras no lleguemos al final
        {
            if (IsInRange((x0, y0)))
                Wally.Pos = (x0, y0);

            //pintar grozor alrededor del punto 
            for (int t = -halfThickness; t <= halfThickness; t++)
            {
                Draw((x0 + perpendicularDir.x * t, y0 + perpendicularDir.y * t));
            }

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }

    }



    public static void DrawBresenhamCircle(int centerX, int centerY, int radius, int grozor)
    {

        int innerRadius = radius - (grozor / 2);
        int outerRadius = radius + (grozor / 2);

        // Dibujar el anillo (área entre innerRadius y outerRadius)
        for (int r = innerRadius; r <= outerRadius; r++)
        {
            DrawBresenhamCircle(centerX, centerY, r);
        }

    }


    private static void DrawBresenhamCircle(int centerX, int centerY, int radius)
    {
        int x = 0, y = radius;
        int d = 3 - 2 * radius; // Fórmula inicial optimizada


        while (x <= y)
        {
            // Pintar los 8 puntos simétricos
            Draw((centerX + x, centerY + y)); // Octante 1
            Draw((centerX - x, centerY + y)); // Octante 4
            Draw((centerX + x, centerY - y)); // Octante 8
            Draw((centerX - x, centerY - y)); // Octante 5
            Draw((centerX + y, centerY + x)); // Octante 2
            Draw((centerX - y, centerY + x)); // Octante 3
            Draw((centerX + y, centerY - x)); // Octante 7
            Draw((centerX - y, centerY - x)); // Octante 6

            // Actualizar parámetro de decisión
            if (d < 0)
            {
                d += 4 * x + 6; // Movimiento horizontal
            }
            else
            {
                d += 4 * (x - y) + 10; // Movimiento diagonal
                y--; // Ajustar y
            }
            x++; // Siempre incrementamos x

        }
    }


    public static void DrawRectangle(int centerx, int centery, int largo, int ancho)
    {
        int halfThickness = Pincel.Size / 2;

        // Calcular los límites del rectángulo
        int halfWidth = largo / 2;
        int halfHeight = ancho / 2;

        int minX = centerx - halfWidth;
        int maxX = centerx + halfWidth;
        int minY = centery - halfHeight;
        int maxY = centery + halfHeight;

        // Ajustar para dimensiones pares
        if (largo % 2 == 0) maxX--;
        if (ancho % 2 == 0) maxY--;

        // Dibujar el rectángulo
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                bool isBorder = (x <= minX + halfThickness || x >= maxX - halfThickness || y <= minY + halfThickness || y >= maxY - halfThickness);


                if (isBorder)
                {
                    Draw((x, y));
                }
            }


        }
    }
}
