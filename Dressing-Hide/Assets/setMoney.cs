using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class setMoney : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("money").ToString() ;
    }
}
