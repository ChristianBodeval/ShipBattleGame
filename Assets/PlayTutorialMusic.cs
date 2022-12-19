using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTutorialMusic : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMusic("MainMenu");
    }
}
