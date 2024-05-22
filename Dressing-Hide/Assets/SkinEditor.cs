using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinEditor : MonoBehaviour
{
    [SerializeField]
    GameObject ListCharacter;


    [SerializeField]
    GameObject CharacterPrefab;
    [SerializeField]
    GameObject SkinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LoadListSkin();
    }

   void LoadListSkin()
    {
        foreach (var i  in GameManager.Instance.Characters)
        {
            GameObject SkinList = Instantiate(CharacterPrefab,ListCharacter.transform);
            SkinList.name = i.CharacterId;
            foreach (var j in i.ListOfAnime)
            {
                GameObject Skin = Instantiate(SkinPrefab, SkinList.transform.GetChild(0).GetChild(0));
                Skin.gameObject.name = SkinList.name + "_" + i.ListOfAnime.IndexOf(j);
                Skin.GetComponent<Image>().sprite = i.ListSkinData[i.ListOfAnime.IndexOf(j)].Avatar;
            }
            SkinList.GetComponent<RectTransform>().sizeDelta = new Vector2(SkinList.transform.GetChild(0).GetChild(0).childCount * 230 , SkinList.GetComponent<RectTransform>().rect.height);
            SkinList.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -SkinList.transform.childCount * 230 / 2);
        }
        ListCharacter.GetComponent<RectTransform>().sizeDelta = new Vector2(ListCharacter.GetComponent<RectTransform>().rect.width, ListCharacter.transform.childCount * 370);
        ListCharacter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -ListCharacter.transform.childCount * 370/2);
   }
}
