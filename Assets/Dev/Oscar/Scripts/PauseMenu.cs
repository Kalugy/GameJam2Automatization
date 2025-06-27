using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject panelPausa;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausa();
        }
    }

    public void TogglePausa()
    {
        juegoPausado = !juegoPausado;
        panelPausa.SetActive(juegoPausado);
        Time.timeScale = juegoPausado ? 0 : 1; // Pausa o reanuda el juego
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal"); // Cambia por el nombre de tu escena de menú
    }
}
