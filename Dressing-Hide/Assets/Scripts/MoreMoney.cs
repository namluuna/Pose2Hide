using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreMoney : MonoBehaviour
{
    public GameObject watchAds;
    public GameObject Claim;

    private const string LastCallDateKey = "LastCallDate";
    private const string CallCountKey = "CallCount";
    private const int MaxCallCount = 3;

    private void Start()
    {
        DateTime lastCallDate = DateTime.Parse(PlayerPrefs.GetString(LastCallDateKey, DateTime.MinValue.ToString()));
        DateTime currentDate = DateTime.Now.Date;

        if (currentDate > lastCallDate)
        {
            PlayerPrefs.SetString(LastCallDateKey, currentDate.ToString());
            PlayerPrefs.SetInt(CallCountKey, 0);
        }

    }

    public void GetMoreMoney()
    {
        //AdManager.Instance.ShowRewardedAd(MoneyAds, "MoreMoney");
        MoneyAds(1); 
    }

    void MoneyAds(int i)
    {
        int callCount = PlayerPrefs.GetInt(CallCountKey, 0);

        if (callCount < MaxCallCount)
        {
            watchAds.SetActive(false);
            Claim.SetActive(true);

            callCount++;
            PlayerPrefs.SetInt(CallCountKey, callCount);
        }
        else
        {
            Message.Instance.ShowMessage("Your daily bonus are used up",watchAds.transform.position);
        }
    }


    public void ClaimMoney()
    {
        watchAds.SetActive(true);
        Claim.SetActive(false);
        GameManager.Instance.money = GameManager.Instance.money + 200;
        GameManager.Instance.Save();
    }

}
