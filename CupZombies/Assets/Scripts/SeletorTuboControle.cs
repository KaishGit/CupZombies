using UnityEngine;
using UnityEngine.UI;

public class SeletorTuboControle : MonoBehaviour
{
    [SerializeField] private GameObject PosicaoSeletorAtivo;
    [SerializeField] private GameObject SeletorAtivo;

    private GameObject IngredienteSelecionado;
    private bool IngredienteDentro;
    private float AreaSelecaoMax;
    private float AreaSelecaoMin;

    private void Start()
    {
        AreaSelecaoMax = GetComponent<Collider2D>().bounds.max.y;
        AreaSelecaoMin = GetComponent<Collider2D>().bounds.min.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            VerificarIngrediente();
        }
    }

    public void VerificarIngrediente()
    {
        AudioManager.Instancia.PlaySfxApertarEspaco();

        var seletorAtual = Instantiate(SeletorAtivo, PosicaoSeletorAtivo.transform.position, Quaternion.identity);
        Destroy(seletorAtual, 0.27f);

        try
        {
            if (IngredienteDentro == true)
            {
                var IngredienteCollider = IngredienteSelecionado.GetComponent<Collider2D>().bounds;
                var IngredienteMax = IngredienteCollider.max.y;
                var IngredienteMin = IngredienteCollider.min.y;

                //print(IngredienteMax + " < " + AreaSelecaoMax + " : " + (IngredienteMax < AreaSelecaoMax));
                //print(IngredienteMin + " > " + AreaSelecaoMin + " : " + (IngredienteMin > AreaSelecaoMin));

                if (IngredienteMax < AreaSelecaoMax && IngredienteMin > AreaSelecaoMin)
                {
                    GameManager.Instancia.VerificarIngrediente(IngredienteSelecionado);
                    Destroy(IngredienteSelecionado.GetComponent<IngredienteControle>());
                    Destroy(IngredienteSelecionado, 0.25f);
                }
            }
        }
        catch (System.Exception)
        {
            //Ignora o clique caso o botão seja apertado rápido demais e envie o mesmo ingrediente para verificação.
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IngredienteDentro = true;
        IngredienteSelecionado = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IngredienteDentro = false;
    }

}
