using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public TMP_Text panel;
    public string[] stories;
    int index = 0;

    public void Next()
    {
        if(index <= stories.Length-1)
        {
            panel.text = stories[index];
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        index++;

    }
}
