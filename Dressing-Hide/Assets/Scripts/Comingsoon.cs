using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comingsoon : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameController.timer = 60;
    }

    public void Main()
    {
        LoadingSceneController.Instance.GotoScene("MainUI");
    }
}
