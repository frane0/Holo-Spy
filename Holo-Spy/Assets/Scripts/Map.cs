using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform player;
    public RectTransform playerIcon;
    public GameObject map;
    float iconLoc_x, iconLoc_y, playerLoc_x, playerLoc_z;

    private void Start()
    {
        map.SetActive(false);
    }
    private void Update()
    {
        if(player.GetComponent<character>().mapObtained)
        {
            if (Input.GetKeyDown(KeyCode.M))
                map.SetActive(!map.activeInHierarchy);
            if (Input.GetKeyDown(KeyCode.Escape))
                map.SetActive(false);
            playerLoc_x = player.transform.position.x;
            playerLoc_z = player.transform.position.z;

            iconLoc_x = (playerLoc_x / 6) * 27.8f;
            iconLoc_y = (playerLoc_z / 6 + 0.5f) * 27.8f;
            playerIcon.anchoredPosition = new Vector2(iconLoc_x, iconLoc_y);
        }
    }

}
