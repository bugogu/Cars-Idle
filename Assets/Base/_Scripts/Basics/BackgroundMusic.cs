using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource _music;

    private GameObject[] _musics;
    private void Start()
    {
        _musics = GameObject.FindGameObjectsWithTag("Music");

        _music = GetComponent<AudioSource>();

        _music.Play();

        if (_musics.Length > 1)
            Destroy(_musics[1]);

        DontDestroyOnLoad(gameObject);
    }
}
