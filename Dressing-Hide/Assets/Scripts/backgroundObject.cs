using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum typeObject {
    backItem,
    arpet,
    floor,
    leftItem,
    onFloor,
    rightItem,
    upItem,
    wall
};


public class backgroundObject : MonoBehaviour
{
    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }

    [SerializeField]
    public typeObject type;
    [SerializeField]
    public int id;

   
    public Position  position;
    public static backgroundObject instance;
    public bool isOwn;
    private void Start()
    {
        
    }

    private void Update()
    {
        resetStatus();
    }
   
    void resetStatus()
    {
        BuyProperties buyProperties = GetComponent<BuyProperties>();

        GameObject obj = GameObject.FindWithTag("ListItemManager");
        try
        {
            switch (type)
            {
                case typeObject.backItem:
                    {
                        if (obj.GetComponent<ListItemManager>().backItemId != id)
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.own;
                                break;
                            }
                            else
                            {
                                buyProperties.statusItem = statusItem.needBuy;
                                break;
                            }
                        }
                        else
                        {
                            try
                            {
                                if (buyProperties.checkOwn())
                                {
                                    buyProperties.statusItem = statusItem.selected;
                                }
                            }
                            catch
                            {

                            }
                        }

                        break;
                    }
                case typeObject.arpet:
                    if (obj.GetComponent<ListItemManager>().carpetId != id)
                    {
                        if (buyProperties.checkOwn())
                        {
                            buyProperties.statusItem = statusItem.own;
                            break;
                        }
                        else
                        {
                            buyProperties.statusItem = statusItem.needBuy;
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.selected;
                            }
                        }
                        catch
                        {

                        }
                    }

                    break;
                case typeObject.floor:
                    if (obj.GetComponent<ListItemManager>().floorId != id)
                    {
                        if (buyProperties.checkOwn())
                        {
                            buyProperties.statusItem = statusItem.own;
                            break;
                        }
                        else
                        {
                            buyProperties.statusItem = statusItem.needBuy;
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.selected;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
                case typeObject.leftItem:
                    if (obj.GetComponent<ListItemManager>().leftItemId != id)
                    {
                        if (buyProperties.checkOwn())
                        {
                            buyProperties.statusItem = statusItem.own;
                            break;
                        }
                        else
                        {
                            buyProperties.statusItem = statusItem.needBuy;
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.selected;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
                case typeObject.onFloor:
                    if (obj.GetComponent<ListItemManager>().onFloorId != id)
                    {
                        if (buyProperties.checkOwn())
                        {
                            buyProperties.statusItem = statusItem.own;
                            break;
                        }
                        else
                        {
                            buyProperties.statusItem = statusItem.needBuy;
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.selected;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
                case typeObject.upItem:
                    if (obj.GetComponent<ListItemManager>().upItemId != id)
                    {
                        if (buyProperties.checkOwn())
                        {
                            buyProperties.statusItem = statusItem.own;
                            break;
                        }
                        else
                        {
                            buyProperties.statusItem = statusItem.needBuy;
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.selected;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
                case typeObject.wall:
                    if (obj.GetComponent<ListItemManager>().wallId != id)
                    {
                        if (buyProperties.checkOwn())
                        {
                            buyProperties.statusItem = statusItem.own;
                            break;
                        }
                        else
                        {
                            buyProperties.statusItem = statusItem.needBuy;
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.selected;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
                case typeObject.rightItem:
                    if (obj.GetComponent<ListItemManager>().rightItemId != id)
                    {
                        if (buyProperties.checkOwn())
                        {
                            buyProperties.statusItem = statusItem.own;
                            break;
                        }
                        else
                        {
                            buyProperties.statusItem = statusItem.needBuy;
                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (buyProperties.checkOwn())
                            {
                                buyProperties.statusItem = statusItem.selected;
                            }
                        }
                        catch
                        {

                        }
                    }
                    break;
                default:

                    break;
            }
        }
        catch
        {
            Debug.Log("");
        }
    }

    public void setIdToManager()
    {
        GameObject obj = GameObject.FindWithTag("ListItemManager");
        switch (type)
        {
            case typeObject.backItem:
                obj.GetComponent<ListItemManager>().backItemId = id;
                break;
            case typeObject.arpet:
                obj.GetComponent<ListItemManager>().carpetId = id;
                break;
            case typeObject.floor:
                obj.GetComponent<ListItemManager>().floorId = id;
                break;
            case typeObject.leftItem:
                obj.GetComponent<ListItemManager>().leftItemId = id;
                break;
            case typeObject.onFloor:
                obj.GetComponent<ListItemManager>().onFloorId = id;
                break;
            case typeObject.rightItem:
                obj.GetComponent<ListItemManager>().rightItemId = id;
                break;
            case typeObject.upItem:
                obj.GetComponent<ListItemManager>().upItemId = id;
                break;
            case typeObject.wall:
                obj.GetComponent<ListItemManager>().wallId = id;
                break;

            default:

                break;
        }
         ////AdManager.Instance.ShowInterstitialAd("EditBackground");

    }
}


