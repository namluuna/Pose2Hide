using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInstance : MonoBehaviour
{
    public static PopUpInstance instance;

    public GameObject PopUpPrefab;  

    public TextMeshProUGUI amount;
    
    public GameObject moneyPrefab;
    public GameObject hintPrefab;
    public GameObject imagePrefab;

    private void Awake()
    {
        instance = this;
    }


}
