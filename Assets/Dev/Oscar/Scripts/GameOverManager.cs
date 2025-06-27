using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject panelGameOver;

    public void MostrarGameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0; // Opcional: pausa el juego al perder
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); // Asegúrate de usar el nombre real
    }
}