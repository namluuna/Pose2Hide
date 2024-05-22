using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class setText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void setTextForItem()
    {
        typeObject type = ObjectPoolForList.instance.Type;
        switch (type)
        {
            case typeObject.backItem:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Back Item";
                break;
            case typeObject.arpet:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Carpet";
                break;
            case typeObject.floor:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Floor";
                break;
            case typeObject.leftItem:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Left Item";
                break;
            case typeObject.onFloor:
                gameObject.GetComponent<TextMeshProUGUI>().text = "On Floor";
                break;
            case typeObject.rightItem:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Right Item";
                break;
            case typeObject.upItem:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Up Item";
                break;
            case typeObject.wall:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Wall";
                break;

            default:
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        setTextForItem();
    }
}
