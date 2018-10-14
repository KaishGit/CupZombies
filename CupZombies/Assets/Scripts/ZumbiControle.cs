using UnityEngine;

public class ZumbiControle : MonoBehaviour
{
    public float Velocidade;

    void Update()
    {
        transform.Translate(-Velocidade * Time.deltaTime, 0, 0);
    }
}
