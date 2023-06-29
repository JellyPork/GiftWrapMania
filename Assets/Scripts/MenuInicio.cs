using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Comenzar()
    {
        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        SceneManager.LoadScene(3);
    }

    public void SalirCreditos()
    {
        SceneManager.LoadScene(0);
    }
}