using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum statusItem{
    needBuy,
    own,
    selected
}

public class buttonManager : MonoBehaviour
{
    [SerializeField]
    public statusItem statusItem;

    public GameObject buttonBuy;
    public GameObject buttonSelect;

    private void Start()
    {
        BuyProperties parentComponent = GetComponentInParent<BuyProperties>();
        statusItem = parentComponent.statusItem;
    }
    private void Update()
    {
        BuyProperties parentComponent = GetComponentInParent<BuyProperties>();
        statusItem = parentComponent.statusItem;
        Image buttonSprite = buttonBuy.GetComponent<Image>();
        if (statusItem == statusItem.needBuy)
        {
            buttonBuy.SetActive(true);
            buttonSelect.SetActive(false);
        }
        if (statusItem ==  statusItem.own)
        {
            buttonSelect.SetActive(true);
            buttonBuy.SetActive(false);
        }
        if (statusItem == statusItem.selected)
        {
            buttonSprite.sprite = parentComponent.selected;
            buttonSelect.SetActive(true);
            buttonBuy.SetActive(false);
        }else if (statusItem == statusItem.own)
        {
            buttonSprite.sprite = parentComponent.select;
        }
    }

}
