using Spine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static AnswerController;

public class GameController : MonoBehaviour
{
    [Header("Other refs")]
    public static GameController instance;
    [SerializeField]
    private Transform Character_Container;
    [SerializeField]
    private Transform Answer_Container;
    [SerializeField]
    private Transform Background_Container;
    [SerializeField]
    private GameObject Character_Prefab;
    [SerializeField]
    private GameObject Answer_Prefab;
    [SerializeField]
    private GameObject Background_Prefab;


    [Header("List refs")]
    [SerializeField]
    public List<GameObject> Image_standard;
    [SerializeField]
    public List<GameObject> Character;
    [SerializeField]
    private List<SpriteRenderer> ContainFrame;
    [Header("Logic variable")]
    public static GamePlayState gamePlayState;

    public static float timer;
    public static bool isColliding;
    static public GameObject MapContain;
    static public GameObject Boss;
    public static bool end_game;

    private void Awake()
    {
        instance = this;
        SetUpLevel();
        SetUpPosition();
    }

    private void Start()
    {
        gamePlayState = GamePlayState.PROCESS;
        end_game = false;
        isColliding = false;
        Time.timeScale = 1;
        GameUIController.Instance.StopAllSoudn();
        AudioManager.instance.playMusic();
    }

    private void Update()
    {
        GameUIController.Instance.Timer.text = Mathf.RoundToInt(timer).ToString();
        if (gamePlayState != GamePlayState.PAUSE && gamePlayState != GamePlayState.LOSE)
        {
            gamePlayState = GamePlayState.PROCESS;
        }
        if (!end_game)
        {
            timer -= Time.deltaTime;
        }
        if (timer > 0)
        {
            
            foreach (var i in Image_standard)
            {
                i.GetComponent<AnswerController>().state = StateAnswer.HINTABLE;
                if ((Vector3.Distance(i.transform.position, Character[Image_standard.IndexOf(i)].transform.position) > 1.5f))
                {
                    return;
                }
                else
                {
                    if ((Character[Image_standard.IndexOf(i)].GetComponent<MeshRenderer>().material.name != i.GetComponent<MeshRenderer>().material.name))
                    {   
                        return;
                    }
                }
                i.GetComponent<AnswerController>().state = StateAnswer.UNHINTABLE;
            }
            gamePlayState = GamePlayState.WIN;
        }
        else if (!end_game)
        {
            gamePlayState = GamePlayState.LOSE;
            GameUIController.Instance.Lose();
            end_game = true;
        }
    }

    void SetUpLevel()
    {
        LevelData data = GameManager.Instance.Levels[GameManager.Instance.current_level];
        timer = data.time;
        MapContain = Instantiate(data.MapLevel);
        Boss = Instantiate(data.BossLevel, new Vector3(30, -35, 0), Quaternion.identity);
        if (data.Background.Count == 1)
        {
            ContainFrame.Add(Instantiate(Background_Prefab, Background_Container).GetComponent<SpriteRenderer>());
            ContainFrame[0].sprite = data.Background[0];
            ContainFrame[0].gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
            foreach (var i in data.Answers)
            {
                GameObject cha = Instantiate(Character_Prefab, Character_Container);
                GameObject ans = Instantiate(Answer_Prefab, Answer_Container);

                cha.gameObject.name = i._character;
                ans.gameObject.name = i._character;

                Image_standard.Add(ans);
                Character.Add(cha);

                //ans.SetActive(false);
            }
        }
        else
        {
            foreach (var i in data.Answers)
            {
                
                GameObject cha = Instantiate(Character_Prefab, Character_Container);
                GameObject ans = Instantiate(Answer_Prefab, Answer_Container);

                cha.gameObject.name = i._character;
                ans.gameObject.name = i._character;

                Image_standard.Add(ans);
                Character.Add(cha);
            }

            foreach(var j in data.Background)
            {
                ContainFrame.Add(Instantiate(Background_Prefab, Background_Container).GetComponent<SpriteRenderer>());

                ContainFrame[ContainFrame.Count - 1].sortingOrder = 4;
                Color color = ContainFrame[ContainFrame.Count - 1].color;
                color.a = 0.7f;

                ContainFrame[ContainFrame.Count - 1].color = color;

                ContainFrame[ContainFrame.Count - 1].sprite = j;
                ContainFrame[ContainFrame.Count - 1].transform.position = data.Positon_Frame[ContainFrame.Count - 1];
                ContainFrame[ContainFrame.Count - 1].gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
            }
        }
    }

    public void ForwardObject()
    {
        if (ContainFrame.Count > 1)
        {
            foreach (var i in ContainFrame)
            {
                i.sortingOrder = 5;
                Color color = i.color;
                color.a = 1f;
            }
        }
    }

    public bool CheckWin()
    {
        foreach (var i in Image_standard)
        {
            i.GetComponent<AnswerController>().state = StateAnswer.HINTABLE;
            if ((Vector3.Distance(i.transform.position, Character[Image_standard.IndexOf(i)].transform.position) > 1.5f))
            {
                return false; 
            }
            else
            {
                if ((Character[Image_standard.IndexOf(i)].GetComponent<SpriteRenderer>().sprite != i.GetComponent<SpriteRenderer>().sprite))
                {
                    return false;
                }
            }
            i.GetComponent<AnswerController>().state = StateAnswer.UNHINTABLE;
        }
        return true;
    }
    public void ShowHint()
    {
        if (GameManager.Instance.hint >= 1)
        {
            foreach (var i in Image_standard)
            {
                if (i.GetComponent<AnswerController>().state == StateAnswer.HINTABLE)
                {
                    AudioManager.instance.playSFX("Click");
                    i.GetComponent<AnswerController>().state = StateAnswer.UNHINTABLE;
                    StartCoroutine(Hint(i.GetComponent<AnswerController>()));
                    GameManager.Instance.hint--;
                    GameUIController.Instance.hint.text = "x" + GameManager.Instance.hint;
                    GameManager.Instance.Save();
                    break;
                }
            }
        }
        else
        {
            MoreHint(1);
            //AdManager.Instance.ShowRewardedAd(MoreHint,"MoreHint");
        }
    }

    void MoreHint(int i)
    {
        GameManager.Instance.hint+= 2;
        GameManager.Instance.Save();
        ShowHint();
    }
    IEnumerator Hint(AnswerController answer)
    {
        answer.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);
        answer.gameObject.SetActive(false);
        answer.state = StateAnswer.HINTABLE;
    }

    void SetUpPosition()
    { 
        float minX = -10;
        float maxX = 10;


        float spacing = (maxX - minX) / (Character.Count - 1);
        float xPosition = minX;
        if (Character.Count != 1)
        {
            foreach (GameObject obj in Character)
            {
                Vector3 position = new Vector3(xPosition, -28f, 0f);
                obj.transform.position = position;

                xPosition += spacing;
            }
        }
        else
        {
            Vector3 position = new Vector3(0, -28f, 0f);
            Character[0].transform.position = position;
        }
    }

    private void OnDestroy()
    {
        gamePlayState = GamePlayState.PROCESS;

        timer = 0;
        isColliding = false;
        MapContain = null;
        Boss = null;
        end_game = false;
    }
}
