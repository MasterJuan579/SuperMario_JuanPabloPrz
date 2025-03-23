using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void ChangeScene(string nombre)
    {
        SceneManager.LoadScene(nombre); // Nombre exacto de la escena
    }
    public void Salir(){
        Debug.Log("salir");
        Application.Quit();
    }

}
