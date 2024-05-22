using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ObjectPool;

public class BuyProperties : MonoBehaviour
{
    [SerializeField]
    public int price;
    
    private bool canBuy;
    [SerializeField]
    public statusItem statusItem= statusItem.needBuy;

    [SerializeField]
    public Sprite select;
    [SerializeField]
    public Sprite selected;

    private void Start()
    {
        setPriceText();
    }

    private void Update()
    {
        updateStatusBtn();
    }

    void updateStatusBtn()
    {
        Transform buttonManager = transform.Find("ButtonManager");
        Transform buttonBuy = buttonManager.Find("ButtonSelect");
        Image buttonSprite = buttonBuy.GetComponent<Image>();
        if (statusItem == statusItem.selected)
        {
            buttonSprite.sprite = selected; 
        }
        if (statusItem == statusItem.own)
        {
            buttonSprite.sprite = select;
        }
    }


    public bool checkOwn()
    {
        int id = gameObject.GetComponent<backgroundObject>().id;
        typeObject type = gameObject.GetComponent<backgroundObject>().type;
        foreach (Pool pool in ObjectPoolForList.instance.pools)
        {
            if (pool.prefab.GetComponent<backgroundObject>().type == type && pool.prefab.GetComponent<backgroundObject>().id == id)
            {
                if (pool.prefab.GetComponent<BuyProperties>().statusItem == statusItem.own)
                {
                    return true;
                };
            }
        }
        return false;
    }
    void setPriceText()
    {

        Transform buttonManager = transform.Find("ButtonManager");
        Transform buttonBuy = buttonManager.Find("ButtonBuy");
        Transform priceTransform = buttonBuy.Find("price");
        if (price >= 0)
        {
            priceTransform.gameObject.GetComponent<TextMeshProUGUI>().text = price.ToString();
        }
        else
        {
            buttonBuy.GetChild(0).gameObject.SetActive(false);
            buttonBuy.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void buy()
    {
        GameObject listItemManager = GameObject.Find("ListItemManager");
        CanBuy();
        if (canBuy == true)
        {
            if (price > 0)
            {
                statusItem = statusItem.own;
                GameManager.Instance.money -= price;
                PlayerPrefs.SetInt("money", GameManager.Instance.money);
                SetStatusForObjectPool(1);
                listItemManager.GetComponent<ListItemManager>().SaveData();
            }
            if(price < 0)
            {
                //if(AdManager.Instance.ShowRewardedAd(SetStatusForObjectPool, "DecorAds")){
                    
                    statusItem = statusItem.own;
                    SetStatusForObjectPool(1);
                    listItemManager.GetComponent<ListItemManager>().SaveData();
                //}
            }
        }
        else
        {
            Message.Instance.ShowMessage("Not enough money",transform.position);
        }
    }


    public void SetStatusForObjectPool(int i)
    {
        int id = gameObject.GetComponent<backgroundObject>().id;
        typeObject type =  gameObject.GetComponent<backgroundObject>().type;
        foreach (Pool pool in ObjectPoolForList.instance.pools)
        {
            if (pool.prefab.GetComponent<backgroundObject>().type==type && pool.prefab.GetComponent<backgroundObject>().id== id)
            {
                pool.prefab.GetComponent<BuyProperties>().statusItem = statusItem.own;
            }
        }
    }

    public void CanBuy()
    {
        if (GameManager.Instance.money>=price)
        {
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }
    }
}
