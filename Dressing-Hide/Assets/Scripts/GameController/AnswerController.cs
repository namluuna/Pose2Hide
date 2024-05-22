using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AnswerController : MonoBehaviour
{
    public StateAnswer state;

    public enum StateAnswer
    {
        HINTABLE,
        UNHINTABLE
    }

    
    private void Start()
    {
        AnswerSetting();
        gameObject.SetActive(false);
    }

    void AnswerSetting()
    {
        LevelData data = GameManager.Instance.Levels[GameManager.Instance.current_level];

        List<CharacterData> char_data = GameManager.Instance.Characters;

        foreach (var i in data.Answers)
        {
            if (i._character == gameObject.name)
            {
                foreach(var j in char_data)
                {
                    if (j.CharacterId == gameObject.name)
                    {
                        gameObject.GetComponent<SkeletonAnimation>().skeletonDataAsset = j.ListOfAnswer[i._pose];
                        gameObject.GetComponent<SkeletonAnimation>().Initialize(true);
                        gameObject.transform.position = i._positon;
                    }
                }
            }
        }
    }

}
