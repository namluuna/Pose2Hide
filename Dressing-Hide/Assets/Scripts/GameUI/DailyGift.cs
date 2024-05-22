using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class DailyGift : MonoBehaviour
{
    [SerializeField]
    GameObject Day1;
    [SerializeField]
    GameObject Day2;
    [SerializeField]
    GameObject Day3;
    [SerializeField]
    GameObject Day4;
    [SerializeField]
    GameObject Day5;
    [SerializeField]
    GameObject Day6;
    [SerializeField]
    GameObject Day7;

    // Start is called before the first frame update
    void Start()
    {
        isShow();
        LoadData();
    }

    void LoadData()
    {
        IsAttend(Day1);
        IsAttend(Day2);
        IsAttend(Day3);
        IsAttend(Day4);
        IsAttend(Day5);
        IsAttend(Day6);
    }

    public bool isShow()
    {
        DateTime currentDate = DateTime.Now;
        int daysSinceLastLogin = 0;
        string savedDateStr;
        if (PlayerPrefs.GetString("LastLoginDate", "FirstTime") != "FirstTime")
        {
            savedDateStr = PlayerPrefs.GetString("LastLoginDate");
            DateTime savedDate = DateTime.ParseExact(
                savedDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TimeSpan timeSinceLastLogin = currentDate.Date - savedDate.Date;
            daysSinceLastLogin = timeSinceLastLogin.Days;
        }

        if (PlayerPrefs.GetInt("Day"+ (daysSinceLastLogin+1), 0) == 1 || PlayerPrefs.GetInt("Day" + (daysSinceLastLogin + 1), 0) == 2)
        {
            return false;
        }
        return true;
    }

    public bool isShowAds()
    {
        DateTime currentDate = DateTime.Now;
        int daysSinceLastLogin = 0;
        string savedDateStr;
        if (PlayerPrefs.GetString("LastLoginDate", "FirstTime") != "FirstTime")
        {
            savedDateStr = PlayerPrefs.GetString("LastLoginDate");
            DateTime savedDate = DateTime.ParseExact(
                savedDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TimeSpan timeSinceLastLogin = currentDate.Date - savedDate.Date;
            daysSinceLastLogin = timeSinceLastLogin.Days;
        }

        if (PlayerPrefs.GetInt("Day" + (daysSinceLastLogin + 1), 0) == 2)
        {
            return false;
        }
        return true;
    }

    public void AttendAds()
    {
        GetX2Gift(1);
        //AdManager.Instance.ShowRewardedAd(GetX2Gift, "DailyGift");
    }

    void GetX2Gift(int i)
    {
        DateTime currentDate = DateTime.Now;
        int daysSinceLastLogin = 0;
        string savedDateStr;
        if (PlayerPrefs.GetString("LastLoginDate", "FirstTime") != "FirstTime")
        {
            savedDateStr = PlayerPrefs.GetString("LastLoginDate");
            DateTime savedDate = DateTime.ParseExact(
                savedDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TimeSpan timeSinceLastLogin = currentDate.Date - savedDate.Date;
            daysSinceLastLogin = timeSinceLastLogin.Days;
        }
        switch (daysSinceLastLogin)
        {
            case 0:
                {
                    if (PlayerPrefs.GetInt("Day1", 0) != 2)
                    {
                        PlayerPrefs.SetString("LastLoginDate", currentDate.ToString("yyyy-MM-dd"));
                        PlayerPrefs.Save();

                        PlayerPrefs.SetInt("Day1", 2);
                        IsAttend(Day1);
                        GameManager.Instance.money += 400;
                    }
                    break;
                }
            case 1:
                {
                    if (PlayerPrefs.GetInt("Day2", 0) != 2)
                    {
                        PlayerPrefs.SetInt("Day2", 2);
                        IsAttend(Day2);
                        GameManager.Instance.hint += 4;
                    }
                    break;
                }
            case 2:
                {
                    if (PlayerPrefs.GetInt("Day3", 0) != 2)
                    {
                        PlayerPrefs.SetInt("Day3", 2);
                        IsAttend(Day3);
                        GameManager.Instance.money += 400;
                    }
                    break;
                }
            case 3:
                {
                    if (PlayerPrefs.GetInt("Day4", 0) != 2)
                    {
                        PlayerPrefs.SetInt("Day4", 2);
                        IsAttend(Day4);
                        GameManager.Instance.money += 400;
                    }
                    break;
                }
            case 4:
                {
                    if (PlayerPrefs.GetInt("Day5", 0) != 2)
                    {
                        PlayerPrefs.SetInt("Day5", 2);
                        IsAttend(Day5);
                        GameManager.Instance.money += 400;
                    }
                    break;
                }
            case 5:
                {
                    if (PlayerPrefs.GetInt("Day6", 0) != 2)
                    {
                        PlayerPrefs.SetInt("Day6", 2);
                        IsAttend(Day6);
                        GameManager.Instance.hint += 4;
                    }
                    break;
                }
            case 6:
                {
                    if (PlayerPrefs.GetInt("Day7", 0) != 2)
                    {
                        PlayerPrefs.SetInt("Day7", 2);
                        Wheel.getRandomItem();
                    }
                    break;
                }
        }
        GameManager.Instance.Save();
    }

    public void Attend()
    {
        DateTime currentDate = DateTime.Now;
        int daysSinceLastLogin = 0;
        string savedDateStr;
        if (PlayerPrefs.GetString("LastLoginDate","FirstTime") != "FirstTime")
        {
            savedDateStr = PlayerPrefs.GetString("LastLoginDate");
            DateTime savedDate = DateTime.ParseExact(
                savedDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TimeSpan timeSinceLastLogin = currentDate.Date - savedDate.Date;
            daysSinceLastLogin = timeSinceLastLogin.Days;
        }
        switch (daysSinceLastLogin) 
        {
            case 0:
                {
                    if (PlayerPrefs.GetInt("Day1", 0) == 0)
                    {
                        PlayerPrefs.SetInt("Day1", 1);
                        IsAttend(Day1);
                        GameManager.Instance.money += 200;
                        PopUpInstance.instance.PopUpPrefab.SetActive(true);
                        PopUpInstance.instance.moneyPrefab.SetActive(true);
                        PopUpInstance.instance.amount.text = "200$";
                    }
                    break;
                }
            case 1:
                {
                    if (PlayerPrefs.GetInt("Day2", 0) == 0)
                    {
                        PlayerPrefs.SetInt("Day2", 1);
                        IsAttend(Day2);
                        GameManager.Instance.hint += 2;
                        PopUpInstance.instance.PopUpPrefab.SetActive(true);
                        PopUpInstance.instance.hintPrefab.SetActive(true);
                        PopUpInstance.instance.amount.text = "2 hints";
                    }
                    break;
                }
            case 2:
                {
                    if (PlayerPrefs.GetInt("Day3", 0) == 0 )
                    {
                        PlayerPrefs.SetInt("Day3", 1);
                        IsAttend(Day3);
                        GameManager.Instance.money += 200;
                        PopUpInstance.instance.PopUpPrefab.SetActive(true);
                        PopUpInstance.instance.moneyPrefab.SetActive(true);
                        PopUpInstance.instance.amount.text = "200$";
                    }
                    break;
                }
            case 3:
                {
                    if (PlayerPrefs.GetInt("Day4", 0) == 0)
                    {
                        PlayerPrefs.SetInt("Day4", 1);
                        IsAttend(Day4);
                        GameManager.Instance.money += 200;
                        PopUpInstance.instance.PopUpPrefab.SetActive(true);
                        PopUpInstance.instance.moneyPrefab.SetActive(true);
                        PopUpInstance.instance.amount.text = "200$";
                    }
                    break;
                }
            case 4:
                {
                    if (PlayerPrefs.GetInt("Day5", 0) == 0)
                    {
                        PlayerPrefs.SetInt("Day5", 1);
                        IsAttend(Day5);
                        GameManager.Instance.money += 200;
                        PopUpInstance.instance.PopUpPrefab.SetActive(true);
                        PopUpInstance.instance.moneyPrefab.SetActive(true);
                        PopUpInstance.instance.amount.text = "200$";
                    }
                    break;
                }
            case 5:
                {
                    if (PlayerPrefs.GetInt("Day6", 0) == 0)
                    {
                        PlayerPrefs.SetInt("Day6", 1);
                        IsAttend(Day6);
                        GameManager.Instance.hint += 2;
                        PopUpInstance.instance.PopUpPrefab.SetActive(true);
                        PopUpInstance.instance.hintPrefab.SetActive(true);
                        PopUpInstance.instance.amount.text = "2 hints";
                    }
                    break;
                }
            case 6:
                {
                    if (PlayerPrefs.GetInt("Day7", 0) == 0)
                    {
                        PlayerPrefs.SetInt("Day7", 1);
                        Wheel.getRandomItem();
                        PopUpInstance.instance.PopUpPrefab.SetActive(true);
                        PopUpInstance.instance.imagePrefab.SetActive(true);
                    }
                    break;
                }
        }
        GameManager.Instance.Save();
    }

    public void IsAttend(GameObject day)
    {
        DateTime currentDate = DateTime.Now;
        int daysSinceLastLogin = 0;
        string savedDateStr;
        if (PlayerPrefs.GetString("LastLoginDate", "FirstTime") != "FirstTime")
        {
            savedDateStr = PlayerPrefs.GetString("LastLoginDate");
            DateTime savedDate = DateTime.ParseExact(
                savedDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TimeSpan timeSinceLastLogin = currentDate.Date - savedDate.Date;
            daysSinceLastLogin = timeSinceLastLogin.Days;
        }

        if (PlayerPrefs.GetInt(day.name, 0) == 1 || PlayerPrefs.GetInt(day.name, 0) == 2)
        {
            day.transform.GetChild(3).gameObject.SetActive(true);
            day.transform.GetChild(0).gameObject.SetActive(false);
            day.transform.GetChild(1).gameObject.SetActive(false);

        } else if (daysSinceLastLogin - int.Parse(day.name[day.name.Length-1].ToString()) >= 0)
        {   
            day.transform.GetChild(4).gameObject.SetActive(true);
            day.transform.GetChild(0).gameObject.SetActive(false);
            day.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
