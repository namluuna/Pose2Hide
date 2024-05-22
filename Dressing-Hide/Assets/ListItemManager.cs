using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ObjectPool;

[Serializable]
public class backGroundObjectSetting
{
    public string name;
    public int unlock;

    public backGroundObjectSetting(string name, int unlock)
    {
        this.name = name;
        this.unlock = unlock;
    }


}

public class ListItemManager : MonoBehaviour
{

    public int backItemId;
    public int carpetId;
    public int floorId;
    public int leftItemId;
    public int onFloorId;
    public int rightItemId;
    public int upItemId;
    public int wallId;
    public static ListItemManager instance;

    public bool IsFirstTime =true;

    public Dictionary<string, int> Items = new Dictionary<string, int>();

    [SerializeField]
    public List<backGroundObjectSetting> listItem =  new List<backGroundObjectSetting>();

    public void SaveId()
    {
        PlayerPrefs.SetInt("backItemId", backItemId);
        PlayerPrefs.SetInt("carpetId", carpetId);
        PlayerPrefs.SetInt("floorId", floorId);
        PlayerPrefs.SetInt("leftItemId", leftItemId);
        PlayerPrefs.SetInt("onFloorId", onFloorId);
        PlayerPrefs.SetInt("rightItemId", rightItemId);
        PlayerPrefs.SetInt("upItemId", upItemId);
        PlayerPrefs.SetInt("wallId", wallId);
    }

    void IsFirstTimePlay()
    {
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

    private void Start()
    {

        if (PlayerPrefs.GetInt("IsFirstTime")==0)
        {
            IsFirstTime = true;
        }
        else
        {
            IsFirstTime = false;
        }

        if (IsFirstTime==true)
        {
            PlayerPrefs.SetInt("IsFirstTime", 1);
            IsFirstTimePlay();
            IsFirstTime = false;
        }

        LoadId();    
        GetData();
        //SaveDataToNull();

        fillDataFromPref();
        //Debug.Log(Items.Count);

    }

    public void Init()
    {
        LoadId();
        GetData();
        fillDataFromPref();
    }


    public void fillDataFromPref()
    {
        foreach (backGroundObjectSetting i in listItem)
        {
            i.unlock = PlayerPrefs.GetInt(i.name);
        }
    }

    public void GetData()
    {
        GameObject objectWithTag = GameObject.Find("MainUI");
        Transform firstChildTransform = objectWithTag.transform.GetChild(3);
        GameObject obj = firstChildTransform.gameObject;
        Transform getBoxlist = obj.transform.GetChild(0);
        Transform Grid = getBoxlist.GetChild(0);
        GameObject grid = Grid.gameObject;
        List<Pool> pool = grid.GetComponent<ObjectPoolForList>().pools;

        foreach (Pool pool1 in pool)
        {
            //listItem.Add(new backGroundObjectSetting(pool1.prefab.name, 0));
            if (pool1.prefab.GetComponent<BuyProperties>().statusItem==statusItem.needBuy)
            {
                listItem.Add(new backGroundObjectSetting(pool1.prefab.name, 0));
            }else if (pool1.prefab.GetComponent<BuyProperties>().statusItem == statusItem.own)
            {
                listItem.Add(new backGroundObjectSetting(pool1.prefab.name, 1));
            }
        }

    }


    public void SaveData()
    {

        GetData();
        foreach (backGroundObjectSetting i in listItem)
        {
            PlayerPrefs.SetInt(i.name, i.unlock);
        }
    }

    public void SaveDataToNull()
    {

        GetData();
        foreach (backGroundObjectSetting i in listItem)
        {
            PlayerPrefs.SetInt(i.name, 0);
        }
    }

    void LoadId()
    {
        backItemId = PlayerPrefs.GetInt("backItemId",0);
        carpetId = PlayerPrefs.GetInt("carpetId", 0);
        floorId = PlayerPrefs.GetInt("floorId", 0);
        leftItemId = PlayerPrefs.GetInt("leftItemId", 0);
        onFloorId = PlayerPrefs.GetInt("onFloorId", 0);
        rightItemId = PlayerPrefs.GetInt("rightItemId", 0);
        upItemId = PlayerPrefs.GetInt("upItemId", 0);
        wallId = PlayerPrefs.GetInt("wallId", 0);
    }

}
