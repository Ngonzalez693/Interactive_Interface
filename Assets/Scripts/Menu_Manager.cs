using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public void StartInterface()
    {
        SceneManager.LoadScene("Room_View");
    }

    public void Exit()
   {
      Debug.Log("Saliendo del juego...");
      Application.Quit();
   }
}
