using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{

    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    public GameObject[] textPopUp;
    public int index;
    public int runner;
    private bool oneTime= false;
    private bool twoTime= false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(ShowText());


    }
    private void Update()
    {
        if (index == 0 && !oneTime)
        {
            fullText = "Vi tester nu nummer et";
            oneTime = true;
            StartCoroutine(ShowText());
        }
        if (index == 1 && !twoTime)
        {
            fullText = "Tester nu også to";
            currentText = "Tester nu også to";
            twoTime = true;
            StartCoroutine(ShowText());
        }
        Debug.Log("Index is: " + index);
        Debug.Log("fullText er: "+fullText);
        Debug.Log("currentText er: "+currentText);
    }

    void CoroutineRunner()
    {
       
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        index++;
    }
}
