using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOver_UI : MonoBehaviour
{
    public GameObject gameOverScreen;
    private float timeElapsed;
    public TMP_Text winningPlayerTextUI;
    private string winningPlayerText;
    public TMP_Text losingPlayerTextUI;
    private string losingPlayerText;
    public int winningPlayer = 0;      // The winning player:  player_1 = 0    player_2 = 1;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = Time.time;
        gameOverScreen.transform.localScale = new Vector3(0f, 0f, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float scalor = (Time.time - timeElapsed)/2f;
        if (scalor >= 1) scalor = 1;
        gameOverScreen.transform.localScale = new Vector3(scalor, scalor, 1);

    }

    private void OnEnable()
    {
        if (winningPlayer == 0)
        {
            winningPlayerText = "Player 1";
            losingPlayerText = "Player 2";
        } else if (winningPlayer == 1)
        {
            winningPlayerText = "Player 2";
            losingPlayerText = "Player 1";
        } else
        {
            winningPlayerText = "No One";
            losingPlayerText = "No One";
        }

        winningPlayerTextUI.text = winningPlayerText;
        losingPlayerTextUI.text = losingPlayerText;

        timeElapsed = Time.time;
        gameOverScreen.transform.localScale = new Vector3(0f, 0f, 1);
    }
}
