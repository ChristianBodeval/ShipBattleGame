using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    private string currentText;
    public float delay = 0.05f;
    public string fullText = "";
    public float waitTimer = 2f;
    public TestTextWriter testTextWriter;
    private int index = 0;

    private void OnEnable()
    {
       StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {

    for (int i = 0; i < fullText.Length + 1; i++)
    {
        currentText = fullText.Substring(0, i);
        this.GetComponent<Text>().text = currentText;
        yield return new WaitForSeconds(delay);
    }
        yield return new WaitForSeconds(waitTimer);
        this.gameObject.SetActive(false);
        index = testTextWriter.GetIndex();
        index = index +1;
        testTextWriter.SetIndex(index) ;
    }

}
