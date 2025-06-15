using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utiles;

public class CameraAjuste : MonoBehaviour
{
    //script para ajustar Camara a las dimensiones del n x m que se pase a

    public Tile white;  //tile de color blanco 
    public Tilemap canvas;
    public float margin = 0.1f;

    public static  Vector3Int pos_inicial = new (-1, 1, 0);

    void Start()
    {
        Camera.main.orthographic = true;  // Asegura que la cámara sea ortográfica.
        AdjustCamera();                   // Ajusta la cámara.
    }

    public void AdjustCamera()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float requiredSize = Mathf.Max(Wally.canvas.x, Wally.canvas.y / aspectRatio) / 1.79f + margin;
        Camera.main.orthographicSize = requiredSize;

        //pos inicial
        // Centrar la cámara en el centro REAL del Tilemap:
        float centerX = (pos_inicial.x+1)+ (Wally.canvas.y/ 2.0f) ; //-2
        float centerY = (pos_inicial.y -1)- (Wally.canvas.y / 2.0f);// + 2

        Camera.main.transform.position = new Vector3(centerX, centerY, -10);


    }



}
