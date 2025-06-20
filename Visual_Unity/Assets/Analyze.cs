using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alcance;
using ArbolSintaxisAbstracta;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Utiles;

public class Analyze : MonoBehaviour
{
    public GameObject BotonSave;
    public GameObject panel_errores;
    public GameObject panel_save;
    public GameObject inputfield;
    public string[] lineas;

    public IEnumerator Corutine()
    {
        yield return new WaitForEndOfFrame();
    }
    public void Analizar()
    {
        Show.On(false);

        //limpiar los errores 
        AstNode.compilingError.Clear();
        //limpiando los diccionarios de la expresiones de Scope 
        Scope.ReiniciarScope();
        Methots.ReinicarMethots();


        StartCoroutine(Corutine());

        //pedirle al usuario que pase un numero 
        Wally.SetCanvasSizeNode(50, 50);
        Wally.CrearTablero();





        var text = inputfield.GetComponent<TMP_InputField>().text;





        if (text == "")
        {
            Show.Clean();
            Debug.Log(" es el vacio");//imprimir error q no se puede anallizar un codigo vacio
            Show.ShowError("We can't analice a null lines of codes");
            GameObject.Find("ErrorPanel").GetComponent<Show>().error_panel.SetActive(true);
        }
        else
        {
            Show.Clean();
            Debug.Log("analizar");
            lineas = text.Split('\n'); //separar por salto de linea 

            for (int i = 0; i < lineas.Length; i++)
            {
                Debug.Log($"Line: {i + 1}- {lineas[i]}");
            }

            Brain interpretar = new Brain();
            var result = interpretar.InterpretarLenguaje(lineas);

            if (result == true)
            {
                Debug.Log("Cambiar de escena ");
                Escenas.ChangeSceneToDraw();
            }
            return;


        }
    }

    public void Guardar()
    {
        panel_save.SetActive(true);
        BotonSave.SetActive(false);
    }
    public void Cancel()
    {
        panel_save.SetActive(false);
        BotonSave.SetActive(true);
    }
    public void Crear()
    {
        //que salga un cartel para poner el nombre 
        var input_name = panel_save.GetComponent<TMP_InputField>();

        if (string.IsNullOrWhiteSpace(input_name.text))
        {
            //alertar al usuario de  que no se puede poner eso vacio

            Show.ShowError("We don't admit a null text as name ");
            panel_errores.SetActive(true);
        }
        else
        {
            //// Ruta dentro del proyecto (Assets/GeneratedFiles/newtxt.txt)
            string folderPath = Path.Combine(Application.dataPath, "SavesCodes");
            string filePath = Path.Combine(folderPath, input_name.text + ".txt");

            // Crear la carpeta si no existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(filePath, inputfield.GetComponent<TMP_InputField>().text);
            Debug.Log($"se guardo el codigo en la ruta {filePath}");
            panel_save.SetActive(false);
        }
    }




    //cargar archivos existentes 

    //crear las barras de

}
