using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        bool quit;

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}