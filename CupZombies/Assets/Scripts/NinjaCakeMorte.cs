using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaCakeMorte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instancia.PlaySfxNinjaCakeMorte();
        GetComponent<Animator>().Play("NinjaMorte");

        StartCoroutine(ExibirPainelWin());
    }

    private IEnumerator ExibirPainelWin()
    {
        yield return new WaitForSeconds(1);
        UIManager.Instancia.ExibirPainelGameOver();
    }
}
