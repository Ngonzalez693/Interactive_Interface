using UnityEngine;

public class ExitOnEscape : MonoBehaviour
{
    void Update()
    {
        // Comprueba si se ha pulsado la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si estamos en el Editor de Unity, detenemos el modo Play
    #if UNITY_EDITOR
            Debug.Log("Saliendo...");
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
    }
}
