using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSoundManager : MonoBehaviour
{
    public AudioSource MenuSound;
    
    // Start is called before the first frame update
    void Start()
    {
        //Stop all audio
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.Stop();
        }

        DontDestroyOnLoad(gameObject);
        MenuSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            Destroy(gameObject);
        }
    }
}
