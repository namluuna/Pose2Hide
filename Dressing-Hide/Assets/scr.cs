using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPool;

public class scr : MonoBehaviour
{

    private void Start()
    {
        PlayerPrefs.SetInt("IsFirstTime",0);

        PlayerPrefs.SetInt("backItem_3", 1);
        PlayerPrefs.SetInt("carpet_3", 1);
        PlayerPrefs.SetInt("floor_3", 1);
        PlayerPrefs.SetInt("leftItem_3", 1);
        PlayerPrefs.SetInt("onFloor_3", 1);
        PlayerPrefs.SetInt("rightItem_3", 1);
        PlayerPrefs.SetInt("upItem_3", 1);
        PlayerPrefs.SetInt("wall_3", 1);




        PlayerPrefs.SetInt("backItemId", 3);
        PlayerPrefs.SetInt("carpetId", 3);
        PlayerPrefs.SetInt("floorId", 3);
        PlayerPrefs.SetInt("leftItemId", 3);
        PlayerPrefs.SetInt("onFloorId", 3);
        PlayerPrefs.SetInt("rightItemId", 3);
        PlayerPrefs.SetInt("upItemId", 3);
        PlayerPrefs.SetInt("wallId", 3);
    }
}
