using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource music;
    bool entering, secondfloor;
    public float fadeTime;
    character cha;

    private void Start()
    {
        cha=GetComponent<character>();
    }
    private void Update()
    {
        if(entering && music != null)
        {
            music.volume=Mathf.MoveTowards(music.volume, 1.0f, Time.deltaTime/fadeTime);
        }
        else if (!entering && music != null)
        {
            music.volume = Mathf.MoveTowards(music.volume, 0.0f, Time.deltaTime / fadeTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "First Music" && !secondfloor)
        {
            music=other.GetComponent<AudioSource>();
            music.Play();
            entering = true;
        }
        if (other.tag == "Stairs" && cha.doorOpened)
        {
            entering = false;
        }
        if (other.tag == "Second Music" && !secondfloor)
        {
            secondfloor = true;
            music = other.GetComponent<AudioSource>();
            music.Play();
            entering = true;
        }
    }
}
