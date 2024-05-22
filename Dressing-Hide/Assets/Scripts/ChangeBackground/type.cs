using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class type : MonoBehaviour
{
    [SerializeField]
    public typeObject Type;

    public static type instance;


    public void setType()
    {
        ObjectPoolForList.instance.Type = Type;

    }
}
