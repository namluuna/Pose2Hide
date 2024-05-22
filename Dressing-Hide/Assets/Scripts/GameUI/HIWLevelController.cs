using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HIWLevelController : MonoBehaviour
{
    public List<GameObject> Levels = new List<GameObject>();

    [SerializeField]
    private GameObject prefabs;

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject gameObject = Instantiate(prefabs, transform);
            gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i).ToString();
            Levels.Add(gameObject); 
        }
    }
}
