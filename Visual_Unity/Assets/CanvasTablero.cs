
using System.Collections;
using System.Collections.Generic;
using PincelUnity_;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utiles;

public class CanvasTablero : MonoBehaviour
{
    public Tilemap canvas;

    private Queue<((int, int) pos, string color)> cola;

    void Start()
    {
        RellenarCanvas();
        // Pincel.ChangeColor("Blue");
    }

    public void RellenarCanvas()
    {
        Debug.Log("Rellenar Canvas");
        Pincel.ChangeColor("White");
        //para avanzar
        //por la X +1 
        //por la Y -1
        var MaxX = CameraAjuste.pos_inicial.x + Wally.canvas.x;
        var MaxY = CameraAjuste.pos_inicial.y - Wally.canvas.y;

        //recorrer el canvas y pintar las cosas del tile de relleno en este caso el White
        //volver a redimensionar el tablero 

        for (int x = 0; x <= MaxX; x++)
        {
            for (int y = 0; y >= MaxY; y--)
            {

                canvas.SetTile(new Vector3Int(x, y, 0), GameObject.Find("CanvasTablero").GetComponent<Colores>().GetColor(Pincel.Color));
            }
        }

        //rellenar los numeradores de los indices 
        // PincelUnity_.ChangeColor("Purple");
        // //poner los numero 
        // for (int x = 0; x < MaxX; x++)
        // {
        //     Methots.Draw((x, 1));
        // }
        // for (int y = 0; y > MaxY; y--) 
        // {
        //     Methots.Draw((-1, y));
        // }
    }



    public void Dibujar()
    {
        cola = Methots.cola;

        //pimero q entra primero q sale
            StartCoroutine(Print());
        
    }
    public IEnumerator Print()
    {

        while (cola.Count > 0)
        {


            var item = cola.Dequeue();

            if (item.color != Pincel.Color)
            {
                //camabiar el color del pincel 
                Pincel.ChangeColor(item.color);
                //y pintar 
                canvas.SetTile(new Vector3Int(item.pos.Item1, item.pos.Item2, 0), GameObject.Find("CanvasTablero").GetComponent<Colores>().GetColor(Pincel.Color));
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                canvas.SetTile(new Vector3Int(item.pos.Item1, item.pos.Item2, 0), GameObject.Find("CanvasTablero").GetComponent<Colores>().GetColor(Pincel.Color));
                yield return new WaitForSeconds(0.2f);
            }


        }
    }
}



