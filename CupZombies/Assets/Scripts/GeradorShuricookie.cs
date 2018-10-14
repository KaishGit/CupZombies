using System.Collections.Generic;
using UnityEngine;

public class GeradorShuricookie : MonoBehaviour
{
    [SerializeField] private List<GameObject> ListaShuriken;


    void Start()
    {
        InvokeRepeating("Gerar", 2, Random.Range(2,5));
    }

    private void Gerar()
    {
        var shuriken = Instantiate(ListaShuriken[Random.Range(0, ListaShuriken.Count)], transform.position, Quaternion.identity);
        var shurikenRigidBody = shuriken.GetComponent<Rigidbody2D>();
        shurikenRigidBody.AddForce(new Vector2(Random.Range(5, 11), Random.Range(5, 11)), ForceMode2D.Impulse);

        Destroy(shuriken, 5);
    }
}