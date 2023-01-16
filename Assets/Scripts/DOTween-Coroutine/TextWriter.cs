using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public string textToShow;
    public float time;
   public TextMeshProUGUI textUI;

    //public string textB;
    void Start()
    {
        StartCoroutine("Writing");
        //textUI.text = textToShow + textB;
       // StartCoroutine("ShowText");
    }

    void Update()
    {
        
    }

    IEnumerator Writing()
    {
        for(int i = 0; i <= textToShow.Length; i++)
        {
        textUI.text = textToShow.Substring(0,i);
        yield return new WaitForSeconds(time);
        }
    }
    //método alternativo usando foreach
    IEnumerator ShowText()
    {
        textUI.text = " ";
        foreach(char c in textToShow)
        {
            textUI.text += c;
            yield return new WaitForSeconds(time);
        }
    }
}
