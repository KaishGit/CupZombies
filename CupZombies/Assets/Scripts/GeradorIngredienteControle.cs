using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeradorIngredienteControle : MonoBehaviour
{
    [SerializeField] private double PeriodoEntreIngredientes;
    [SerializeField] private int QuantidadeIngredientesGerados;
    [SerializeField] private int IntervaloEntrePoder2x;
    [SerializeField] private int IntervaloEntrePoder3x;
    [SerializeField] private GameObject Poder2x;
    [SerializeField] private GameObject Poder3x;

    private float TempoProximoIngrediente;
    private List<GameObject> ListaIngredientes = new List<GameObject>();

    void Start()
    {
        TempoProximoIngrediente = Time.timeSinceLevelLoad + 2;
    }

    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > TempoProximoIngrediente && Time.timeScale == 1)
        {
            GerarIngredientes();
            TempoProximoIngrediente = Time.timeSinceLevelLoad + Convert.ToSingle(PeriodoEntreIngredientes);
        }
    }

    private void GerarIngredientes()
    {
        if (IntervaloEntrePoder2x != 0 &&
           QuantidadeIngredientesGerados != 0 && 
           QuantidadeIngredientesGerados % IntervaloEntrePoder2x == 0)
        {
            Instantiate(Poder2x, transform.position, Quaternion.identity);
        }
        else if (IntervaloEntrePoder3x != 0 &&
           QuantidadeIngredientesGerados != 0 &&
           QuantidadeIngredientesGerados % IntervaloEntrePoder3x == 0)
        {
            Instantiate(Poder3x, transform.position, Quaternion.identity);
        }
        else
        {
            if (ListaIngredientes.Count == 0)
            {
                GerarNovaLista();
            }

            var IngredienteRandom = ListaIngredientes[UnityEngine.Random.Range(0, ListaIngredientes.Count)];
            Instantiate(IngredienteRandom, transform.position, Quaternion.identity);
            ListaIngredientes.Remove(IngredienteRandom);
        }

        QuantidadeIngredientesGerados++;
    }

    private void GerarNovaLista()
    {
        ListaIngredientes = new List<GameObject>(GameManager.Instancia.PegarListaIngredientes());

        int fator = GameManager.Instancia.FatorDeDificuldade;
        List<GameObject> ListaIngredientesNovos = new List<GameObject>();

        for (var i = 0; i < fator; i++)
        {
            ListaIngredientesNovos.Add(ListaIngredientes[UnityEngine.Random.Range(0, ListaIngredientes.Count)]);
        }

        var ListaIngredientesReceita = GameManager.Instancia.PegarListaIngredientesReceita();

        foreach (var itemReceita in ListaIngredientesReceita)
        {
            if (ListaIngredientesNovos.FirstOrDefault(i => itemReceita.name == i.name) == null)
            {
                ListaIngredientesNovos.Insert(UnityEngine.Random.Range(0, ListaIngredientesNovos.Count), itemReceita);
            }
        }

        ListaIngredientes = ListaIngredientesNovos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
