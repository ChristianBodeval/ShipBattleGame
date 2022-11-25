using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestTextWriter : MonoBehaviour
{
    public int index = 1;
    public float Delay = 0.1f;
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
    public GameObject PeriodAndMinusKey;
    public GameObject GAndHKey;
    public GameObject LeftAndRightKey;
    public GameObject UpAndDownKey;
    public GameObject Island1;
    public GameObject Island2;
    public GameObject Merchantship;
    public GameObject PointingArrows;
    public GameObject TutorialImage;
    public Health healthScript;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    public Text text7;
    public Text text8;
    public Text text9;
    public Text text10;
    public Text text11;
    public Text text12;
    public Text text13;
    public Text text14;
    public Text text15;
    private bool moveAAndDKeys;
    private bool moveLeftAndRightKeys;
    private bool moveUpAndDownKeys;
    private bool checker = true;
    private bool checker2 = true;

    // Start is called before the first frame update
    void Start()
    {
    }
    public int GetIndex()
    {
        return index;
    }
    public int SetIndex(int index)
    {
        this.index = index;
        return this.index;
    }
    
    //healthScript = Health.CurrentHealth();
    private void Update()
    {
        StartCoroutine(ShowObjects());
        switch (index)
        {
            case 1:
                text1.gameObject.SetActive(true);
                break;
            case 2:
                text2.gameObject.SetActive(true);
                break;
            case 3:
                text3.gameObject.SetActive(true);
                break;
            case 4:

                text4.gameObject.SetActive(true);
                WAndSKey.gameObject.SetActive(true);
                AAndDKey.gameObject.SetActive(true);
                LeftAndRightKey.gameObject.SetActive(true);
                UpAndDownKey.gameObject.SetActive(true);
                break;
            case 5:

                if (moveAAndDKeys && moveWAndSKeys && moveLeftAndRightKeys && moveUpAndDownKeys)
                {
                    text5.gameObject.SetActive(true);
                    TutorialImage.gameObject.SetActive(true);
                }
                break;
            case 6:
                text6.gameObject.SetActive(true);
                break;
            case 7:
                text7.gameObject.SetActive(true);
                GAndHKey.gameObject.SetActive(true);
                PeriodAndMinusKey.gameObject.SetActive(true);
                break;
            case 8:
                if (shotKeyPeriodAndMinus && shotKeyGAndH)
                {
                    text8.gameObject.SetActive(true);
                    TutorialImage.gameObject.SetActive(true);
                }
                break;
            case 9:
                text9.gameObject.SetActive(true);
                break;
            case 10:
                text10.gameObject.SetActive(true);
                Island1.gameObject.SetActive(true);
                Island2.gameObject.SetActive(true);

                break;
            case 11:
                //if (healthScript.dead)
                if (Island1.active == false && Island2.active == false)
                { 
                text11.gameObject.SetActive(true);
                }
                break;
            case 12:
                text12.gameObject.SetActive(true);
                PointingArrows.gameObject.SetActive(true);

                break;
            case 13:
                text13.gameObject.SetActive(true);

                break;
            case 14:
                text14.gameObject.SetActive(true);
                Merchantship.gameObject.SetActive(true);
                break;
            case 15:
                text15.gameObject.SetActive(true);
                break;
            default:
                Debug.Log("Oops, ran out of text UwU");
                break;
        }
        Debug.Log(index);
    }
    IEnumerator ShowObjects()
    {

        if (Input.GetKey(KeyCode.W) && index == 5 ||
            Input.GetKey(KeyCode.S) && index == 5)
        {
            yield return new WaitForSeconds(1);
            moveWAndSKeys = true;
            WAndSKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.A) && index == 5 ||
            Input.GetKey(KeyCode.D) && index == 5)
        {
            yield return new WaitForSeconds(1);
            moveAAndDKeys = true;
            AAndDKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && index == 5 ||
           Input.GetKey(KeyCode.RightArrow) && index == 5)
        { 
            yield return new WaitForSeconds(1);
            moveLeftAndRightKeys = true;
            LeftAndRightKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.UpArrow)   && index == 5 ||
            Input.GetKey(KeyCode.DownArrow) && index == 5)
        {
            yield return new WaitForSeconds(1);
            moveUpAndDownKeys = true;
            UpAndDownKey.gameObject.SetActive(false);
            Debug.Log("pressing moveing buttons atm!");
        }
        if (index == 5)
        {
            if (checker)
            {
                checker = false;
                TutorialImage.gameObject.SetActive(false);
            }
        }
        if (index == 8)
        {
            if (checker2)
            {
                checker2 = false;
                TutorialImage.gameObject.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.G) && index == 8 ||
          Input.GetKey(KeyCode.H) && index == 8)
        {
            yield return new WaitForSeconds(1);
            shotKeyGAndH = true;
            GAndHKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Period) && index == 8 ||
          Input.GetKey(KeyCode.Minus) && index == 8)
        {
            yield return new WaitForSeconds(1);
            shotKeyPeriodAndMinus = true;
            PeriodAndMinusKey.gameObject.SetActive(false);
        }
        if (index == 13)
        {
            yield return new WaitForSeconds(3);
            PointingArrows.gameObject.SetActive(false);
        }

        if (index == 15)
        {
            
        }

        yield return null;
    }
   
    
        
}
