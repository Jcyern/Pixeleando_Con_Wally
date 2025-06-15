using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
    public GameObject barra_texto;
    public GameObject error_panel;
    static string texto;

    public void MostrarError(string message)
    {
        if (barra_texto.GetComponent<TextMeshProUGUI>().text == "")
        {
            barra_texto.GetComponent<TextMeshProUGUI>().text += message;
        }
        else
        {
            barra_texto.GetComponent<TextMeshProUGUI>().text += "\n" + message;
        }


    }



    public static void ShowError(string message)
    {
        GameObject.Find("ErrorPanel").GetComponent<Show>().MostrarError(message);
    }

    public  static void On(bool on)
    {
        if (on)
            GameObject.Find("ErrorPanel").GetComponent<Show>().error_panel.SetActive(true);

        else
        GameObject.Find("ErrorPanel").GetComponent<Show>().error_panel.SetActive(false);
    }

    public static void Clean()
    {
        GameObject.Find("ErrorPanel").GetComponent<Show>().barra_texto.GetComponent<TextMeshProUGUI>().text = "";
        GameObject.Find("ErrorPanel").GetComponent<Show>().barra_texto.GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
    }



}
