using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HIWLevelBtnBehaviour : MonoBehaviour
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
        if (index == 1 && PlayerPrefs.GetInt("levelHIW" + 1, 0) == 1)
        {
            PlayerPrefs.SetInt("UnlockedlevelHIW" + 0, 1);
        }
        if (PlayerPrefs.GetInt("UnlockedlevelHIW" + (index)) == 0)
        {
            Button.interactable = false;
            locked.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("UnlockedlevelHIW" + (index-1)) == 1 && PlayerPrefs.GetInt("levelHIW" + (index)) == 1 )
        {
            Button.interactable = true;
            locked.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
        }
    }

    public void GotoLevel()
    {
        //AdManager.Instance.ShowInterstitialAd("SelectLevel");

        GameManager.Instance.HIW_current_level = index-1;
        if (GameManager.Instance.HIW_current_level == 0)
        {
            TutorialController.Instance.LoadListTutorial("Level Bonus");
        }
        LoadingSceneController.Instance.GotoScene("HIWGameplay");
    }
}
