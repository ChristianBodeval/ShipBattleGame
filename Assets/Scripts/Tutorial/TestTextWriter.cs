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
    private bool moveAAndDKeys;
    private bool moveLeftAndRightKeys;
    private bool moveUpAndDownKeys;
    private bool checker = true;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.sceneLoaded
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
                //TutorialImage.gameObject.SetActive(false);


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
            default:
                Debug.Log("Oops, ran out of text UwU");
                break;
        }
        Debug.Log(index);

    }
    IEnumerator ShowObjects()
    {

        if (Input.GetKey(KeyCode.W) ||
               Input.GetKey(KeyCode.S) && index == 4)
        {
            yield return new WaitForSeconds(1);
            moveWAndSKeys = true;
            WAndSKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) && index == 4)
        {
            yield return new WaitForSeconds(1);
            moveAAndDKeys = true;
            AAndDKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) ||
           Input.GetKey(KeyCode.RightArrow) && index == 4)
        { 
            yield return new WaitForSeconds(1);
            moveLeftAndRightKeys = true;
            LeftAndRightKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) && index == 4)
        {
            yield return new WaitForSeconds(1);
            moveUpAndDownKeys = true;
            UpAndDownKey.gameObject.SetActive(false);
        }
        if (index == 4)
        {
            if (checker)
            {
            yield return new WaitForSeconds(3);
                TutorialImage.gameObject.SetActive(false);
                checker = false;
            }
        }
        if (Input.GetKey(KeyCode.G) ||
          Input.GetKey(KeyCode.H) && index == 7)
        {
            yield return new WaitForSeconds(1);
            shotKeyGAndH = true;
            GAndHKey.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Period) ||
          Input.GetKey(KeyCode.Minus) && index == 7)
        {
            yield return new WaitForSeconds(1);
            shotKeyPeriodAndMinus = true;
            PeriodAndMinusKey.gameObject.SetActive(false);
        }
        yield return null;
    }
   
    
        
}
