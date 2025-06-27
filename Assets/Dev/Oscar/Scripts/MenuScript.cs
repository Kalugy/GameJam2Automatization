using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject panelCreditos;

    public void MostrarCreditos()
    {
        panelCreditos.SetActive(true);
    }

    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
    }

    public void IniciarJuego()
    {
        // SceneManager.LoadScene("NombreDeTuEscenaDeJuego");
    }

    public void Como()
    {
    
    }
}