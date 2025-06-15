using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class CreateCodes : MonoBehaviour
{
    public void CreateANdOpen()
    {
        // Ruta dentro del proyecto (Assets/GeneratedFiles/newtxt.txt)
        string folderPath = Path.Combine(Application.dataPath, "CodesFiles");
        string filePath = Path.Combine(folderPath, "newtxt.txt");

        // Crear la carpeta si no existe
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Crear/sobrescribir el archivo
        File.WriteAllText(filePath, "Contenido del archivo creado desde Unity!\n");

        // Actualizar la ventana del Editor para ver el archivo
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif

        // Abrir el archivo con el programa predeterminado (Ubuntu)
        System.Diagnostics.Process.Start("xdg-open", filePath);
    }
}
