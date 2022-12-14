using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public  int         roundLenghtSeconds;                                         // The amount of time in seconds that a round lasts
    private int         countDownTimer;                                             // A variable used as the number shown on screen
    private int         minutes;                                                    // A variable used to hold the time left, (but only the minuts)
    private int         seconds;                                                    // A variable used to show the time left, (but only the seconds)
    public  TMP_Text    timerUI;                                                    // A variable used to hold a refrens to the text UI that shows the timer
    private float       timeElapsed;                                                // A variable used to hold the time sins the program was started when the timerUI is loaded
    public Slider       slider;

    void Start()                                                                    // Start is called before the first frame update
    {
        roundLenghtSeconds  = 300;                                                  // The length of the round (in seconds) is defined

        countDownTimer      = roundLenghtSeconds;                                   // The start value for the countDownTimer is asigned to be the lenght of the round
        slider.maxValue     = roundLenghtSeconds;
        slider.value        = 0;
        timeElapsed         = Time.time;
    }

    void FixedUpdate()                                                              // FixedUpdate is called once per specified time
    {
        
        minutes = countDownTimer / 60;                                                // the minuts are calculated by dividing the time left in seconds by 60 and rounding out by casting (int)
        seconds = countDownTimer % 60;                                                // the seconds are calculated by taking the seconds left and using modulus(%) 60.

        if (countDownTimer > 0)                                                     // an if statment is used to stop the countdown when zero(0) is reached
            countDownTimer = (int)(roundLenghtSeconds - (Time.time - timeElapsed));// The time left is calculatet by subtracting the amount of time passed from when the timer UI was called, from the total amount of time sins the game was started and then subtracting that from the lenght of the round in seconds

        

        slider.value = roundLenghtSeconds - countDownTimer;

        if (countDownTimer > 60)
        {
            timerUI.text = minutes.ToString() + ":" + string.Format("{0:00}", seconds); // The timerUI is asigned some text in the form of a String containing the minuts and seconds left. The part of the string containing the seconds is formatted to always show 2 digidets.

        } else
        {
            timerUI.text = countDownTimer.ToString();
        }
    }

    
}
