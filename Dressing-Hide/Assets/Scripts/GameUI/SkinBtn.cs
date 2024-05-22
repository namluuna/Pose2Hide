using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinBtn : MonoBehaviour
{
    string Character;
    int SkinIndex;
    int Price;
    // Start is called before the first frame update
    void Start()
    {
        LoadSkinData();
    }

    void LoadSkinData()
    {
        string [] strings = gameObject.name.Split("_");
        Character = strings[0];
        SkinIndex = int.Parse(strings[1]);

        int isOwn = PlayerPrefs.GetInt(gameObject.name,0);

        if (isOwn == 0)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (isOwn == 1 || SkinIndex == 0 )
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if(PlayerPrefs.GetInt(Character) == SkinIndex)
            {
                transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
                transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }

        foreach (var i in GameManager.Instance.Characters)
        {
            if(i.CharacterId == Character)
            {
                Price = i.ListSkinData[SkinIndex].Price;
                break;
            }
        }
    }

    public void SetSkin()
    {
        GameManager.Instance.SetSkinData(Character, SkinIndex);
    }

    public void BuySkin()
    {
        if(GameManager.Instance.money > Price)
        {
            GameManager.Instance.money -= Price;
            GameManager.Instance.Save();
            PlayerPrefs.SetInt(gameObject.name, 1);
        }
        else
        {
            Message.Instance.ShowMessage("You dont have enough money",transform.position);
        }
    }
}
