using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterLine : MonoBehaviour
{
    public GameObject Numbers;
    public GameObject TextARea;
    private TMP_InputField inputField;      // Referencia al componente InputField // el de poner tu texto
    private TMP_InputField lines_numbers ; // el de los numeros 


    private int current_Line = 1;

    void Awake()
    {
        // Obtiene el componente InputField adjunto al GameObject
        inputField = TextARea.GetComponent<TMP_InputField>();
        lines_numbers = Numbers.GetComponent<TMP_InputField>();

        // Fuerza el modo multilínea (permite saltos con Enter)
        inputField.lineType = TMP_InputField.LineType.MultiLineNewline;
        inputField.textComponent.enableWordWrapping = false; // Desactiva el wrap automático // permite q siga horizontalmente sin hacer el cambio de linea automatico 
        UpdateLinesNumbers(CountLines(inputField.text));

        //para q el input field de los numeros no sea interactuable
        lines_numbers.interactable = false;

        //este evento se activa cada vez que hay un cualquiera cambio de texto dentro del inputfield 
        inputField.onValueChanged.AddListener(OnTextChange);

    }


    void OnTextChange(string text)
    {
        UpdateLinesNumbers(CountLines(text));
    }

    //metodo para calcular la cantidad de lineas del text area 
    int CountLines ( string text_area)
    {
        //sino hay hay texto devuelve la linea 1
        if( string.IsNullOrEmpty(text_area))return 1;
        
        //separa por lineas y devuelve la cantidad
        return text_area.Split('\n').Length;
    }

    //para actualizar la numeracion en el hemisferio en la parte izquierda
    void UpdateLinesNumbers(int lines)
    {
        StringBuilder sb = new ();

        for(int i =1;i<=lines ; i ++)
        {                              //donde unico no se pone salto de linea es antes del uno para no se vea desfazado 
            if(i>1)sb.Append("\n");   ///la idea q es vaya quedando asi 1\n2\n\3\n4
            sb.Append(i);
        }

        lines_numbers.text = sb.ToString();
    }






}