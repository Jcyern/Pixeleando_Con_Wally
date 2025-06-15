using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Error : MonoBehaviour
{
    public GameObject panel_error ;
    bool accionEnCurso= false;
    float delayAccion = 0.2f ;
    

    IEnumerator EjecutarAccionConDelay()
    {
        accionEnCurso = true;

        // Espera el tiempo definido,en este caso 0,2 frames
        yield return new WaitForSeconds(delayAccion);

        if (panel_error != null)
        {
            panel_error.SetActive(!panel_error.activeSelf);
            Debug.Log($"Objeto {panel_error.name} alternado después de {delayAccion}s");
        }

        accionEnCurso = false;
    }


    // Update is called once per frame
    void Update()
    {
        //verifica si se presiona ctrol + j para activar el panel de errores

        if((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&  Input.GetKey(KeyCode.J))
        {
            if (!accionEnCurso)
            {
                StartCoroutine(EjecutarAccionConDelay());
            }
        }
    }
}
