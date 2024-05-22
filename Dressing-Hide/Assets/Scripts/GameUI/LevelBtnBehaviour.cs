using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBtnBehaviour : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Button Button;
    [SerializeField]
    GameObject locked;

    private int index;

    void Start()
    {
        index = int.Parse(text.text);

        if (PlayerPrefs.GetInt("level" + (index)) == 0)
        {
            Button.interactable = false;
            locked.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("level" + (index)) == 1)
        {
            Button.interactable = true;
            locked.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
        }
        if (index == 1)
        {
            Button.interactable = true;
            locked.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
        }
    }

    public void GotoLevel()
    {
        //AdManager.Instance.ShowInterstitialAd("SelectLevel");

        GameManager.Instance.current_level = index-1;
        if (GameManager.Instance.current_level == 1)
        {
            TutorialController.Instance.LoadListTutorial("Level Normal");
        }
        LoadingSceneController.Instance.GotoScene("Gameplay");
    }
}
