using System;
using UnityEngine;

public class GeradorZumbiControle : MonoBehaviour
{
    [SerializeField] private GameObject ZumbiPrefab;
    [SerializeField] private float VelocidadeZumbi;
    [SerializeField] private int TempoEsperaPrimeiraGeracao;
    [SerializeField] private int TempoMaximo;
    [SerializeField] private int TempoMinimo;

    private float TempoProximaGeracao;

    void Start()
    {
        TempoProximaGeracao = Time.timeSinceLevelLoad + TempoEsperaPrimeiraGeracao;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instancia.HordaZumbiQuantidade > 0)
        {
            if (Time.timeSinceLevelLoad > TempoProximaGeracao && Time.timeScale == 1)
            {
                GameObject zumbi =  Instantiate(ZumbiPrefab, transform.position, Quaternion.identity);
                zumbi.GetComponent<ZumbiControle>().Velocidade = VelocidadeZumbi;
                GameManager.Instancia.DecrementarHorda();
                TempoProximaGeracao = Time.timeSinceLevelLoad + UnityEngine.Random.Range(TempoMinimo, TempoMaximo);
            }
        }
    }
}
