using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button PlayGameButton;

    void Start()
    {
        PlayGameButton.onClick.AddListener(PlayGame);
    }

    void PlayGame()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();

        SceneManager.LoadScene("MenuFases");
    }
}
