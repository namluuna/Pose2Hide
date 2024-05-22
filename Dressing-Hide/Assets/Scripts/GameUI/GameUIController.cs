using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    private static GameUIController instance;
    [Header("Mini Scene")]
    [SerializeField]
    private GameObject victory_scene;
    [SerializeField]
    private GameObject lose_scene;
    [SerializeField]
    private GameObject pause_scene;
    [Header("Some refs")]
    [SerializeField]
    private Camera main_camera;
    [SerializeField]
    public TextMeshProUGUI Timer;
    [SerializeField]
    public Slider music_slider;
    [SerializeField]
    public Slider soundfx_slider;
    [SerializeField]
    private GameObject Time_count_down;
    [SerializeField]
    private TextMeshProUGUI level;
    [SerializeField]
    private GameObject tick_win;
    [SerializeField]
    private GameObject tick_lose;
    [SerializeField]
    public TextMeshProUGUI hint;
    [SerializeField]
    public GameObject adsHint;
    [SerializeField]
    public TextMeshProUGUI MainButtonText1;
    [SerializeField]
    public TextMeshProUGUI MainButtonText2;
    [SerializeField]
    public Button MainButton;
    [SerializeField]
    public Button MainButton2;

    [SerializeField]
    public GameObject AppReview;

    [SerializeField]
    public GameObject Firework1;
    [SerializeField]
    public GameObject Firework2;
    [SerializeField]
    public GameObject HintBtn;
    [Header("Transform position")]
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject answer;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private TextMeshProUGUI won_money;
    [SerializeField]
    private TextMeshProUGUI extra_money;
    [SerializeField]
    private GameObject firework;
    [SerializeField]
    private GameObject firework_1;

    [Header("ProcessBar")]
    [SerializeField]
    private GameObject current_nut;
    [SerializeField]
    private GameObject remain_nut;
    [SerializeField]
    private Transform process_bar;
    [SerializeField]
    private GameObject Pop_Up_HIW;
    [SerializeField]
    private GameObject Pop_Up_Decor;
    [SerializeField]
    private GameObject giftPopup;
    [SerializeField]
    private GameObject container_process_bar;
    [SerializeField]
    private Button giftbox;
    [SerializeField]
    private Button extraMoney;
    [Header("Other")]

    public BossController cops;
    public GameObject tutorial;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        if (GameManager.Instance.current_level + 1 != GetHighestLevel())
        {
            won_money.text = "+ " + GameManager.Instance.Levels[GameManager.Instance.current_level].money/2;
            extra_money.text = "" + (GameManager.Instance.Levels[GameManager.Instance.current_level].money * 2);
        }
        else
        {
            won_money.text = "+ " + GameManager.Instance.Levels[GameManager.Instance.current_level].money;
            extra_money.text = "" + (GameManager.Instance.Levels[GameManager.Instance.current_level].money * 4);
        }

        if (PlayerPrefs.GetInt("level" + 6, 0) == 1)
        {
            MainButton.onClick.AddListener(GotoDecorScene);
            MainButton2.onClick.AddListener(GotoDecorScene);

            MainButtonText1.text = "Decorate";
            MainButtonText2.text = "Decorate";
        }
        cops = GameController.Boss.GetComponent<BossController>();
        if (GameManager.Instance.current_level == 0 && PlayerPrefs.GetInt("isTutorialGameplay", 0) == 0)
        {

            tutorial.gameObject.SetActive(true);
            //TutorialController.Instance.LoadListTutorial("Level Normal");
        }
        if (GameManager.Instance.current_level >5 && GameManager.Instance.current_level < 10 )
        {
            //TutorialController.Instance.LoadListTutorial("Level HideFromWife"); 
        }
        music_slider.value = AudioManager.instance.musicSource.volume;
        music_slider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);

        soundfx_slider.value = AudioManager.instance.SoundfxSource.volume;
        soundfx_slider.onValueChanged.AddListener(OnSoundfxVolumeSliderChanged);

        level.text = "Level " + (GameManager.Instance.current_level+1).ToString();
    }

    private void Update()
    {
        hint.text =  + GameManager.Instance.hint+"";
        if (GameManager.Instance.hint == 0)
        {
            hint.gameObject.SetActive(false);  
            
            adsHint.SetActive(true);
        }
        else
        {
            hint.gameObject.SetActive(true);
            adsHint.SetActive(false);
        }
    }

    private void OnMusicVolumeSliderChanged(float arg0)
    {
        AudioManager.instance.musicSource.volume = arg0;
    }

    private void OnSoundfxVolumeSliderChanged(float arg0)
    {
        AudioManager.instance.SoundfxSource.volume = arg0;
    }

    public static GameUIController Instance
    {
        get { return instance; }
    }

    public void AdsEarnMore()
    {
        AudioManager.instance.playSFX("Click");
        //AdManager.Instance.ShowRewardedAd(ExtraMoney, "BonusMoney");
        ExtraMoney(1);
    }

    void ExtraMoney(int i)
    {
        won_money.text = "+ " + (GameManager.Instance.Levels[GameManager.Instance.current_level].money * 5).ToString();
        GameManager.Instance.money += GameManager.Instance.Levels[GameManager.Instance.current_level].money * 4;
        GameManager.Instance.Save();
    }

    public void GetGift()
    {
        //AdManager.Instance.ShowRewardedAd(BonusGift, "GetGift");
        BonusGift(0);
        AudioManager.instance.playSFX("Click");
        GameManager.Instance.Save();
    }
    void BonusGift(int i)
    {
        giftPopup.gameObject.SetActive(true);
        int indexGiftRandom = 0;
        float randomValue = UnityEngine.Random.value; // Generate a random value between 0.0 and 1.0
        
        Debug.Log(giftPopup.transform.GetChild(3).GetChild(0).gameObject.name);
        //int randomNumber;
        if (randomValue < 0.15f)
        {
            //money 15%
            
            giftPopup.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            indexGiftRandom = 8; GameManager.Instance.money += 200;

        }
        else if (randomValue >= 0.15f && randomValue < 0.45f)
        {
            giftPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            //hint 30%
            indexGiftRandom = 7; GameManager.Instance.hint += 1;
        }
        else if (randomValue >= 0.45f && randomValue < 0.55f)
        {
            //money 10%
            indexGiftRandom = 6; GameManager.Instance.money += 500;
            giftPopup.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
        }
        else if (randomValue >= 0.55f && randomValue < 0.60f)
        {
            giftPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            //item 5%
            indexGiftRandom = 5; GameManager.Instance.hint += 1;
        }
        else if (randomValue >= 0.60f && randomValue < 0.80f)
        {
            giftPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            //hint 20%
            indexGiftRandom = 4; GameManager.Instance.hint += 2;
        }
        else if (randomValue >= 0.80f && randomValue < 0.85f)
        {
            //money 5%
            indexGiftRandom = 3; GameManager.Instance.money += 1000; 
            giftPopup.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
        }
        else if (randomValue >= 0.85f && randomValue < 0.95f)
        {
            giftPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            //hint 10%
            indexGiftRandom = 2; GameManager.Instance.hint += 3;
        }
        else if (randomValue >= 0.95f && randomValue <= 1f)
        {
            giftPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            //item 5%
            indexGiftRandom = 1; GameManager.Instance.hint += 3;
        }

    }

    public void UnlockHIWLevels()
    {
        AudioManager.instance.playSFX("Click");
        GameManager.Instance.HIW_current_level = HIWGetHighestLevelUnlock()-1;
        //AdManager.Instance.ShowRewardedAd(UnlockHIWLevel, "UnlockLevel");
        UnlockHIWLevel(1);
        //LoadingSceneController.Instance.GotoScene("HIWGameplay");
    }

    void UnlockHIWLevel(int i)
    {
        LoadingSceneController.Instance.GotoScene("HIWGameplay");
    }

    public void GotoDecorScene()
    {
        GameManager.Instance.gotoDecorScene();
    }
    public void GotoDecorSceneTutorial()
    {
        GameManager.Instance.gotoDecorSceneTotorial();
    }

    public void Victory()
    {

        GameController.end_game = true;
        GameController.isColliding = false;
        AudioManager.instance.musicSource.Stop();
        tick_win.SetActive(true);
        AudioManager.instance.playSFX("Win");
        StartCoroutine(MoveWin());

    }

    public void Lose()
    {
        GameController.end_game = true;
        GameController.isColliding = false;
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.playSFX("Lose");
        StartCoroutine(MoveLose());
    }

    public void Pause()
    {
        AudioManager.instance.playSFX("Click");
        pause_scene.SetActive(!pause_scene.activeInHierarchy);
        if(pause_scene.activeInHierarchy == false)
        {
            GameController.gamePlayState = GamePlayState.PROCESS;
            AudioManager.instance.musicSource.UnPause();
            AudioManager.instance.SoundfxSource.UnPause();
            Time.timeScale = 1.0f;
        }
        else
        {
            GameController.gamePlayState = GamePlayState.PAUSE;
            AudioManager.instance.musicSource.Pause();
            AudioManager.instance.SoundfxSource.Pause();
            Time.timeScale = 0;
        }
    }

    public void ContinuePlay()
    {
        ContinueAds(1);
        //if(AdManager.Instance.ShowRewardedAd(ContinueAds, "MoreTime"))
        //{

        //}
    }

    public void ContinueAds(int i)
    {
        main_camera.orthographicSize = 38.05f;
        main_camera.transform.position = new Vector3(0, 0, main_camera.transform.position.z);
        HintBtn.GetComponent<Button>().interactable = true;
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.SoundfxSource.Stop();
        AudioManager.instance.playSFX("Click");
        GameController.timer = 12;
        GameController.end_game = false;
        GameController.gamePlayState = GamePlayState.PROCESS;
        cops.transform.position = new Vector3(cops.transform.position.x + 25f , cops.transform.position.y, cops.transform.position.z);
        character.SetActive(true);
        answer.SetActive(true);
        background.SetActive(true);
        Time_count_down.SetActive(true);
        lose_scene.gameObject.SetActive(false);
        Pop_Up_HIW.gameObject.SetActive(false);
        foreach (Transform child in process_bar)
        {
            Destroy(child.gameObject);
        }
        container_process_bar.gameObject.SetActive(false);
        Pop_Up_Decor.SetActive(false);
        giftbox.gameObject.SetActive(false);
        AudioManager.instance.playMusic();
    }

    public void WinMainScene()
    {
        GameController.end_game = false;
        StopAllSoudn();
        Time.timeScale = 1;
        GameManager.Instance.current_level++;
        PlayerPrefs.SetInt("CurrentLevel", GameManager.Instance.current_level);
        //////AdManager.Instance.ShowInterstitialAd("BackHome");
        //////AdManager.Instance.ShowInterstitialAd("BackHome");
        Destroy(tick_win);
        AudioManager.instance.playSFX("Click");
        LoadingSceneController.Instance.GotoScene("MainUI");
    }

    public void LoadMainScene()
    {
        GameController.end_game = false;
        Time.timeScale = 1;
        StopAllSoudn();
        AudioManager.instance.playSFX("Click");
        //////AdManager.Instance.ShowInterstitialAd("BackHome");
        //////AdManager.Instance.ShowInterstitialAd("BackHome");
        Destroy(tick_win);
        LoadingSceneController.Instance.GotoScene("MainUI");
    }

    public void Replay()
    {
        GameController.end_game = false;
        Time.timeScale = 1f;
        StopAllSoudn();
        AudioManager.instance.playSFX("Click");
        //////AdManager.Instance.ShowInterstitialAd("Replay");
        LoadingSceneController.Instance.GotoScene("Gameplay");
    }

    public void NextLevel()
    {
        GameController.end_game = false;
        Time.timeScale = 1f;
        AudioManager.instance.playSFX("Click");
        GameManager.Instance.current_level++;
        PlayerPrefs.SetInt("CurrentLevel", GameManager.Instance.current_level);
        ////AdManager.Instance.ShowInterstitialAd("NextLevel"); 
        LoadingSceneController.Instance.GotoScene("Gameplay");
    }

    private IEnumerator MoveWin()
    {
        tick_win.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        tick_win.SetActive(true);
        firework.SetActive(true);
        HintBtn.GetComponent<Button>().interactable = false;
        GameController.instance.ForwardObject();
        PlayAnimationBus();
        iTween.ScaleTo(tick_win, iTween.Hash(
            "scale", Vector3.one,
            "easetype", iTween.EaseType.linear
        ));

        cops.SetCharacterState("Move");
        AudioManager.instance.playSFX("WalkWin");
        iTween.MoveTo(cops.gameObject, iTween.Hash(
            "x", cops.transform.position.x - 25f,
            "time", 3f,
            "easetype", iTween.EaseType.linear
        ));
        yield return new WaitForSeconds(3);
        AudioManager.instance.SoundfxSource.Stop();
        cops.SetCharacterState("Wonder");

        yield return new WaitForSeconds(2.5f);
        cops.SetCharacterState("Move");
        firework_1.SetActive(true);
        AudioManager.instance.playSFX("WalkWin");
        iTween.MoveBy(cops.gameObject, iTween.Hash(
           "x", -70f,
           "time", 3f,
           "easetype", iTween.EaseType.linear
       ));
        yield return new WaitForSeconds(3);
        PlayAnimationWinGame2();
        Time_count_down.SetActive(false);
        tick_win.SetActive(false);
        OnProcessBar(true);
        AudioManager.instance.SoundfxSource.Stop(); 
        victory_scene.gameObject.SetActive(true);
        StartCoroutine(HideInSecond(victory_scene.transform.GetChild(3).gameObject, 3));
        StartCoroutine(HideInSecond(victory_scene.transform.GetChild(6).gameObject,3));
        firework.SetActive(true);
        AudioManager.instance.playSFX("WinScene");
        if (GameManager.Instance.current_level + 1 != GetHighestLevel())
        {
            GameManager.Instance.money += GameManager.Instance.Levels[GameManager.Instance.current_level].money;
        }
        else
        {
            GameManager.Instance.money += GameManager.Instance.Levels[GameManager.Instance.current_level].money/2;
        }

        PlayerPrefs.SetInt("level" + (GameManager.Instance.current_level+2),1);

    }

    public void Click()
    {
        AudioManager.instance.playSFX("Click");
    }

    private IEnumerator MoveLose()
    {
        tick_lose.transform.localScale = new Vector3(0.5f,0.5f,1);
        tick_lose.SetActive(true);
        HintBtn.GetComponent<Button>().interactable = false;

        iTween.ScaleTo(tick_lose, iTween.Hash(
            "scale", Vector3.one,
            "easetype", iTween.EaseType.linear 
        ));
        cops.SetCharacterState("Move");
        iTween.MoveTo(cops.gameObject, iTween.Hash(
            "x", cops.transform.position.x - 25f,
            "time", 3f,
            "easetype", iTween.EaseType.linear
        ));
        yield return new WaitForSeconds(3);
        cops.SetCharacterState("Stop");
        yield return new WaitForSeconds(1f);

        AudioManager.instance.SoundfxSource.Stop();
        cops.SetCharacterState("Point");
        yield return new WaitForSeconds(1f);
        character.SetActive(false);
        answer.SetActive(false);    
        background.SetActive(false);    
        Time_count_down.SetActive(false);
        lose_scene.gameObject.SetActive(true);
        StartCoroutine(HideInSecond(lose_scene.transform.GetChild(2).gameObject,3));
        StartCoroutine(HideInSecond(lose_scene.transform.GetChild(4).gameObject, 3));

        AudioManager.instance.playSFX("LoseScene");
        tick_lose.SetActive(false);
        OnProcessBar(false);
        main_camera.orthographicSize = 30;
        iTween.MoveTo(main_camera.gameObject, iTween.Hash(
            "position", new Vector3(main_camera.transform.position.x+4, -20f, main_camera.transform.position.z),
            "time", 1f,
            "easetype", iTween.EaseType.linear
        ));
        yield return new WaitForSeconds(1f);
    }

    public void OnProcessBar(bool check)
    {
        if (GameManager.Instance.current_level == 4 && GetHighestLevel() == 5)
        {
            Pop_Up_Decor.SetActive(true);
        }

        if(GetHighestLevel() % 3 == 0 && check && GetHighestLevel()-1 == GameManager.Instance.current_level)
        {
            PlayerPrefs.SetInt("levelHIW" + (HIWGetHighestLevel()+1), 1);
            //if(PlayerPrefs.GetInt("UnlockedlevelHIW" + (HIWGetHighestLevelUnlock()-1)) == 1)
            //{
            //    PlayerPrefs.SetInt("UnlockedlevelHIW" + (HIWGetHighestLevelUnlock()), 1);
            //}

            Pop_Up_HIW.gameObject.SetActive(true);
            StartCoroutine(HideInSecond(Pop_Up_HIW.transform.GetChild(3).gameObject, 3f));
        }

        if (PlayerPrefs.GetInt("level" + 6, 0) == 1)
        {
            container_process_bar.SetActive(true);
        }
        else
        {
            container_process_bar.SetActive(false);
        }

        if (PlayerPrefs.GetInt("level" + 4, 0) == 1 && PlayerPrefs.GetFloat("isReview", 0) == 0)
        {
            //Instantiate(AppReview);
        }

        for (int i=1; i <= 5; i++)
        {
            if(i <= GetHighestLevel() % 5 || GetHighestLevel() % 5 == 0)
            {
                Instantiate(current_nut, process_bar);
            }
            else
            {
                Instantiate(remain_nut, process_bar);
            }
        }   

         if(GetHighestLevel() % 5 == 0)
        {
            giftbox.gameObject.SetActive(true);
        }
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
        return 1;
    }

    IEnumerator HideInSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(true);
    }

    int HIWGetHighestLevel()
    {
        for (int i = 1; i <= GameManager.Instance.Levels.Count; i++)
        {
            if (PlayerPrefs.GetInt("levelHIW" + i, 0) == 0)
            {
                return i - 1;
            }
        }
        return 0;
    }

    int HIWGetHighestLevelUnlock()
    {
        for (int i = 1; i < 32; i++)
        {
            if (PlayerPrefs.GetInt("UnlockedlevelHIW" + i, 0) == 0)
            {
                if (PlayerPrefs.GetInt("levelHIW" + i, 0) == 1)
                {
                    return i;
                }
                else
                {
                    return i - 1;
                }
            }
        }
        return 1;
    }

    public void GotoHIWScene()
    {
            AudioManager.instance.playSFX("Click");
            LoadingSceneController.Instance.GotoScene("HIWGameplay");
    }

    public void HideHIWScene()
    {
        AudioManager.instance.playSFX("Click");
        Pop_Up_HIW.gameObject.SetActive(false); 
    }

    public void PlayAnimationBus()
    {
        Animation animation = GameController.MapContain.GetComponent<Animation>();
        animation.Play("Close_Door");
        AudioManager.instance.playSFX("CloseDoor");
    }

    public void PlayAnimationWinGame2()
    {

        Animation animation = GameController.MapContain.GetComponent<Animation>();
        animation.Play("Open_Door");
        AudioManager.instance.playSFX("CloseDoor");
    }

    public void StopAllSoudn() {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.SoundfxSource.Stop();
    }
}
