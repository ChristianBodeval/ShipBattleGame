using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TextWriter : MonoBehaviour
{
    private int index = 1;
    public float delay = 0.1f;
    public float waitTimer = 2f;
    private string fullText;                        // The Text currently displayed
    private string currentText = "";
    private bool[] firstText = new bool[20]; //Bool array to make sure the text isnt showing every time update runs, 20 to allow for more text
    private bool buttonClicked = true;
    private bool moveWAndSKeys = false;
    private bool shotKeyGAndH = false;
    private bool shotKeyPeriodAndMinus = false;
    public GameObject AAndDKey;                     // GameObjects for all the different visuals
    public GameObject WAndSKey;
    public GameObject periodAndMinusKey;
    public GameObject GAndHKey;
    public GameObject LeftAndRightKey;
    public GameObject UpAndDownKey;
    public GameObject Island1;
    public GameObject Island2;
    public GameObject Mechantship;
    public GameObject PointingArrows;
    private bool moveAAndDKeys;
    private bool moveLeftAndRightKeys;
    private bool moveUpAndDownKeys;

    // Start is called before the first frame update
    void Start(){}
    private void FixedUpdate()
    {
                                                    // Runs the functions to check wether those buttons has been clicked
        MoveWAndS();
        MoveAAndD();
        MoveLeftAndRight();
        MoveUpAndDown();
        ShootKeyGAndH();
        ShootKeyPeriodAndMinus();
        
        // a switch to check and call which text is going to be runned
        switch (index)
        {
            case 1:
                if (!firstText[0])
                {
                    buttonClicked = false;
                    fullText = "AAARRRRGHHH! Welcome to the Seven Seas!";
                    firstText[0] = true;
                    StartCoroutine(ShowText());
                }
                break;

            case 2:
                if (!firstText[1])
                {
                    buttonClicked = false;
                    fullText = "Time to show what ye’ve got!";
                    firstText[1] = true;
                    StartCoroutine(ShowText());
                }
                break;

            case 3:
                if (!firstText[2])
                {
                    buttonClicked = false;
                    fullText = "Your primary mission is to be da LAST MAN STANDIN'!";
                    firstText[2] = true;
                    StartCoroutine(ShowText());
                }
                break;

            case 4:
                
                if (!firstText[3])
                {
                    buttonClicked = false;
                    fullText = "Try moving ya ship!";
                    firstText[3] = true;
                                                                    // Using invoke to show and unshow visuals after a certant amount of time
                    StartCoroutine(ShowText());
                    Invoke("ShowAAndDKeys", waitTimer);
                    Invoke("ShowWAndSKeys", waitTimer);
                    Invoke("ShowLeftAndRightKeys", waitTimer);
                    Invoke("ShowUpAndDownKeys", waitTimer);
                }
                break;
            case 5:
                
                buttonClicked = true;
                if (moveWAndSKeys == true && moveAAndDKeys == true &&
                    moveLeftAndRightKeys == true && moveUpAndDownKeys == true &&          // If all types of moveKeys have been pressed, then go on
                    !firstText[4])
                {
                    buttonClicked = false;
                    fullText = "NICE! Excellent maneuvers";
                    firstText[4] = true;
                }
                if(buttonClicked == false) {
                    waitTimer += 4f;
                    StartCoroutine(ShowTextNoWait());
                    waitTimer -= 4f;
                    Invoke("UnShowAandDKeys", waitTimer);                                
                    Invoke("UnShowWAndSKeys", waitTimer);
                    Invoke("UnShowLeftAndRightKeys", waitTimer);
                    Invoke("UnShowUpAndDownKeys", waitTimer);
                    buttonClicked = true;
                }
                break;
            case 6:
                if (!firstText[5])
                {
                    buttonClicked = false;
                    fullText = "And as you know, the best Defense is OFFENSE!";
                    firstText[5] = true;
                    StartCoroutine(ShowText());
                }
                break;
            case 7:
                if (!firstText[6])
                {
                    buttonClicked = false;

                    fullText = "Try shooting your cannons!";
                    firstText[6] = true;
                    StartCoroutine(ShowText());
                    Invoke("ShowPeriodAndMinusKeys", waitTimer-1f);
                    Invoke("ShowGAndHKeys", waitTimer-1f);
                }
                break;
            case 8:
                buttonClicked = true;
                if (shotKeyGAndH == true && shotKeyPeriodAndMinus == true && !firstText[7])    // If both types of shootkeys have been pressed, go on
                {
                    buttonClicked = false;
                    fullText = "NICE SHOT!";
                    firstText[7] = true;
                }
                if (buttonClicked == false)
                {
                    waitTimer = 4f;
                    StartCoroutine(ShowTextNoWait());
                    waitTimer -= 4f;
                    Invoke("UnShowPeriodAndMinusKeys", waitTimer + 2f);
                    Invoke("UnShowGAndHKeys", waitTimer + 2f);
                    buttonClicked = true;
                }
             
                break;
            case 9:
                if (!firstText[8])
                {
                    buttonClicked = false;

                    fullText = "Now the real fun begins! Muahahha";
                    firstText[8] = true;
                    StartCoroutine(ShowText());
                }
                break;
            case 10:
                if (!firstText[9])
                {
                    buttonClicked = false;

                    fullText = "Ya' see those irrrlands?? Destroy them to gain powerups";
                    firstText[9] = true;
                    Invoke("ShowIsland1", waitTimer - 1f);                                  // Activates both islands
                    Invoke("ShowIsland2", waitTimer - 1f);
                    waitTimer = 5f; //Change to NextText() after islands are killed
                    StartCoroutine(ShowText());
                    waitTimer -= 5f;
                   

                }
                break;
            case 11:
                if (!firstText[10])
                {
                    waitTimer = 4f;
                    buttonClicked = false;

                    fullText = "Keep an eye on ya ship's health in ur dedicated corner";
                    firstText[10] = true;
                    StartCoroutine(ShowText());
                    waitTimer -= 2f;
                    Invoke("UnShowIsland1", waitTimer + 2f);
                    Invoke("UnShowIsland2", waitTimer + 2f);
                    Invoke("ShowPointingArrows", waitTimer -1f);
                }
                break;
            case 12:
                if (!firstText[11])
                {
                    buttonClicked = false;

                    fullText = "Caus' theres only one way to regen it, by destroying merchantships!";
                    firstText[11] = true;
                    StartCoroutine(ShowText());
                    Invoke("UnShowPointingArrows", waitTimer + 2f);
                }
                break;
            case 13:
                if (!firstText[12])
                {
                    buttonClicked = false;

                    fullText = "Look! Some is coming this way!";
                    firstText[12] = true;
                    StartCoroutine(ShowText());
                }
                break;
            case 14:
                if (!firstText[13])
                {
                    buttonClicked = false;

                    fullText = "Looks like ya got dis under wraps, time to go out and show ya skills!";
                    firstText[13] = true;
                    waitTimer = 7f;
                    StartCoroutine(ShowText());
                }
                break;

            default:
                fullText = "ERROR! Index not found";
                break;
        }
        Debug.Log("Index is: " + index);
    }
   
    void MoveWAndS()
    {
        if (Input.GetKey(KeyCode.W)         ||
            Input.GetKey(KeyCode.S))
        {
            moveWAndSKeys = true;
            Debug.Log("Movekeys is: " + moveWAndSKeys);
        }
       
    }
    void MoveAAndD()
    {
        if( Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D))
        {
            moveAAndDKeys = true;
        }
    }
    void MoveLeftAndRight()
    {
        if (Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            moveLeftAndRightKeys = true;
        }
    }
    void MoveUpAndDown()
    {
        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow))
        {
            moveUpAndDownKeys = true;
        }
    }
    void ShootKeyGAndH()
    {
        if (Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.H))
        {
            shotKeyGAndH = true;
        }
        Debug.Log("THE shotKeyGAndH BOOLEAN IS "+ shotKeyGAndH);
      
    } 
    void ShootKeyPeriodAndMinus()
    {
        if (Input.GetKey(KeyCode.Period) || Input.GetKey(KeyCode.Minus))
        {
            shotKeyPeriodAndMinus = true;
        }
    }
    void NextText()
    {
         index++;
    }
    void ShowAAndDKeys()
    {
        AAndDKey.gameObject.SetActive(true);
    }
    void UnShowAandDKeys()

    {
        AAndDKey.gameObject.SetActive(false);
    }
    void ShowWAndSKeys()
    {
        WAndSKey.gameObject.SetActive(true);
    }
    void UnShowWAndSKeys()
    {
        WAndSKey.gameObject.SetActive(false);
    }
    void ShowUpAndDownKeys()
    {
        UpAndDownKey.gameObject.SetActive(true);
    }
    void UnShowUpAndDownKeys()
    {
        UpAndDownKey.gameObject.SetActive(false);
    }
    void ShowLeftAndRightKeys()
    {
        LeftAndRightKey.gameObject.SetActive(true);
    }
    void UnShowLeftAndRightKeys()
    {
        LeftAndRightKey.gameObject.SetActive(false);
    }
    void ShowPeriodAndMinusKeys()
    {
        periodAndMinusKey.gameObject.SetActive(true);
    }
    void UnShowPeriodAndMinusKeys()
    {
        periodAndMinusKey.gameObject.SetActive(false);

    }
    void ShowGAndHKeys()
    {
        GAndHKey.gameObject.SetActive(true);

    }
    void UnShowGAndHKeys()
    {
        GAndHKey.gameObject.SetActive(false);

    }
    void ShowIsland1()
    {
        Island1.gameObject.SetActive(true);
    }
    void UnShowIsland1()
    {
        Island1.gameObject.SetActive(false);
    }
    void ShowIsland2()
    {
        Island2.gameObject.SetActive(true);
    }
    void UnShowIsland2()
    {
        Island2.gameObject.SetActive(false);
    }
    void ShowPointingArrows()
    {
        PointingArrows.gameObject.SetActive(true);
    }
    void UnShowPointingArrows()
    {
        PointingArrows.gameObject.SetActive(false);
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
         if(!buttonClicked)
        Invoke("NextText", waitTimer);
    }
    IEnumerator ShowTextNoWait()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            //NextText();
            yield return new WaitForSeconds(delay);
        }
        //index = 6;
        NextText();
    }
}
