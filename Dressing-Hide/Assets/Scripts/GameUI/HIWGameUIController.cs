
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HIWGameUIController : MonoBehaviour
{
    private static HIWGameUIController instance;
    [SerializeField]
    private GameObject victory_scene;
    [SerializeField]
    private GameObject lose_scene;
    [SerializeField]
    private GameObject pause_scene;
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
    public TextMeshProUGUI money;
    [SerializeField]
    public TextMeshProUGUI extra_money;

    [SerializeField]
    private Slider ProcessBar;
    [SerializeField]
    GridLayoutGroup ProcessContainer;
    [SerializeField]
    GameObject process_tick;
    [SerializeField]
    private GameObject NextLevelBtn;
    private float process;
    private float max_process;


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
        SetupProcessBar();
        
        if(PlayerPrefs.GetInt("levelHIW" + (GameManager.Instance.HIW_current_level + 2)) == 1)
        {
            NextLevelBtn.SetActive(true);
        }
        else
        {
            NextLevelBtn.SetActive(false);
        }

        money.text = "+ " + (GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].money).ToString();
        extra_money.text = "+ " + (GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].money * 4).ToString();
        

        music_slider.value = AudioManager.instance.musicSource.volume;
        music_slider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);

        soundfx_slider.value = AudioManager.instance.SoundfxSource.volume;
        soundfx_slider.onValueChanged.AddListener(OnSoundfxVolumeSliderChanged);

        level.text = "Level " + (GameManager.Instance.HIW_current_level+1).ToString();
        Debug.Log("Unlock1 " + PlayerPrefs.GetInt("levelHIW" + 1));
        Debug.Log("Unlock2 " + PlayerPrefs.GetInt("levelHIW" + 2));

        Debug.Log("Unlock"+ GameManager.Instance.HIW_current_level + 2 + "__"+PlayerPrefs.GetInt("levelHIW" + GameManager.Instance.HIW_current_level + 2));
    }

    private void Update()
    {
        if (!HIWGameController.isEndGame)
        {
            process += Time.deltaTime;
            ProcessBar.value = process / max_process;
            if (HIWGameController.current_phase >= 1){
                ProcessContainer.transform
                    .GetChild(HIWGameController.current_phase-1)
                    .GetChild(1).gameObject.SetActive(true);
            }
        }
        if (HIWGameController.isEndGame)
        {
            ProcessBar.gameObject.SetActive(false);
        }
    }

    void SetupProcessBar()
    {
        ListWall wall_sprites = GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].Walls;
        ProcessContainer.spacing = new Vector2(600/wall_sprites.Anime.Count - ProcessContainer.cellSize.x, 0)  ;
        ProcessContainer.padding.left = 600 / wall_sprites.Anime.Count;
        foreach (var anime in wall_sprites.Anime) {
            process += anime;
            Instantiate(process_tick,ProcessContainer.transform);
        }
        max_process = process;
        process = 0;
    }

    private void OnMusicVolumeSliderChanged(float arg0)
    {
        AudioManager.instance.musicSource.volume = arg0;
    }

    private void OnSoundfxVolumeSliderChanged(float arg0)
    {
        AudioManager.instance.SoundfxSource.volume = arg0;
    }

    public static HIWGameUIController Instance
    {
        get { return instance; }
    }

    public void Victory()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.playSFX("Win");
        victory_scene.gameObject.SetActive(true);
        GameManager.Instance.money += GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].money;
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,-25, Camera.main.transform.position.z);
        //if (PlayerPrefs.GetInt("levelHIW" + (GameManager.Instance.HIW_current_level + 2)) == 1)
        //{
        //    PlayerPrefs.SetInt("UnlockedlevelHIW" + (GameManager.Instance.HIW_current_level + 2), 1);
        //}
        PlayerPrefs.SetInt("UnlockedlevelHIW" + (GameManager.Instance.HIW_current_level+1), 1);
    }


    public void Lose()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.playSFX("Lose");
        lose_scene.gameObject.SetActive(true);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, -25, Camera.main.transform.position.z);
    }

    public void Pause()
    {
        AudioManager.instance.playSFX("Click");
        pause_scene.SetActive(!pause_scene.activeInHierarchy);

        if(pause_scene.activeInHierarchy == false)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void WinMainScene()
    {
        LoadingSceneController.Instance.GotoScene("MainUI");
    }

    public void LoadMainScene()
    {
        //AdManager.Instance.ShowInterstitialAd("BackHome");

        LoadingSceneController.Instance.GotoScene("MainUI");
    }

    public void Replay()
    {
        //AdManager.Instance.ShowInterstitialAd("Replay"); 
        AudioManager.instance.playSFX("Click");
        LoadingSceneController.Instance.GotoScene("HIWGameplay");
    }

    public void ContinuePlayAds()
    {
        AudioManager.instance.playSFX("Click");
        //AdManager.Instance.ShowRewardedAd(ContinuePlay, "MoreTime");
        ContinuePlay(1);
    }
    void ContinuePlay(int i)
    {
        AudioManager.instance.playSFX("Click");
        LoadingSceneController.Instance.GotoScene("HIWGameplay");
    }

    public void AdsEarnMore()
    {
        AudioManager.instance.playSFX("Click");
         //AdManager.Instance.ShowRewardedAd(ExtraMoney, "BonusMoney");
        ExtraMoney(1);
    }

    void ExtraMoney(int i)
    {
        GameManager.Instance.money += GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].money*4;

        money.text = "+ " + (GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].money * 5).ToString();
    }
    public void NextLevel()
    {
        AudioManager.instance.playSFX("Click");
        GameManager.Instance.HIW_current_level++;
        //AdManager.Instance.ShowInterstitialAd("NextLevel");
        LoadingSceneController.Instance.GotoScene("HIWGameplay");
    }
}
