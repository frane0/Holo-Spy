using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserGuns : MonoBehaviour
{
    public GameObject laserBeam;
    public float cooldown, laserTime, offset;
    float counter=0;
    bool waitedForOffset=false;
    public mainMenu menu;

    private void Update()
    {
        if (!menu.isPaused)
        {
            if (waitedForOffset)
            {
                if (counter < cooldown)
                {
                    counter += Time.deltaTime;
                    laserBeam.SetActive(false);
                }
                else if (counter >= cooldown && counter < cooldown + laserTime)
                {
                    laserBeam.SetActive(true);
                    counter += Time.deltaTime;
                }
                else if (counter >= laserTime + cooldown)
                {
                    counter = 0;
                }
            }
            else
            {
                if (counter < offset)
                {
                    counter += Time.deltaTime;
                }
                else
                {
                    waitedForOffset = true;
                    counter = 0;
                }
            }
        }
    }
}
