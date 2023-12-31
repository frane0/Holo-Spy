using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveATM : MonoBehaviour
{
    //toplanan puanların oyun kaydedildikten sonra tekrar toplanmasını engellemek için box colliderlarının aktif olup olmadığı durumunu PlayerPrefs ile kaydediyor
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
