using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public List<CharacterData> Characters;
    public List<LevelData> Levels;
    public List<HIWLevelData> HIWLevels;
    public Dictionary<string, int > SkinCharacters = new Dictionary<string, int>();
    public int HIW_current_level;
    public int current_level;
    public bool isReload;
    public int money;
    public int hint;

    public int isDecorTutorial;
    public int isNewGameTutorial;
    private void Awake()
    {
        isDecorTutorial = PlayerPrefs.GetInt("isDecorTutorial",0);
        money = PlayerPrefs.GetInt("money",100);
        hint = PlayerPrefs.GetInt("hint", 0);
        if (instance == null)
        {
            instance = this;
            current_level = PlayerPrefs.GetInt("CurrentLevel",0);
          
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSkinData();
        //AdManager.Instance.Init();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;   
    }
    public void Save()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("hint", hint);

    }


    private void LoadMoney()
    {
        money = PlayerPrefs.GetInt("money", 0);
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public void gotoDecorScene()
    {
        LoadingSceneController.Instance.GotoDecor(true);
    }
    public void gotoDecorSceneTotorial()
    {
        LoadingSceneController.Instance.GotoDecor(false);
    }

    void LoadDecor()
    {
        //TutorialController.Instance.LoadListTutorial("Shop_1");
        GameObject.Find("MainUI").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("MainUI").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("MainUI").transform.GetChild(1).gameObject.SetActive(false);
    }

    void LoadDecorTutorial()
    {
        
    }

    public void LoadSkinData()
    {
        foreach (var i in Characters)
        {
            SkinCharacters.Add(i.CharacterId, PlayerPrefs.GetInt(i.CharacterId,0)); 
        }
    }

    public void SetSkinData(string Character, int SkinIndex)
    {
        SkinCharacters[Character] = SkinIndex;
        PlayerPrefs.SetInt(Character, SkinIndex);
    }
}
