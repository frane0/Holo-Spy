using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class wizard : MonoBehaviour
{
    public GameObject player;
    Vector3 direction;
    float angle;
    Quaternion lookDir;
    int sayac=0;
    public float cooldown = 4;
    public Animator anim;
    public Rigidbody spell_pf;
    public GameObject firePoint;
    bool isCasting = false;
    public float bulletSpeed = 30;
    public mainMenu menu;
    private void FixedUpdate()
    {
        if (!menu.isPaused)
        {
            sayac++;
            if (sayac * Time.fixedDeltaTime < cooldown && !isCasting)
            {
                gameObject.transform.LookAt(player.transform);
            }

            else if (sayac * Time.fixedDeltaTime >= cooldown && !isCasting)
            {
                anim.SetBool("cast", true);
                sayac = 0;
                isCasting = true;
            }

            else
            {
                if (sayac * Time.fixedDeltaTime >= 0.65f)
                {
                    Rigidbody spell_go;
                    spell_go = Instantiate(spell_pf, firePoint.transform.position, firePoint.transform.rotation);
                    spell_go.velocity = spell_go.transform.TransformDirection(Vector3.forward * bulletSpeed);
                    isCasting = false;
                    sayac = 0;
                    anim.SetBool("cast", false);
                }
            }
        }
        
    }
}
