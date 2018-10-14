using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;
    public int HordaZumbiQuantidade;
    public int FatorDeDificuldade;

    [SerializeField] private GameObject Correto;
    [SerializeField] private GameObject Errado;
    [SerializeField] private GameObject NinjaCakeMao;
    [SerializeField] private GameObject PosicaoPoder;
    [SerializeField] private GameObject Poder2x;
    [SerializeField] private GameObject Poder3x;
    [SerializeField] private TMP_Text QuantidadeZumbisText;
    [SerializeField] private List<GameObject> ListaPosicaoReceita;
    [SerializeField] private List<GameObject> ListaIngredientes;

    private float orthoSize = 5;
    private float aspect = 1.75f;
    private int quantidadeZumbis;
    private bool Poder2xAtivado;
    private bool Poder3xAtivado;
    private List<GameObject> ingredientesReceitaUI = new List<GameObject>();
    private List<GameObject> elementosReceitaUi = new List<GameObject>();   
    private List<string> ingredientesCorretos = new List<string>();

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }

        Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }

    private void Start()
    {
        quantidadeZumbis = HordaZumbiQuantidade;
        QuantidadeZumbisText.text = "Horde: " + quantidadeZumbis;

        GerarReceita();
    }

    private void GerarReceita()
    {
        LimparReceitaAnterior();
     
        List<GameObject> ListaIngredientesReceita = new List<GameObject>(ListaIngredientes);

        for (int i = 0; i < this.ListaPosicaoReceita.Count; i++)
        {
            var IngredienteRandom = ListaIngredientesReceita[Random.Range(0, ListaIngredientesReceita.Count)];

            ListaIngredientesReceita.Remove(IngredienteRandom);

            var ingredienteUI = Instantiate(IngredienteRandom, this.ListaPosicaoReceita[i].transform.position, Quaternion.identity);

            Destroy(ingredienteUI.GetComponent<IngredienteControle>());
            ingredienteUI.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            ingredientesReceitaUI.Add(ingredienteUI);
        }   
    }

    public void DecrementarHorda()
    {
        HordaZumbiQuantidade--;
    }

    public void DecrementarHordaUI()
    {
        quantidadeZumbis--;

        if (quantidadeZumbis == 0)
        {
            AudioManager.Instancia.PlaySfxVitoriaFase();
            LiberarProximaFase();
            StartCoroutine(ExibirPainelWin());
        }

        QuantidadeZumbisText.text = "Horde: " + quantidadeZumbis;
    }
  
    public void VerificarIngrediente(GameObject IngredienteSelecionado)
    {
        if(IngredienteSelecionado.name == "Bloco2x(Clone)")
        {
            var Poder2xUi = Instantiate(Poder2x, PosicaoPoder.transform.position, Quaternion.identity);
            elementosReceitaUi.Add(Poder2xUi);

            var corrretoSelecao = Instantiate(Correto, IngredienteSelecionado.transform.position, Quaternion.identity);
            Destroy(corrretoSelecao, 0.25f);

            Poder2xAtivado = true;

            AudioManager.Instancia.PlaySfxPowerUp();

            return;
        }
        else if (IngredienteSelecionado.name == "Bloco3x(Clone)")
        {
            var Poder3xUi = Instantiate(Poder3x, PosicaoPoder.transform.position, Quaternion.identity);
            elementosReceitaUi.Add(Poder3xUi);

            var corrretoSelecao = Instantiate(Correto, IngredienteSelecionado.transform.position, Quaternion.identity);
            Destroy(corrretoSelecao, 0.25f);

            Poder3xAtivado = true;

            AudioManager.Instancia.PlaySfxPowerUp();

            return;
        }

        var ingredienteCorreto = false;

        foreach (var ingredienteReceita in ingredientesReceitaUI)
        {
            if (IngredienteSelecionado.name == ingredienteReceita.name &&
               !ingredientesCorretos.Contains(IngredienteSelecionado.name))
            {
                ingredienteCorreto = true;

                ingredientesCorretos.Add(ingredienteReceita.name);

                var corretoReceita = Instantiate(Correto, ingredienteReceita.transform.position, Quaternion.identity);
                elementosReceitaUi.Add(corretoReceita);

                var corrretoSelecao = Instantiate(Correto, IngredienteSelecionado.transform.position, Quaternion.identity);
                Destroy(corrretoSelecao, 0.25f);
            }
        }

        if (ingredienteCorreto == true)
        {
            AudioManager.Instancia.PlaySfxAcertarIngrediente();

            if (ingredientesCorretos.Count == ListaPosicaoReceita.Count)
            {
                NinjaCakeMao.GetComponent<NinjaCakeAtaque>().Atacar();

                if (Poder2xAtivado == true)
                {
                    StartCoroutine(LancarAtaque2x());
                }
                else if (Poder3xAtivado == true)
                {
                    StartCoroutine(LancarAtaque2x());
                    StartCoroutine(LancarAtaque3x());
                }

                GerarReceita();
            }
        }
        else
        {
            AudioManager.Instancia.PlaySfxErrarIngrediente();

            var erradoSelecao = Instantiate(Errado, IngredienteSelecionado.transform.position, Quaternion.identity);
            Destroy(erradoSelecao, 0.25f);

            GerarReceita();
        }
    }

    public List<GameObject> PegarListaIngredientes()
    {
        return ListaIngredientes;
    }

    public List<GameObject> PegarListaIngredientesReceita()
    {
        List<GameObject> listaIngredientesReceita = new List<GameObject>();

        foreach(var item in ingredientesReceitaUI)
        {
            listaIngredientesReceita.Add(ListaIngredientes.FirstOrDefault(i => item.name.Contains(i.name)));
        }

        return listaIngredientesReceita;
    }

    private void LimparReceitaAnterior()
    {
        DestruirGameObjectLista(ingredientesReceitaUI);
      
        DestruirGameObjectLista(elementosReceitaUi);

        ingredientesCorretos.Clear();

        Poder2xAtivado = false;
        Poder3xAtivado = false;
    }

    private void DestruirGameObjectLista(List<GameObject> Lista)
    {
        foreach (var listaItem in Lista)
        {
            Destroy(listaItem);
        }
        Lista.Clear();
    }

    private void LiberarProximaFase()
    {
        if (SceneManager.GetActiveScene().name == "Fase01-01")
        {
            PlayerPrefs.SetInt("Fase01_02_Liberada", 1);
        }
        else if (SceneManager.GetActiveScene().name == "Fase01-02")
        {
            PlayerPrefs.SetInt("Fase01_03_Liberada", 1);
        }
    }

    private IEnumerator LancarAtaque2x()
    {
        yield return new WaitForSeconds(0.4f);
        NinjaCakeMao.GetComponent<NinjaCakeAtaque>().Atacar();
    }

    private IEnumerator LancarAtaque3x()
    {
        yield return new WaitForSeconds(0.8f);
        NinjaCakeMao.GetComponent<NinjaCakeAtaque>().Atacar();
    }

    private IEnumerator ExibirPainelWin()
    {
        yield return new WaitForSeconds(5);
        UIManager.Instancia.ExibirPainelWin();
    }
}
