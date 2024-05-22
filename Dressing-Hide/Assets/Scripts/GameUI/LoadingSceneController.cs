using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;

    [SerializeField]
    GameObject LoadingScenePrefab;
    [SerializeField]
    GameObject Policy;
    [SerializeField]
    GameObject Notify;
    public static float LoadingTime = 1.0f;

    [SerializeField]
    Slider loadingslider;

    public bool isLoading; 
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Policy", 0) == 0)
        {
            //PlayerPrefs.GetInt("Policy", 0);
            Policy.SetActive(true);
        }
        if (PlayerPrefs.GetInt("IsUpdate" + Application.version, 0) == 0 && !(PlayerPrefs.GetInt("Policy", 0) == 0))
        {
            Notify.SetActive(true);
        }
    }

    public static LoadingSceneController Instance
    {
        get { return instance; }
    }

    public void GotoScene(string scene)
    {
        LoadingScenePrefab.SetActive(true);
        StartCoroutine(LoadSceneAsync(scene));
    }

    public IEnumerator GotoDecorTutorial(bool isDecor)
    {
        LoadingScenePrefab.SetActive(true);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainUI");
        isLoading = true;
        float startTime = Time.time;
        float targetTime = startTime + LoadingTime;

        while (!loadOperation.isDone || Time.time < targetTime)
        {
            float processValue = Mathf.Clamp01((Time.time - startTime) / 1.0f);
            loadingslider.value = processValue;
            AudioManager.instance.musicSource.Pause();
            AudioManager.instance.SoundfxSource.Pause();
            yield return null;
        }
        isLoading = false;
        AudioManager.instance.musicSource.UnPause();
        AudioManager.instance.SoundfxSource.UnPause();
        LoadingScenePrefab.SetActive(false);

        if (isDecor)
        {
            MainUIController.Instance.EditBackGround();
        }
        else
        {
            MainUIController.Instance.EditBackGroundTutorial();
        }
    }

    public void GotoDecor(bool isDecor)
    {
        StartCoroutine(GotoDecorTutorial(isDecor));
    }


    public void IgnoreLoading()
    {
        LoadingScenePrefab.SetActive(true);
        StartCoroutine(SkipLoad());
    }

    public IEnumerator SkipLoad()
    {
        SceneManager.LoadSceneAsync("Gameplay");
        AudioManager.instance.musicSource.Pause();
        AudioManager.instance.SoundfxSource.Pause();
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainUI");

        float startTime = Time.time;
        float targetTime = startTime + LoadingTime;

        while (!loadOperation.isDone || Time.time < targetTime)
        {
            float processValue = Mathf.Clamp01((Time.time - startTime) / 1.0f);
            loadingslider.value = processValue;
            AudioManager.instance.musicSource.Pause();
            AudioManager.instance.SoundfxSource.Pause();
            yield return null;
        }
        AudioManager.instance.musicSource.UnPause();
        AudioManager.instance.SoundfxSource.UnPause();
        LoadingScenePrefab.SetActive(false);
    }
    public IEnumerator LoadSceneAsync(string scene)
    {

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);
        isLoading = true;
        float startTime = Time.time;
        float targetTime = startTime + LoadingTime;

        while (!loadOperation.isDone || Time.time < targetTime)
        {
            float processValue = Mathf.Clamp01((Time.time - startTime) / 1.0f);
            loadingslider.value = processValue;
            AudioManager.instance.musicSource.Pause();
            AudioManager.instance.SoundfxSource.Pause();
            yield return null;
        }
        isLoading = false;
        AudioManager.instance.musicSource.UnPause();
        AudioManager.instance.SoundfxSource.UnPause();
        LoadingScenePrefab.SetActive(false);
    }

    public void Yes()
    {
        PlayerPrefs.SetInt("IronSource_Consent", 1);
        PlayerPrefs.SetInt("Policy", 1);
        Policy.gameObject.SetActive(false);
        Notify.SetActive(true);
        GotoScene("MainUI");
    }
    public void No()
    {
        PlayerPrefs.SetInt("IronSource_Consent", 2);
        PlayerPrefs.SetInt("Policy", 1);
        Policy.gameObject.SetActive(false);
        GotoScene("MainUI");

    }


}