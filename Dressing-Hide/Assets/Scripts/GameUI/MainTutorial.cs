using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainTutorial : MonoBehaviour
{
    [SerializeField]
    Button Wall;
    Button Buy;
    Button Select;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animation>().Play("1st");
        Wall.onClick.AddListener(ClickedWall);
    }

    void ClickedWall()
    {
        GetComponent<Animation>().Play("2nd");
        Buy = GameObject.Find("MainUI").transform.GetChild(3).GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(1).GetComponent<Button>();
        Buy.onClick.AddListener(ClickedBuy);
    }

    void ClickedBuy()
    {
        GetComponent<Animation>().Play("3rd");
        Select = GameObject.Find("MainUI").transform.GetChild(3).GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<Button>();
        Select.onClick.AddListener(EndTutorial);
    }

    void EndTutorial()
    {
        gameObject.SetActive(false);    
    }


}
