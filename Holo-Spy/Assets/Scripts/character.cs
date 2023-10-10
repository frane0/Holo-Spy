using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
{
    Vector3 movement;
    public float mouseSensitivity = 10f, speed = 10f, mouseMax;
    public Animator anim;
    public int health;
    int xp, sayac_xp, sayac_stun, sayac_hit;
    public Slider healthBar, xpBar;
    public TMP_Text hp_text, xp_text, notification;
    public GameObject head, labyrinthDoor, youWon, gameOver, sp0, sp1, sp2, door1, enemies, door2, door3, parkour, door4;
    float yRot = 0, sayac_bildirim = 5;
    public SaveATM saveATM;
    public mainMenu menu;
    AudioSource sound;
    void Start()
    {
        Cursor.visible=false;
        if(!PlayerPrefs.HasKey("save"))
            PlayerPrefs.SetInt("save", 0);
        if (!PlayerPrefs.HasKey("xp"))
            PlayerPrefs.SetInt("xp", 0);
        /*PlayerPrefs.SetInt("save", 0);
        PlayerPrefs.SetInt("xp", 0);
        saveATM.DeleteSave();*/
        switch (PlayerPrefs.GetInt("save"))
        {
            case 0: transform.position = sp0.transform.position;
                break;
            case 1: transform.position = sp1.transform.position;
                break;
            case 2: transform.position = sp2.transform.position;
                break;
        }
        saveATM.Load();
        health = 100;
        healthBar.value = health;
        hp_text.text = health.ToString() + "/100";
        sayac_hit = 0;
        sayac_stun = 0;
        sayac_xp = 0;
        xp = PlayerPrefs.GetInt("xp");
        xpBar.value = xp;
        xp_text.text = xp.ToString() + "/10";
        print(PlayerPrefs.GetInt("save"));
        sound=GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (!menu.isPaused)
        {
            if (sayac_bildirim < 3)
            {
                notification.gameObject.SetActive(true);
            }
            else
            {
                notification.gameObject.SetActive(false);
            }
            sayac_bildirim += Time.fixedDeltaTime;

            healthBar.value = health;
            hp_text.text = health.ToString() + "/100";
            xpBar.value = xp;
            xp_text.text = xp.ToString() + "/10";

            if (anim.GetBool("hitFront"))
            {
                sayac_hit++;
                if (sayac_hit * Time.fixedDeltaTime >= 0.5)
                {
                    anim.SetBool("hitFront", false);
                    sayac_hit = 0;
                }
            }

            if (anim.GetBool("collect"))
            {
                sayac_xp++;
                if (sayac_xp * Time.fixedDeltaTime >= 0.5)
                {
                    anim.SetBool("collect", false);
                    sayac_xp = 0;
                }
            }

            if (anim.GetBool("stunFront"))
            {
                sayac_stun++;
                if (sayac_stun * Time.fixedDeltaTime >= 8)
                {
                    anim.SetBool("stunFront", false);
                    sayac_stun = 0;
                }
            }

            if (!anim.GetBool("stunFront") && !anim.GetBool("isDead"))
            {
                movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.fixedDeltaTime * speed;
                transform.Translate(movement);

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("isRunning", true);
                    if(!sound.isPlaying)
                        sound.Play();
                }
                else
                {
                    anim.SetBool("isRunning", false);
                    sound.Stop();
                }

                float mouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * mouseSensitivity;
                transform.Rotate(Vector3.up * mouseX);
                float mouseY = -Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * mouseSensitivity;
                yRot += mouseY;
                yRot = Mathf.Clamp(yRot, -mouseMax, mouseMax);
                head.transform.localRotation = Quaternion.Euler(yRot, 0, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

            Destroy(collision.gameObject);
            health -= 20;
            if (health <= 0)
            {
                health = 0;
                anim.SetBool("isDead", true);
                gameOver.SetActive(true);

            }
            anim.SetBool("hitFront", true);

        }

        else if (collision.gameObject.tag == "spell")
        {
            Destroy(collision.gameObject);
            health -= 5;
            if (health <= 0)
            {
                health = 0;
                anim.SetBool("isDead", true);
                gameOver.SetActive(true);
            }
            anim.SetBool("stunFront", true);

        }
    }
    public bool doorOpened = false;
    public bool mapObtained = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DoorControl" && !doorOpened)
        {
            sayac_bildirim = 0;
            notification.text = "Etkileþime geçmek için E'ye bas.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                labyrinthDoor.SetActive(false);
                doorOpened = true;
                sayac_bildirim = 0;
                notification.text = "Kapý Açýldý";
            }
        }
        if (other.tag == "Map" && !mapObtained)
        {
            sayac_bildirim = 0;
            notification.text = "Etkileþime geçmek için E'ye bas.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                mapObtained = true;
                sayac_bildirim = 0;
                notification.text = "Harita Edinildi!";
            }
        }
        if (other.tag == "CheckPoint 1")
        {
            if (sayac_bildirim > 3)
            {
                sayac_bildirim = 0;
                notification.text = "Etkileþime geçmek için E'ye bas.";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetInt("save", 1);
                PlayerPrefs.SetInt("xp", xp);
                saveATM.Save();
                health = 100;
                sayac_bildirim = 0;
                notification.text = "Veriler Kaydedildi ve Hologram Yenilendi!";
            }
        }
        if (other.tag == "Door 1" && door1.activeInHierarchy)
        {
            sayac_bildirim = 0;
            notification.text = "Etkileþime geçmek için E'ye bas.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                enemies.SetActive(true);
                door1.SetActive(false);
                sayac_bildirim = 0;
                notification.text = "Kapý Açýldý";
            }
        }

        if (other.tag == "Points")
        {
            sayac_bildirim = 0;
            notification.text = "Etkileþime geçmek için E'ye bas.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                xp++;
                other.gameObject.SetActive(false);
            }
        }

        if (other.tag == "Door 2" && door2.activeInHierarchy)
        {
            sayac_bildirim = 0;
            notification.text = "Etkileþime geçmek için E'ye bas.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                parkour.SetActive(true);
                door2.SetActive(false);
                sayac_bildirim = 0;
                notification.text = "Kapý Açýldý";
            }
        }
        if (other.tag == "Door 3" && door3.activeInHierarchy)
        {
            sayac_bildirim = 0;
            notification.text = "Etkileþime geçmek için E'ye bas.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                door3.SetActive(false);
                sayac_bildirim = 0;
                notification.text = "Kapý Açýldý";
            }
        }
        if (other.tag == "MainComputer")
        {
            if (sayac_bildirim > 3)
            {
                sayac_bildirim = 0;
                notification.text = "Etkileþime geçmek için E'ye bas.";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (xp == 10)
                {
                    youWon.SetActive(true);
                    Cursor.visible = true;
                }
                else
                {
                    sayac_bildirim = 0;
                    notification.text = "Þifreyi kýrabilmemiz için verilerin tamamýný göndermen gerek!";
                }
            }
        }

        if (other.tag == "Door 4" && door3.activeInHierarchy)
        {
            sayac_bildirim = 0;
            notification.text = "Etkileþime geçmek için E'ye bas.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                door4.SetActive(false);
                sayac_bildirim = 0;
                notification.text = "Kapý Açýldý";
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "laser")
        {
            health -= 20;
            if (health <= 0)
            {
                health = 0;
                anim.SetBool("isDead", true);
                gameOver.SetActive(true);
            }
            anim.SetBool("hitFront", true);
        }
    }

}
