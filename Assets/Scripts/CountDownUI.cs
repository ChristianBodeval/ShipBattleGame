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
    public int countDownLength = 10;
    private int countDown;
    private float scalor = 1f;


    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scalor = (Time.time - (int)Time.time) + 0.5f;
        timerUI.transform.localScale = new Vector3(scalor, scalor, 1);
        countDown = (int) (countDownLength - (Time.time - timeElapsed));
        timerUI.text = countDown.ToString();
        //timerUI.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        if (countDown <= 0)
        {
            countDown = 0;
            inGameUI.SetActive(true);
            countDownObject.SetActive(false);
        }
    }
}
