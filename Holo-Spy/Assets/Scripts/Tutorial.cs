using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] texts;
    int index=0;
    private void Update()
    {
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }

        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            panel.SetActive(true);
            if (index < texts.Length - 1)
            {
                texts[index].SetActive(true);
                if (Input.anyKeyDown)
                {
                    texts[index].SetActive(false);
                    index++;
                }
            }
            else
            {
                texts[index].SetActive(true);
                if (Input.anyKeyDown)
                {
                    texts[index-1].SetActive(false);
                    panel.SetActive(false);
                    PlayerPrefs.SetInt("Tutorial", 1);
                }
            }
        }
        else
        {
            index = 0;
            panel.SetActive(false);
        }
    }
}
