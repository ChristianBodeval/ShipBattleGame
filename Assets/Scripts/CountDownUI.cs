using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    public GameObject countDownObject;
    public GameObject inGameUI;

    public TMP_Text timerUI;
    private float timeElapsed;
    public int countDownLength;
    public int countDown;
    private float scalor = 1f;

    private void Start()
    {
        timeElapsed = Time.time;
        //countDownLength will start from countDownLength-1, therefore add 1
        countDownLength += 1;
    }

// Update is called once per frame
void Update()
    {
        scalor = (Time.time - (int)Time.time) + 0.5f;
        timerUI.transform.localScale = new Vector3(scalor, scalor, 1);
        countDown = (int) (countDownLength - (Time.time - timeElapsed));
        timerUI.text = countDown.ToString();

        if (countDown < 1)
        {
            timerUI.text = "FIGHT!!!";
            timerUI.transform.localScale = new Vector3(4f, 4f, 1);
        }

        //timerUI.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        if (countDown <= -1)
        {
            countDown = -1;
            inGameUI.SetActive(true);
            countDownObject.SetActive(false);
        }
    }
}
