using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ObjectPool;

public class Wheel : MonoBehaviour
{
    public int numberOfGift = 8;
    public float timeRotate;
    public float numberCircleRotate;

    private const float CIRCLE = 360.0f;
    private float angleOfOneGift;
    public Transform parrent;
    private float currentTime;
    public AnimationCurve curve;
    private string gift;
    private int RollTime;

    [SerializeField]
    public GameObject text;

    [SerializeField]
    public TextMeshProUGUI remain_text;
    [SerializeField]
    public TextMeshProUGUI ads_remain_text;
    [SerializeField]
    public TextMeshProUGUI remain_rolltime;
    [SerializeField]
    public GameObject popUp;

    [SerializeField]
    Button roll;

    [SerializeField]
    Button rollAds;

    [SerializeField]
    Button back;
    // Start is called before the first frame update
    void Start()
    {
        angleOfOneGift = CIRCLE / numberOfGift;
        setPositionToData();
        ResetRollTime();
        RollTime = PlayerPrefs.GetInt("RollTime", 0);
        remain_rolltime.text = RollTime + "/10";
    }
    public static void getRandomItem() 
    {
        List<backGroundObjectSetting> listRamdom = new List<backGroundObjectSetting>();
        listRamdom.Clear();
        GameObject listItemManager = GameObject.Find("ListItemManager");
        foreach (var item in listItemManager.GetComponent<ListItemManager>().listItem)
        {
            if (item.unlock == 0)
            {
                listRamdom.Add(item);
            }
        }
        int minValue = 0;
        int maxValue = listRamdom.Count;

        System.Random random = new System.Random();
        int randomNumber = random.Next(minValue, maxValue);

        backGroundObjectSetting randomItem = listRamdom[randomNumber];


        GameObject objectWithTag = GameObject.Find("MainUI");
        Transform firstChildTransform = objectWithTag.transform.GetChild(3);
        GameObject obj = firstChildTransform.gameObject;
        Transform getBoxlist = obj.transform.GetChild(0);
        Transform Grid = getBoxlist.GetChild(0);
        GameObject grid = Grid.gameObject;
        List<Pool> pool = grid.GetComponent<ObjectPoolForList>().pools;

        foreach (var i in pool)
        {
            if (randomItem.name == i.prefab.name)
            {
                PopUpInstance.instance.transform.GetChild(0).
                    Find("ItemGet").GetChild(2).GetComponent<Image>().sprite 
                    = i.prefab.transform.GetChild(0).GetComponent<Image>().sprite;
                i.prefab.GetComponent<BuyProperties>().statusItem = statusItem.own;
                listItemManager.GetComponent<ListItemManager>().SaveData();
                break;
            }
        }
    }

    private void Gift(int index)
    {
        switch (index)
        {
            case 1:
                getRandomItem();
                gift = "";
                break;

            case 2:
                GameManager.Instance.hint += 3;
                gift = "3 hints";
                break;

            case 3:
                GameManager.Instance.money += 400;
                gift = "400$";
                break;

            case 4:
                GameManager.Instance.hint += 2;
                gift = "2 hints";
                break;

            case 5:
                getRandomItem();
                gift = "";
                break;

            case 6:
                GameManager.Instance.money += 200;
                gift = "200$";
                break;

            case 7:
                GameManager.Instance.hint += 1;
                gift = "1 hint";
                break;
            case 8:
                GameManager.Instance.money += 100;
                gift = "100$";
                break;

            default:
                // Code to be executed when expression doesn't match any case
                break;
        }
    }

    IEnumerator RoateWheel()
    {
        AudioManager.instance.playSFX("Roll");
        AudioManager.instance.musicSource.Pause();
        float startAngle = transform.eulerAngles.z;
        currentTime = 0;
        
        int indexGiftRandom= 0;
        float randomValue = UnityEngine.Random.value; // Generate a random value between 0.0 and 1.0

        //int randomNumber;
        if (randomValue < 0.15f)
        {
            //money 15%
            indexGiftRandom = 8;
        }
        else if (randomValue >= 0.15f && randomValue < 0.45f)
        {
            //hint 30%
            indexGiftRandom = 7;
        }
        else if (randomValue >= 0.45f && randomValue < 0.55f)
        {
            //money 10%
            indexGiftRandom = 6;
        }
        else if (randomValue >= 0.55f && randomValue < 0.60f)
        {
            //item 5%
            indexGiftRandom = 5;
        }
        else if (randomValue >= 0.60f && randomValue < 0.80f)
        {
            //hint 20%
            indexGiftRandom = 4;
        }
        else if (randomValue >= 0.80f && randomValue < 0.85f)
        {
            //money 5%
            indexGiftRandom = 3;
        }
        else if (randomValue >= 0.85f && randomValue < 0.95f)
        {
            //hint 10%
            indexGiftRandom = 2;
        }
        else if (randomValue >= 0.95f && randomValue <= 1f)
        {
            //item 5%
            indexGiftRandom = 1;
        }
        Debug.Log(indexGiftRandom);

        Gift(indexGiftRandom);
        //Gift(1);

        roll.interactable = false;
        rollAds.interactable = false;
        back.interactable = false;
        float angleWant = (numberCircleRotate * CIRCLE) + angleOfOneGift * indexGiftRandom-startAngle;
        while (currentTime<timeRotate)
        {
            yield return new WaitForEndOfFrame();
            currentTime +=  Time.deltaTime;
            float angleCurrent = angleWant * curve.Evaluate(currentTime/timeRotate);
            this.transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }

        AudioManager.instance.SoundfxSource.Stop();

        popUp.SetActive(true);
        AudioManager.instance.playSFX("Reward");
        Transform childTransform = popUp.transform.Find("ItemGet");

        if (indexGiftRandom == 1 || indexGiftRandom == 5)
        {
            Transform item = childTransform.GetChild(2);
            item.gameObject.SetActive(true);
        }
        else if (indexGiftRandom == 2 || indexGiftRandom == 4 || indexGiftRandom == 7)
        {
            Transform hint = childTransform.GetChild(1);
            hint.gameObject.SetActive(true);
        }
        else if(indexGiftRandom == 3 || indexGiftRandom == 6 || indexGiftRandom == 8)
        {
            Transform money = childTransform.GetChild(0);
            money.gameObject.SetActive(true);
        }

        text.GetComponent<TextMeshProUGUI>().text = gift;

        yield return new WaitForSeconds(1f);
        //roll.interactable = true;
        //rollAds.interactable = true;
        back.interactable = true;
        AudioManager.instance.musicSource.UnPause();
        GameManager.Instance.Save();

    }

    private const string LAST_PLAYED_TIME_KEY = "LastPlayedTime";
    private static readonly System.TimeSpan TWELVE_HOURS = new System.TimeSpan(2, 0, 0);
    //[ContextMenu("Rotate")]
    public void RotateNow()
    {
        // Lấy mốc thời gian đã chơi lần cuối từ PlayerPrefs
        if (PlayerPrefs.HasKey(LAST_PLAYED_TIME_KEY))
        {
            string lastPlayedTimeString = PlayerPrefs.GetString(LAST_PLAYED_TIME_KEY);
            System.DateTime lastPlayedTime = System.DateTime.Parse(lastPlayedTimeString);

            // Lấy thời gian hiện tại
            System.DateTime currentTime = System.DateTime.Now;

            // Tính thời gian đã trôi qua kể từ lần chơi cuối cùng
            System.TimeSpan elapsedTime = currentTime - lastPlayedTime;

            // So sánh với 12 tiếng
            if (elapsedTime >= TWELVE_HOURS)
            {
                // Gọi hàm RotateWheel() sau khi đã trôi qua 12 tiếng
                StartCoroutine(RoateWheel());

                // Lưu mốc thời gian mới vào PlayerPrefs
                PlayerPrefs.SetString(LAST_PLAYED_TIME_KEY, currentTime.ToString());
                PlayerPrefs.Save();
            }
            else
            {
                Message.Instance.ShowMessage("Please wait patiently", popUp.transform.position);
            }
        }
        else
        {
            //if (AdManager.Instance.AdSpacing <= 0) { //AdManager.Instance.ShowInterstitialAd("Replay"); AdManager.Instance.ResetAdspace(); }

            StartCoroutine(RoateWheel());
            roll.interactable = true;
            // Nếu chưa có mốc thời gian lần chơi cuối cùng, lưu mốc thời gian mới vào PlayerPrefs
            System.DateTime currentTime = System.DateTime.Now;
            PlayerPrefs.SetString(LAST_PLAYED_TIME_KEY, currentTime.ToString());
            PlayerPrefs.Save();
            ////AdManager.Instance.ShowInterstitialAd("LuckyWheel");
        }
    }


    public void AdsRotateNow()
    {
        //RotateWheelAds(1);
        if (MainUIController.CountDownAdsWheel <= 0)
        {
            RotateWheelAds(1);
            //if (AdManager.Instance.ShowRewardedAd(RotateWheelAds, "WheelAds"))
            //{

            //}
        }
        else
        {
            Message.Instance.ShowMessage("Please wait patiently", popUp.transform.position);
        }
    }

    void RotateWheelAds(int i)
    {
        if (RollTime > 0)
        {
            RollTime--;
            PlayerPrefs.SetInt("RollTime", RollTime);
            StartCoroutine(RoateWheel());
            MainUIController.CountDownAdsWheel = 30f;
        }
        else
        {
            Message.Instance.ShowMessage("Your daily spins are used up", popUp.transform.position);
        }
    }

    void setPositionToData()
    {
        for (int i =0; i< parrent.childCount; i++)
        {
            parrent.GetChild(i).eulerAngles = new Vector3(0, 0, -CIRCLE / numberOfGift * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RemainTime();
        remain_rolltime.text = RollTime + "/10";
    }


    public void ResetRollTime()
    {
        // Lấy ngày được lưu từ PlayerPrefs
        string savedDateString = PlayerPrefs.GetString("ResetDay", "");

        if (!string.IsNullOrEmpty(savedDateString))
        {
            // Nếu có ngày được lưu, chuyển đổi nó thành DateTime
            DateTime savedDate = DateTime.Parse(savedDateString);

            // Lấy ngày hiện tại
            DateTime currentDate = DateTime.Now;

            // So sánh ngày hiện tại với ngày được lưu
            TimeSpan difference = currentDate - savedDate;

            if (difference.Days > 0)
            {
                PlayerPrefs.SetInt("RollTime", 10);
            }
            else
            {
                RollTime = PlayerPrefs.GetInt("RollTime", 0);
            }
        }
        else
        {
            // Nếu chưa có ngày được lưu, lưu ngày hiện tại vào PlayerPrefs
            PlayerPrefs.SetInt("RollTime", 10);
            DateTime currentDate = DateTime.Now;
            PlayerPrefs.SetString("ResetDay", currentDate.ToString());
        }
        PlayerPrefs.Save();
    }


    public void RemainTime()
    {
        string lastPlayedTimeString = PlayerPrefs.GetString(LAST_PLAYED_TIME_KEY, System.DateTime.Now.ToString());
        System.DateTime lastPlayedTime = System.DateTime.Parse(lastPlayedTimeString);

        // Calculate the remaining time
        System.TimeSpan elapsedTime = System.DateTime.Now - lastPlayedTime;
        System.TimeSpan remainingTime = TWELVE_HOURS - elapsedTime;

        // Format the remaining time as "HH:mm:ss"
        string remainingTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);

        // Update the TextMeshProUGUI component with the remaining time
        if (remainingTime <= System.TimeSpan.Zero|| remainingTimeString.Equals("01:59:59"))
        {
            remain_text.text = "Spin";
            GameObject.FindObjectOfType<MainUIController>().Wheel_redtic.SetActive(true);
        }
        else
        {
            // Format the remaining time as "HH:mm:ss"
            remainingTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
            remain_text.text = remainingTimeString;     
            GameObject.FindObjectOfType<MainUIController>().Wheel_redtic.SetActive(false);
        }

        if(MainUIController.CountDownAdsWheel > 0)
        {
            MainUIController.CountDownAdsWheel -= Time.deltaTime;
            ads_remain_text.text = Mathf.RoundToInt(MainUIController.CountDownAdsWheel) + "";
            GameObject.FindObjectOfType<MainUIController>().Wheel_redtic.SetActive(false);
        }
        else
        {
            GameObject.FindObjectOfType<MainUIController>().Wheel_redtic.SetActive(true);
            ads_remain_text.text = "Spin";
        }
        
    }
}
