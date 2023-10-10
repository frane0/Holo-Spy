using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveATM : MonoBehaviour
{
    //toplanan puanlar�n oyun kaydedildikten sonra tekrar toplanmas�n� engellemek i�in box colliderlar�n�n aktif olup olmad��� durumunu PlayerPrefs ile kaydediyor
    public GameObject[] atms;
    public void Save()
    {
        foreach(GameObject atm in atms)
        {
            if (atm.activeInHierarchy)
                PlayerPrefs.SetInt(atm.name, 1);
            else
                PlayerPrefs.SetInt(atm.name, 0);
        }
    }

    public void Load()
    {
        foreach(GameObject atm in atms)
        {
            if (PlayerPrefs.HasKey(atm.name))
            {
                if (PlayerPrefs.GetInt(atm.name) == 1)
                    atm.SetActive(true);
                else
                    atm.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt(atm.name, 1);
            }
        }
    }
    public void DeleteSave()
    {
        foreach(GameObject atm in atms)
        {
            PlayerPrefs.DeleteKey(atm.name);
        }
    }
}
