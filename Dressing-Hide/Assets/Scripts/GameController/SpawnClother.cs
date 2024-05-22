using Spine.Unity.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnClother : MonoBehaviour
{
    
    public GameObject DragObject;



    public void SettingLevel(ClotherData data)
    {
        DragObject.GetComponent<ClotherController>().SetDataSkin(data); 
    }
}
