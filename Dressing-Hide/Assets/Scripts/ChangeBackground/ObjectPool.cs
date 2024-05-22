using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;

    }
    
    public bool setActive;

    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Pool> pools;
    public List<GameObject> poolObject;

    private void Start()
    {
        poolObject = new List<GameObject>();
        foreach (Pool pool in pools)
        {
            GameObject obj = Instantiate(pool.prefab, gameObject.transform);
            obj.SetActive(setActive);
            obj.transform.position = gameObject.transform.position;
            obj.transform.position = new Vector3(
                gameObject.transform.position.x + obj.GetComponent<backgroundObject>().position.x,
                gameObject.transform.position.y +obj.GetComponent<backgroundObject>().position.y,
                gameObject.transform.position.z+ obj.GetComponent<backgroundObject>().position.z);
            obj.transform.rotation = Quaternion.identity;
            poolObject.Add(obj);
        }

    }



}
