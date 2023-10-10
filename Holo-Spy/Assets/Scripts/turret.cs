using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class turret : MonoBehaviour
{
    public GameObject player, gun;
    int sayac = 0;
    public float cooldown = 4;
    public Animator anim;
    public Rigidbody bullet_pf;
    public GameObject firePoint;
    bool isFiring = false;
    public float bulletSpeed=40;
    public mainMenu menu;
    private void FixedUpdate()
    {
        if (!menu.isPaused)
        {
            sayac++;
            if (sayac * Time.fixedDeltaTime < cooldown && !isFiring)
            {
                gun.transform.LookAt(player.transform, gameObject.transform.up);
            }

            else if (sayac * Time.fixedDeltaTime >= cooldown && !isFiring)
            {
                anim.SetBool("fire", true);
                sayac = 0;
                isFiring = true;
            }

            else
            {
                if (sayac * Time.fixedDeltaTime >= 0.1f)
                {
                    Rigidbody bullet_go;
                    bullet_go = Instantiate(bullet_pf, firePoint.transform.position, firePoint.transform.rotation);
                    bullet_go.velocity = bullet_go.transform.TransformDirection(Vector3.up * bulletSpeed);
                    isFiring = false;
                    sayac = 0;
                    anim.SetBool("fire", false);
                }
            }
        }
        
    }
}