using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ObjectPool;

public class ObjectPoolForList : MonoBehaviour
{
    public bool setActive;

    public static ObjectPoolForList instance;

    [SerializeField]
    public typeObject Type;
    
    [SerializeField]
    GameObject backItem,
    arpet,
    floor,
    leftItem,
    onFloor,
    rightItem,
    upItem,
    wall;

    
    [SerializeField]
    Image backItem2,
    arpet2,
    floor2,
    leftItem2,
    onFloor2,
    rightItem2,
    upItem2,
    wall2;

    [SerializeField]
    Sprite select;
    [SerializeField]
    Sprite selected;

    private void Awake()
    {
        instance = this;
        SetDataToPoolList();
    }

    public List<Pool> pools;
    public List<GameObject> poolObject;



    private void Start()
    {
        setDataForDictionary();

        //poolObjecToList();
    }

    public void SetDataToPoolList()
    {
        GameObject listItemManager = GameObject.Find("ListItemManager");
        foreach (var i in listItemManager.GetComponent<ListItemManager>().listItem)
        {
            foreach (Pool p in pools)
            {
                if (i.name ==  p.prefab.name)
                {
                    if (i.unlock ==1)
                    {
                        p.prefab.GetComponent<BuyProperties>().statusItem = statusItem.own;
                    }else if (i.unlock == 0)
                    {
                        p.prefab.GetComponent<BuyProperties>().statusItem = statusItem.needBuy;
                    }
                    
                }
            }
        }
    }

    void setDataForDictionary()
    {

        GameObject listItemManager = GameObject.Find("ListItemManager");


        foreach (var i in listItemManager.GetComponent<ListItemManager>().listItem)
        {

            foreach (Pool pool in pools)
            {
                if (i.name == pool.prefab.name)
                {
                    if (pool.prefab.GetComponent<BuyProperties>().statusItem == statusItem.own)
                    {
                        i.unlock = 1;
                    }
                }
            }    
        }
    }

    public void poolObjecToList()
    {
        poolObject = new List<GameObject>();
        ClearObject();
        ActivateObjectOfType();
        try
        {
            foreach (Pool pool in pools)
            {
                if (pool.prefab.GetComponent<backgroundObject>().type == Type)
                {
                    GameObject obj = Instantiate(pool.prefab, gameObject.transform);
                    obj.SetActive(setActive);
                    obj.transform.position = gameObject.transform.position;
                    obj.transform.rotation = Quaternion.identity;
                    obj.transform.GetComponentInChildren<buttonManager>().transform.localPosition = new Vector3(0, -75, 0);
                    obj.transform.GetComponentInChildren<buttonManager>().transform.localScale = new Vector3(0.9f,0.9f,0.6f);
                    //obj.transform.GetComponentInChildren<buttonManager>().transform.GetChild(0).localScale = 
                    poolObject.Add(obj);
                }
            }
        }
        catch
        {

        }
    }

    public void ClearObject()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length; i++)
        {
            Destroy(children[i].gameObject);
        }
        poolObject.Clear();
    }

    public void ActivateObjectOfType()
    {
        switch (Type)
        {
            case typeObject.backItem:
                SetActiveGameObject(backItem, backItem2);
                break;
            case typeObject.arpet:
                SetActiveGameObject(arpet, arpet2);
                break;
            case typeObject.floor:
                SetActiveGameObject(floor, floor2);
                break;
            case typeObject.leftItem:
                SetActiveGameObject(leftItem, leftItem2);
                break;
            case typeObject.onFloor:
                SetActiveGameObject(onFloor, onFloor2);
                break;
            case typeObject.rightItem:
                SetActiveGameObject(rightItem, rightItem2);
                break;
            case typeObject.upItem:
                SetActiveGameObject(upItem, upItem2);
                break;
            case typeObject.wall:
                SetActiveGameObject(wall, wall2);
                break;
            default:
                // Xử lý nếu giá trị enum không hợp lệ.
                break;
        }
    }

    private void SetActiveGameObject(GameObject obj, Image img)
    {
        // Tắt tất cả các game object đang active.
        backItem.SetActive(false);
        arpet.SetActive(false);
        floor.SetActive(false);
        leftItem.SetActive(false);
        onFloor.SetActive(false);
        rightItem.SetActive(false);
        upItem.SetActive(false);
        wall.SetActive(false);

        backItem2.sprite = select;
        arpet2.sprite = select;
        floor2.sprite = select;
        leftItem2.sprite = select;
        onFloor2.sprite = select;
        rightItem2.sprite = select;
        upItem2.sprite = select;
        wall2.sprite = select;

        // Bật game object tương ứng.
        obj.SetActive(true);
        img.sprite = selected;
    }
}
