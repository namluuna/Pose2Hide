using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static AnswerController;


public class HIWGameController : MonoBehaviour
{
    public static GamePlayState gamePlayState;
    // Start is called before the first frame update
    [SerializeField]
    private Transform Character_Container;
    [SerializeField]
    private GameObject Character_Prefab;

    [SerializeField]
    private UnityEngine.Animation wall;
    [SerializeField]
    private Transform wall_container;
    [SerializeField]
    private List<UnityEngine.Animation> Walls = new List<UnityEngine.Animation>();

    [SerializeField]
    private ListWall wall_sprites;

    [SerializeField]
    List<HIWAnswerSpawn> HIWCharacter = new List<HIWAnswerSpawn>();
    [SerializeField]
    List<ListHIWAnswer> HIWAnswers = new List<ListHIWAnswer>(); 

    [SerializeField]
    private static List<GameObject> Characters = new List<GameObject>();
    [SerializeField]
    public static Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>();
    public static bool isEndGame = false;

    public static int current_phase,end_phase;
    public bool isPhase;
    float timer;
    void Start()
    {
        Time.timeScale = 1;
        SetUpLevel();
        SetUpPosition();
    }

    void SetUpLevel()
    {
        if (GameManager.Instance.HIW_current_level == 0)
        {
            TutorialController.Instance.LoadListTutorial("Level Bonus");
        }
        current_phase = 0;
        HIWCharacter = GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].Characters;
        wall_sprites = GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].Walls;
        HIWAnswers = GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level].Answers;
        timer = wall_sprites.Anime[0];
        foreach (var i in wall_sprites.Sprites)
        {
            Walls.Add(Instantiate(wall,wall_container));
            Walls[Walls.Count - 1].GetComponent<SpriteRenderer>().sprite = i;
            Walls[Walls.Count - 1].gameObject.SetActive(false);
        }
        end_phase = wall_sprites.Sprites.Count-1;
        isPhase = true;
        isEndGame = false;
        foreach (var i in HIWCharacter)
        {
            GameObject cha = Instantiate(Character_Prefab, Character_Container);
            cha.gameObject.name = i._character;
            Characters.Add(cha);
        }
        Walls[current_phase].gameObject.SetActive(true);
        Walls[current_phase].Play("Anime" + wall_sprites.Anime[0]);
    }

    private void Update()
    {
        
        if (isPhase)
        {
            if (timer > 0)
            {
                gamePlayState = GamePlayState.PROCESS;
                timer -= Time.deltaTime;
                foreach (var i in Characters)
                {
                    CharacterHIWController index = i.GetComponent<CharacterHIWController>();
                    gamePlayState = GamePlayState.PROCESS;
                    if (index.index != getIndex(index.gameObject.name))
                    {
                        return;
                    }
                    else
                    {
                        if (index._sprite.skeletonDataAsset.skeletonJSON.name[index._sprite.skeletonDataAsset.skeletonJSON.name.Length - 1] - '0' != HIWAnswers[current_phase]._listAnswer[getIndex(index.gameObject.name)]._pose)
                        {
                            return;
                        }
                    }
                }
                Debug.Log("Win");
                gamePlayState = GamePlayState.WIN;

            }
            else if (!isEndGame && gamePlayState != GamePlayState.WIN)
            {
                isPhase = false;
                isEndGame = true;
                Walls[current_phase].Stop();
                HIWGameUIController.Instance.Lose();
            }
            else if (current_phase != end_phase && !isEndGame)
            {
                Walls[current_phase].GetComponent<SpriteRenderer>().sortingOrder = 3;
                current_phase++;
                timer = wall_sprites.Anime[current_phase];
                isPhase = true;
                Walls[current_phase].gameObject.SetActive(true);
                Walls[current_phase].Play("Anime"+ wall_sprites.Anime[current_phase]);
            }
            else if(current_phase == end_phase && gamePlayState == GamePlayState.WIN && !isEndGame) 
            {
                isPhase = false;
                isEndGame = true;
                HIWGameUIController.Instance.Victory();
            }
        }
    }
    int getIndex(string name)
    {
        foreach (var i in HIWAnswers[current_phase]._listAnswer)
        {
            if (i._character.Equals(name))
            {
                return HIWAnswers[current_phase]._listAnswer.IndexOf(i);
            }   
        }
        return -1;
        
    }

    void SetUpPosition()
    {
        float minX = -17f;
        float maxX = 17f;
        int count = Characters.Count;
        float spacing = (maxX - minX) / (count - 1);
        float middleX = minX + (maxX - minX) / 2f;

        for (int i = 0; i < count; i++)
        {
            float xPosition;

            if (count % 2 == 0) // Nếu số lượng là chẵn
            {
                // Cân bằng ở hai bên
                xPosition = minX + spacing * i;
            }
            else // Nếu số lượng là lẻ
            {
                // Đặt một GameObject ở vị trí giữa màn hình
                if (i == count / 2)
                {
                    xPosition = middleX;
                }
                else
                {
                    // Cân bằng ở hai bên
                    if (i < count / 2)
                    {
                        xPosition = minX + spacing * i;
                    }
                    else
                    {
                        xPosition = middleX + spacing * (i - count / 2);
                    }
                }
            }

            Vector3 position = new Vector3(xPosition, 0, 0f);
            positions.Add(i, position);
            Characters[i].transform.localPosition = position;
            Characters[i].GetComponent<CharacterHIWController>().index = i;
            Characters[i].GetComponent<CharacterHIWController>().ResetPosition();
        }
    }

    private void OnDestroy()
    {
        current_phase = 0;
        end_phase = 0;
        positions.Clear();
        Characters.Clear();
    }
}
