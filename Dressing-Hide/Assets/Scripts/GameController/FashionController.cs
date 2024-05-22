using Spine;
using Spine.Unity.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static AnswerController;


public enum GamePlayState
{
    WIN,
    LOSE,
    PROCESS,
    PAUSE
}
public class FashionController : MonoBehaviour
{
    [Header("Other refs")]
    public static FashionController instance;
    [SerializeField]
    private Transform Character_Container;
    [SerializeField]
    private GameObject Character_Prefab;
    [SerializeField]
    private GameObject Background_Prefab;

    public FashionLevelData FashionLevelData;

    public static GamePlayState gamePlayState;

    public Dictionary<MixAndMatchSkinsExample.ItemType, FashionValue> ListSkinValue = new Dictionary<MixAndMatchSkinsExample.ItemType, FashionValue>(); 
    public static float timer;
    public static bool end_game;

    [SerializeField]
    private GameObject Win;
    [SerializeField]
    private GameObject Lose;

    private void Awake()
    {
        instance = this;

    }

    public FashionValue SumAllItem()
    {
        FashionValue totalFashionValue = new FashionValue();

        foreach (var kvp in ListSkinValue)
        {
            totalFashionValue.Elegant += kvp.Value.Elegant;
            totalFashionValue.Sexy += kvp.Value.Sexy;
            totalFashionValue.Cute += kvp.Value.Cute;
            totalFashionValue.Stylish += kvp.Value.Stylish; 
            totalFashionValue.Warm += kvp.Value.Warm;
        }

        return totalFashionValue;
    }

    private void Start()
    {
        gamePlayState = GamePlayState.PROCESS;
        end_game = false; 
        Time.timeScale = 1;
    }

    public void CheckWin()
    {
        FashionValue totalFashionValue = SumAllItem();


        if (totalFashionValue.CalculateTotal() >= FashionLevelData.FashionValue.CalculateTotal())
        {
            Win.SetActive(true);
        }
        else
        {
            Lose.SetActive(true);
        }
    }
}
