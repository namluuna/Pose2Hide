using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    private List<GameObject> childObjects = new List<GameObject>();

    void Start()
    {
        // Lấy danh sách các GameObject con
        foreach (Transform child in transform)
        {
            childObjects.Add(child.gameObject);
        }

        // Kích hoạt một GameObject ngẫu nhiên
        ActivateRandomChildObject();
    }

    void ActivateRandomChildObject()
    {
        if (childObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, childObjects.Count); // Chọn ngẫu nhiên một chỉ số
            GameObject randomChild = childObjects[randomIndex]; // GameObject được chọn

            randomChild.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No child objects to activate.");
        }
    }
}
