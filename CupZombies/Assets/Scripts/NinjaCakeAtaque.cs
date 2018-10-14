using System.Collections;
using UnityEngine;

public class NinjaCakeAtaque : MonoBehaviour
{
    [SerializeField] private GameObject ShuricookiePrefab;
    [SerializeField] private GameObject NinjaCake;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Atacar();
        //}
    }

    public void Atacar()
    {
        NinjaCake.GetComponent<Animator>().Play("NinjaAtaque");

        StartCoroutine(WaitAtaque(ShuricookiePrefab));
    }

    private IEnumerator WaitAtaque(GameObject ShurikenPrefab)
    {
        yield return new WaitForSeconds(0.2f);
        AudioManager.Instancia.PlaySfxLancarShuricookie();
        Instantiate(ShurikenPrefab, transform.position, Quaternion.identity);
    }
}
