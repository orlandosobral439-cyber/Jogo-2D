using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public AudioSource musicaDeFundo;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        musicaDeFundo.Play();
    }
}