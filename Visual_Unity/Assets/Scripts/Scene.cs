

using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenas : MonoBehaviour

{

    public static void ChangeSceneToDraw()
    {
        SceneManager.LoadScene("Tilemap");
    }


    public void ChangetoCode()
    {
        SceneManager.LoadScene("Editor_de_texto");
    }
}