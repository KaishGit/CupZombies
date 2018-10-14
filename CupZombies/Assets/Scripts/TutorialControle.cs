using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialControle : MonoBehaviour
{
    [SerializeField] private Text TutorialTexto;
    [SerializeField] private GameObject IngredientePrefab1;
    [SerializeField] private GameObject IngredientePrefab2;
    [SerializeField] private GameObject IngredientePrefab3;
    [SerializeField] private GameObject IngredientePrefab4;
    [SerializeField] private GameObject PosicaoIngrediente;
    [SerializeField] private GameObject BotaoPular;
    [SerializeField] private TextMeshProUGUI BotaoPularText;
    [SerializeField] private GameObject PosicaoBotaoPular;

    private GameObject _ingrediente1;
    private GameObject _ingrediente2;
    private GameObject _ingrediente3;
    private GameObject _ingrediente4;
    private int _etapa = 0;

    void Start()
    {
        _ingrediente1 = Instantiate(IngredientePrefab1, PosicaoIngrediente.transform.position, Quaternion.identity);
        Destroy(_ingrediente1.GetComponent<IngredienteControle>());
        _etapa = 1;
    }

    void Update()
    {
        if(_etapa == 1)
        {
            if(_ingrediente1 == null)
            {
                _ingrediente2 = Instantiate(IngredientePrefab2, PosicaoIngrediente.transform.position, Quaternion.identity);
                Destroy(_ingrediente2.GetComponent<IngredienteControle>());
                _etapa = 2;

                TutorialTexto.text = "Press again to collect the other ingredient and help NinjaCake to attack";
            }
        }

        if (_etapa == 2)
        {
            if (_ingrediente2 == null)
            {
                _ingrediente3 = Instantiate(IngredientePrefab3, PosicaoIngrediente.transform.position, Quaternion.identity);
                Destroy(_ingrediente3.GetComponent<IngredienteControle>());
                _etapa = 3;

                TutorialTexto.text = "Collect the power multiplier and the correct ingredients. Multipliers increase the number of attacks";
            }
        }

        if (_etapa == 3)
        {
            if (_ingrediente3 == null)
            {
                _ingrediente1 = Instantiate(IngredientePrefab1, PosicaoIngrediente.transform.position, Quaternion.identity);
                Destroy(_ingrediente1.GetComponent<IngredienteControle>());
                _etapa = 4;
            }
        }

        if (_etapa == 4)
        {
            if (_ingrediente1 == null)
            {
                _ingrediente2 = Instantiate(IngredientePrefab2, PosicaoIngrediente.transform.position, Quaternion.identity);
                Destroy(_ingrediente2.GetComponent<IngredienteControle>());
                _etapa = 5;
            }
        }

        if (_etapa == 5)
        {
            if (_ingrediente2 == null)
            {
                _ingrediente4 = Instantiate(IngredientePrefab4, PosicaoIngrediente.transform.position, Quaternion.identity);
                Destroy(_ingrediente4.GetComponent<IngredienteControle>());
                _etapa = 6;

                TutorialTexto.text = "Collect the wrong ingredient. Collecting wrong ingredients changes the recipe";
            }
        }

        if (_etapa == 6)
        {
            if (_ingrediente4 == null)
            {
                TutorialTexto.text = "Finish the horde to win";

                BotaoPular.transform.position = PosicaoBotaoPular.transform.position;
                Destroy(PosicaoBotaoPular);
                BotaoPularText.SetText("Finalize");

                _etapa = 7;
            }
        }
    }

    public void FinalizarTutorial()
    {
        AudioManager.Instancia.PlaySfxBotaoUi();

        PlayerPrefs.SetInt("TutorialCompleto", 1);
        SceneManager.LoadScene("Fase01-01");
    }
}