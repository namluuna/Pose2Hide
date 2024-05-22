using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<GameObject> Levels = new List<GameObject>();

    [SerializeField]
    private GameObject prefabs;

    public bool isHIW = false;
    private void Start()
    {
        if (!isHIW)
        {
            for (int i = 2; i <= GameManager.Instance.Levels.Count-1; i++)
            {
                GameObject gameObject = Instantiate(prefabs, transform);
                gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i).ToString();
                Levels.Add(gameObject);
            }
        }
        else
        {
            for (int i = 2; i <= GameManager.Instance.HIWLevels.Count; i++)
            {
                GameObject gameObject = Instantiate(prefabs, transform);
                gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i).ToString();
                Levels.Add(gameObject);
            }
        }
    }
}
