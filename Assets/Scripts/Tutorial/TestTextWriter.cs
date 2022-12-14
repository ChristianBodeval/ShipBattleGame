using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestTextWriter : MonoBehaviour
{
    public int index = 1;
    public float Delay = 0.1f;
    public float waitTimer = 2f;
    private bool[] soundChecker = new bool[50];
    private bool[] hasBeenPressed = new bool[50];
    private Ramming[] ramming;
    public Health health;
    public GameObject Island1;
    public GameObject Island2;
    public GameObject Merchantship;
    public GameObject Merchantship2;
    public GameObject PointingArrows;
    public GameObject PointingArrows2;
    public GameObject TPArrows;
    public GameObject TutorialImage;
    public GameObject RammingText;
    public GameObject[] Key;
    private bool[] moveKeys = new bool[50];
    private bool[] shootKeys = new bool[50];
    private bool[] dashKeys = new bool[50];

    public AudioSource textSound;
    public Text[] text;
    // Array of booleans to check if TutorialImage has been shown, Starts off false

    public int GetIndex()
    {
        return index;
    }

    public int SetIndex(int index)
    {
        this.index = index;
        return this.index;
    }

    private void Start()
    {
        /*
        foreach (ShipManager ship in GameManager.Instance.players)
        {
            ramming.Add ship.GetComponent<Ramming>();
        }
        GameManager.Instance.players*/
    }

    private void Update()
    {
        ramming = FindObjectsOfType<Ramming>();
        ShowObjects();
        switch (index)
        {
            case 1:
                text[index].gameObject.SetActive(true);
                break;

            case 2:
                text[index].gameObject.SetActive(true);
                break;

            case 3:
                text[index].gameObject.SetActive(true);
                break;

            case 4:

                for (int i = 0; i < 8; i++)
                {
                    Key[i].SetActive(true);
                }
                text[index].gameObject.SetActive(true);
                break;

            case 5:
                TutorialImage.gameObject.SetActive(false);

                if (moveKeys[0] && moveKeys[1] && moveKeys[2] && moveKeys[3] && moveKeys[4] && moveKeys[5] && moveKeys[6] && moveKeys[7]) //all versions of movement keys for both boats
                {
                    text[index].gameObject.SetActive(true);
                    TutorialImage.gameObject.SetActive(true);
                }
                break;

            case 6: // Teleporter
                text[index].gameObject.SetActive(true);
                break;

            case 7: // Teleporter 2
                text[index].gameObject.SetActive(true);
                TPArrows.gameObject.SetActive(true);
                break;

            case 8:
                text[index].gameObject.SetActive(true);
                TPArrows.gameObject.SetActive(false);
                break;

            case 9:
                text[index].gameObject.SetActive(true);
                for (int i = 8; i < 12; i++)
                {
                    Key[i].SetActive(true);
                }
                break;

            case 10:
                TutorialImage.gameObject.SetActive(false);

                if (shootKeys[8] && shootKeys[9] && shootKeys[10] && shootKeys[11]) //both version of shootKeys from each boat
                {
                    text[index].gameObject.SetActive(true);
                    TutorialImage.gameObject.SetActive(true);
                }
                break;

            case 11: // Ramming
                text[index].gameObject.SetActive(true);
                break;

            case 12: // Ramming 2
                text[index].gameObject.SetActive(true);
                RammingText.gameObject.SetActive(true);
                Key[12].gameObject.SetActive(true);
                Key[13].gameObject.SetActive(true);
                break;

            case 13: // Dash
                TutorialImage.SetActive(false);
                for (int i = 0; i < ramming.Length; i++)
                {
                    if (ramming[i].IsRammed == true && index == 13 && dashKeys[12] && dashKeys[13])
                    {
                        Debug.Log("RAMMED");
                        text[index].gameObject.SetActive(true);
                        RammingText.gameObject.SetActive(false);
                        TutorialImage.SetActive(true);
                    }
                }
                break;

            case 14:
                text[index].gameObject.SetActive(true);
                Island1.gameObject.SetActive(true);
                Island2.gameObject.SetActive(true);
                break;

            case 15:
                TutorialImage.gameObject.SetActive(false);
                if (Island1.active == false && Island2.active == false)
                {
                    TutorialImage.gameObject.SetActive(true);
                    text[index].gameObject.SetActive(true);
                }
                break;

            case 16:
                text[index].gameObject.SetActive(true);
                TutorialImage.gameObject.SetActive(true);
                PointingArrows.gameObject.SetActive(true);
                PointingArrows2.gameObject.SetActive(true);

                break;

            case 17:
                text[index].gameObject.SetActive(true);
                PointingArrows.gameObject.SetActive(false);
                PointingArrows2.gameObject.SetActive(false);

                break;

            case 18:
                text[index].gameObject.SetActive(true);
                Merchantship.gameObject.SetActive(true);
                Merchantship2.gameObject.SetActive(true);

                break;

            case 19:
                TutorialImage.gameObject.SetActive(false);
                if (health.dead == true)
                {
                    text[index].gameObject.SetActive(true);
                    TutorialImage.gameObject.SetActive(true);
                    //index++;
                }
                break;
            case 20:
                SceneManager.LoadScene("MainMenu_Start");
                break;
            default:
                Debug.Log("Oops, ran out of text");
                break;
        }

        Debug.Log("index is: " + index);
    }

    private void ShowObjects()
    {
        KeyCode[] keyCodes = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.G, KeyCode.H, KeyCode.Period, KeyCode.Minus, KeyCode.W, KeyCode.UpArrow };

        for (int i = 0; i < 8; i++)
        {
            if (index == 5 && Input.GetKeyDown(keyCodes[i]) && soundChecker[i] == false) // Move keys
            {
                soundChecker[i] = true;
                moveKeys[i] = true;
                Key[i].SetActive(false);

                if (hasBeenPressed[i] == false)
                {
                    textSound.Play();
                    hasBeenPressed[i] = true;
                }
            }
        }
        for (int i = 8; i < 12; i++)
        {
            if (index == 10 && Input.GetKeyDown(keyCodes[i]) && soundChecker[i] == false) // Shoot keys
            {
                soundChecker[i] = true;
                shootKeys[i] = true;
                Key[i].SetActive(false);

                if (hasBeenPressed[i] == false)
                {
                    textSound.Play();
                    hasBeenPressed[i] = true;
                }
            }
        }

        for (int i = 12; i < 14; i++)
        {
            if (index == 13 && Input.GetKeyDown(keyCodes[i]) && soundChecker[i] == false) // Dash keys
            {
                soundChecker[i] = true;
                dashKeys[i] = true;
                Key[i].SetActive(false);

                if (hasBeenPressed[i] == false)
                {
                    textSound.Play();
                    hasBeenPressed[i] = true;
                }
            }
        }
    }
}