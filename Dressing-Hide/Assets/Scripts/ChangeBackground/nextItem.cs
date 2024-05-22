using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextItem : MonoBehaviour
{
    [SerializeField]
    public int id;

    public static nextItem instance;
    [SerializeField]
    public typeObject type;

    private void Start()
    {
        
    }

    private void Update()
    {
        GameObject obj = GameObject.FindWithTag("ListItemManager");

        switch (type)
        {
            case typeObject.backItem:
                id = obj.GetComponent<ListItemManager>().backItemId;
                break;
            case typeObject.arpet:
                id = obj.GetComponent<ListItemManager>().carpetId;
                break;
            case typeObject.floor:
                id = obj.GetComponent<ListItemManager>().floorId;
                break;
            case typeObject.leftItem:
                id = obj.GetComponent<ListItemManager>().leftItemId;
                break;
            case typeObject.onFloor:
                id = obj.GetComponent<ListItemManager>().onFloorId;
                break;
            case typeObject.rightItem:
                id = obj.GetComponent<ListItemManager>().rightItemId;
                break;
            case typeObject.upItem:
                id = obj.GetComponent<ListItemManager>().upItemId;
                break;
            case typeObject.wall:
                id = obj.GetComponent<ListItemManager>().wallId;
                break;

            default:
                break;
        }

        //Transform[] children = GetComponentsInChildren<Transform>();
        ObjectPool objectpool = gameObject.GetComponent<ObjectPool>();


        foreach (var chil in objectpool.poolObject)
        {

            if (chil.gameObject.GetComponent<backgroundObject>().id == id)
            {
                chil.gameObject.SetActive(true);
            }
            else
            {
                chil.gameObject.SetActive(false);
            }
        }

    }


}
