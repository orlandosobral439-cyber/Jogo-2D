using UnityEngine;

public class somg1 : MonoBehaviour
{
    public static somg1 instance;

    public AudioSource somDaColeta, somDeDano, somDoPulo;

    void Awake()
    {
        instance = this;
    }
}