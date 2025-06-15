using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Colores : MonoBehaviour
{
    //colores disponibles en unity
    public Tile white;
    public Tile blue;
    public Tile black;
    public Tile yellow;
    public Tile red;
    public Tile purple;
    public Tile orange;
    public Tile green;

    public Dictionary<string, Tile> colors = new Dictionary<string, Tile>();

    void Awake()
    {
        RegisterColors();
    }
    public void RegisterColors()
    {
        colors["White"] = white;
        colors["Blue"] = blue;
        colors["Black"] = black;
        colors["Yellow"] = yellow;
        colors["Red"] = red;
        colors["Purple"] = purple;
        colors["Orange"] = orange;
        colors["Green"] = green;

        Debug.Log("Colores registrados correctamente");

    }


    public Tile GetColor(string c)
    {
        return colors[c];
    }
    

}
