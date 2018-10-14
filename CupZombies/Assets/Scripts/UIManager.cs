using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instancia;

    [SerializeField] private GameObject PainelPause;
    [SerializeField] private GameObject PainelWin;
    [SerializeField] private GameObject PainelGameOver;
    [SerializeField] private AudioListener AudioListenerGame;

    private void Awake()
    {
        if(Instancia == null)
        {
            Instancia = this;
        }
    }

    public void ExibirPainelWin()
    {
        PainelWin.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExibirPainelGameOver()
    {
        AudioManager.Instancia.PlaySfxDerrotaFase();
        AudioManager.Instancia.PausarMusica();

        PainelGameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void Pausar()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();
        AudioManager.Instancia.PausarMusica();

        PainelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Despausar()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();
        AudioManager.Instancia.DespausarMusica();

        PainelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReiniciarFase()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuFases");
    }

    public void ProximaFase()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "Fase01-01")
        {
            SceneManager.LoadScene("Fase01-02");
        }
        else if (SceneManager.GetActiveScene().name == "Fase01-02")
        {
            SceneManager.LoadScene("Fase01-03");
        }
        else if (SceneManager.GetActiveScene().name == "Fase01-03")
        {
            SceneManager.LoadScene("MenuFases");
        }
    }
}