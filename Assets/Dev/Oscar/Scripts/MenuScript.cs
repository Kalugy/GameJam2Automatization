using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject panelCreditos;
    public GameObject panelInstruc;

    public void MostrarCreditos()
    {
        panelCreditos.SetActive(true);
    }

    public void MostrasInstru()
    {
        panelInstruc.SetActive(true);
    }

    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
    }

    public void CerrarInstru()
    {
        panelInstruc.SetActive(false);
    }

    public void IniciarJuego()
    {
        // SceneManager.LoadScene("NombreDeTuEscenaDeJuego");
    }

    public void Como()
    {
    
    }
}