using UnityEngine;

public class DestruirComOTempo: MonoBehaviour
{
    public float tempoDeVida;
    
    void Start()
    {
        Destroy(this.gameObject, tempoDeVida);
    }
}