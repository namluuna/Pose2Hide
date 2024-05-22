using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListClotherController : MonoBehaviour
{

    public List<ClotherData> list;
    public SpawnClother basePrefab;
    void Start()
    {
        foreach(ClotherData i in list)
        {
            SpawnClother clother = Instantiate(basePrefab,transform);
            clother.SettingLevel(i);
            clother.gameObject.name = i.name;   
        }
    }

}
