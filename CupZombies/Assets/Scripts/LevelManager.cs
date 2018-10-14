using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button Fase01_01Button;
    [SerializeField] private Button Fase01_02Button;
    [SerializeField] private Button Fase01_03Button;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Fase01_02_Liberada") == 1)
        {
            Fase01_02Button.interactable = true;
        }

        if (PlayerPrefs.GetInt("Fase01_03_Liberada") == 1)
        {
            Fase01_03Button.interactable = true;
        }
    }

    public void CarregarFase01_01()
    {
        //PlayerPrefs.SetInt("TutorialCompleto", 0);
        AudioManager.Instancia.PlaySfxBotaoUi();

        if (PlayerPrefs.GetInt("TutorialCompleto") == 0)
        {

            SceneManager.LoadScene("FaseTutorial");
        }
        else
        {
            SceneManager.LoadScene("Fase01-01");
        }
    }

    public void CarregarFase01_02()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();

        SceneManager.LoadScene("Fase01-02");
    }

    public void CarregarFase01_03()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();

        SceneManager.LoadScene("Fase01-03");
    }

    public void CarregarFaseStart()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();

        SceneManager.LoadScene("Start");
    }
}