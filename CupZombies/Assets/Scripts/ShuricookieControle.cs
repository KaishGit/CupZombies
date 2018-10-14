using UnityEngine;

public class ShuricookieControle : MonoBehaviour
{
   [SerializeField] private int Velocidade;

    void Update()
    {
        transform.Translate(Velocidade * Time.deltaTime, 0, 0, Space.World);

        if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Red"))
            collision.GetComponent<Animator>().Play("ZumbiRedDeath");
        else
            collision.GetComponent<Animator>().Play("ZumbiDeath");

        AudioManager.Instancia.PlaySfxZumbiMorte();

        Destroy(collision.GetComponent<Collider2D>());
        Destroy(collision.gameObject, 1);
        Destroy(gameObject);
        GameManager.Instancia.DecrementarHordaUI();       
    }

}
