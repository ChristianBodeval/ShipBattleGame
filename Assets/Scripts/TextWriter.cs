using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private int index = 1;
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    public GameObject[] textPopUp;
    private bool[] firstText = new bool[20];

    // Start is called before the first frame update
    void Start(){}
    private void Update()
    {
       
        // a switch to check and call which text is going to be runned
        switch(index)
        {
            case 1:
                if (!firstText[0])
                {
                    fullText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla";
                    firstText[0] = true;
                    StartCoroutine(ShowText());
                }
                break;

            case 2:
                if (!firstText[1])
                {
                    fullText = "Tester nu også to";
                    firstText[1] = true;
                    StartCoroutine(ShowText());
                }
                break;
            case 3:
                if (!firstText[2])
                {
                    fullText = "Nu tester vi fandme nummer 3 du";
                    firstText[2] = true;
                    StartCoroutine(ShowText());
                }
                break;
        }
        Debug.Log("Index is: " + index);
        Debug.Log("fullText er: "+fullText);
        Debug.Log("currentText er: "+currentText);
    }

    void CoroutineRunner()
    {
        index++;
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Invoke("CoroutineRunner", 2f);
    }
}
