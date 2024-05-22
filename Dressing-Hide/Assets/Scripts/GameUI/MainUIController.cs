using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public static MainUIController Instance;    

    [SerializeField]
    GameObject MenuItem;
    [SerializeField]
    GameObject level_grid;
    [SerializeField]
    GameObject HIW_level_grid;
    [SerializeField]
    TextMeshProUGUI level_selected;

    [SerializeField]
    public Slider music_slider;
    [SerializeField]
    public Slider soundfx_slider;


    [SerializeField]
    Button Decorate;
    [SerializeField]
    Button bonusMap;


    [SerializeField]
    TextMeshProUGUI bonus_mapText;
    [SerializeField]
    TextMeshProUGUI decorBtnText;
    public GameObject Wheel_redtic;
    public GameObject Daily_redtic;

    [SerializeField]
    GameObject Daily_Btn;
    [SerializeField]
    GameObject Daily_Btn_X2;

    [SerializeField]
    GameObject wheel;
    [SerializeField]
    GameObject Tutorial;
    [SerializeField]
    GameObject DecorScene;
    [SerializeField]
    GameObject dailyGift;



    public static float CountDownAdsWheel;
    public void LoadGameScene()
    {
        GameManager.Instance.current_level =  GetHighestLevel()-1;
        ////AdManager.Instance.ShowInterstitialAd("SelectLevel");
        if(GameManager.Instance.current_level == 0)
        {
            TutorialController.Instance.LoadListTutorial("Level Normal");
        }
        AudioManager.instance.playSFX("Click");
        //LoadingSceneController.Instance.GotoScene("Gameplay"); 
        LoadingSceneController.Instance.GotoScene("Gameplay");
        //StartCoroutine(LoadingSceneController.Instance.LoadSceneAsync("Gameplay"));
    }

    public void Click()
    {
        AudioManager.instance.playSFX("Click");
    }

    public void SelectLevel()
    {
        AudioManager.instance.playSFX("Click");
        ////AdManager.Instance.ShowInterstitialAd("SelectLevel");

        MenuItem.SetActive(false);
        level_grid.SetActive(true);
    }

    public void HIWSelectLevel()
    {
        AudioManager.instance.playSFX("Click");

        ////AdManager.Instance.ShowInterstitialAd("SelectLevel");
        MenuItem.SetActive(false);
        HIW_level_grid.SetActive(true);
    }

    public void EditBackGroundTutorial()
    {
        MenuItem.SetActive(false);
        DecorScene.SetActive(true);
        Tutorial.SetActive(true);
    }

    public void EditBackGround()
    {
        MenuItem.SetActive(false);
        DecorScene.SetActive(true);
        //Tutorial.SetActive(true);
    }

    public void Wheel()
    {
        TutorialController.Instance.LoadListTutorial("Wheel");
    }

    public void HIWBackSelectLevel()
    {
        AudioManager.instance.playSFX("Click");
        MenuItem.SetActive(true);
        HIW_level_grid.SetActive(false);
    }

    public void BackSelectLevel()
    {
        AudioManager.instance.playSFX("Click");
        MenuItem.SetActive(true);
        level_grid.SetActive(false);
    }


    public void Gift()
    {
        //AdManager.Instance.ShowRewardedAd(GetGift, "GetGift");

    }

    void GetGift(int i)
    {

    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        //AdManager.Instance.ShowBanner();

        StartCoroutine(WheelReset());

        if (PlayerPrefs.GetInt("level" +4,0) == 1)
        {
            bonusMap.interactable = true;
            bonus_mapText.text = "Bonus";
        }

        if (PlayerPrefs.GetInt("level" + 6, 0) == 1)
        {
            Decorate.transform.GetChild(3).GetComponent<Image>().color = Color.white;
            Decorate.transform.GetChild(1).gameObject.SetActive(true);
            Decorate.interactable = true;
            decorBtnText.text = "Decorate";
        }

        PlayerPrefs.SetInt("money",GameManager.Instance.money);
        if (!GameManager.Instance.isReload)
        {
            LoadingSceneController.LoadingTime = 2f;
            LoadingSceneController.Instance.IgnoreLoading();
            GameManager.Instance.isReload = true;
        }
        else
        {
            //dailyGift.SetActive(dailyGift.GetComponent<DailyGift>().isShow());
            Daily_redtic.SetActive(dailyGift.GetComponent<DailyGift>().isShow());
            Daily_Btn.SetActive(dailyGift.GetComponent<DailyGift>().isShow());
            Daily_Btn_X2.SetActive(dailyGift.GetComponent<DailyGift>().isShowAds());
            LoadingSceneController.LoadingTime = 1f;    
        }

        Time.timeScale = 1;
        StopAllSoudn();
        level_selected.text = "Level " + GetHighestLevel().ToString();
        AudioManager.instance.playMainMusic();
        music_slider.value = AudioManager.instance.musicSource.volume;
        music_slider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);

        soundfx_slider.value = AudioManager.instance.SoundfxSource.volume;
        soundfx_slider.onValueChanged.AddListener(OnSoundfxVolumeSliderChanged);
    }

    IEnumerator WheelReset()
    {
        wheel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        wheel.SetActive(false);
    }

    public void StopAllSoudn()
    {
        AudioManager.instance.playSFX("Click");
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.SoundfxSource.Stop();
    }

    int GetHighestLevel()
    {
        for (int i = 2; i <= GameManager.Instance.Levels.Count; i++)
        {
            if (PlayerPrefs.GetInt("level" + i, 0) == 0)
            {
                return i - 1;
            }
        }
        return GameManager.Instance.Levels.Count;
    }

    private void OnMusicVolumeSliderChanged(float arg0)
    {
        AudioManager.instance.musicSource.volume = arg0;
    }

    private void OnSoundfxVolumeSliderChanged(float arg0)
    {
        AudioManager.instance.SoundfxSource.volume = arg0;
    }

    private void OnDestroy()
    {
        CountDownAdsWheel = 0;
    }
}
