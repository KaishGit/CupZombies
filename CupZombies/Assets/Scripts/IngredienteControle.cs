using UnityEngine;

public class IngredienteControle : MonoBehaviour
{
    [SerializeField] private float Velocidade;

    void Update()
    {
        transform.Translate(0, -Velocidade * Time.deltaTime, 0);
    }
}
