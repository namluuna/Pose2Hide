using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public TutorialData Tutorials;
    private static TutorialController instance;

    [SerializeField]
    Transform ListImageTutorial;
    [SerializeField]
    GameObject ImageTutorial;
    [SerializeField] 
    List<GameObject> ImageListTutorial;

    [SerializeField]
    GameObject ScreenTutorial;
    [SerializeField]
    GameObject NextButton;

    int CurrentIndex;
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
        
    }

    public static TutorialController Instance
    {
        get { return instance; }
    }

    public void LoadListTutorial(string TutorialName)
    {
        //if (PlayerPrefs.GetInt(TutorialName, 0) == 1)
        //{
        //    return;
        //}
        //NextButton.SetActive(true);

        //foreach (var i in Tutorials.ListTutorial)
        //{
        //    if(i.Name == TutorialName)
        //    {
        //        foreach (var j in i.sprites)
        //        {
        //            GameObject image = Instantiate(ImageTutorial, ListImageTutorial);
        //            image.GetComponent<Image>().sprite = j;
        //        }
        //        break;
        //    }
        //}
        //CurrentIndex = ListImageTutorial.childCount-1;
        //Debug.Log("____"+TutorialName);
        //ScreenTutorial.SetActive(true);
        //PlayerPrefs.SetInt(TutorialName, 1);
    }
    public void NextImage()
    {
        GameObject CurrentImage = ListImageTutorial.transform.GetChild(CurrentIndex).gameObject;

        StartCoroutine(MoveTransform(CurrentImage));

        CurrentIndex--;
        if (CurrentIndex < 0)
        {
            NextButton.SetActive(false);

            for (int i = 0; i < ListImageTutorial.transform.childCount; i++)
            {
               Destroy(ListImageTutorial.transform.GetChild(i).gameObject);
            }
        }
    }

    IEnumerator MoveTransform(GameObject CurrentImage)
    {
        iTween.MoveTo(CurrentImage, iTween.Hash(
            "position", new Vector3(CurrentImage.transform.position.x - 1500, CurrentImage.transform.position.y, 0),
            "time", 0.3f,
            "easetype", iTween.EaseType.linear
        ));
        yield return new WaitForSeconds(0.3f);
    }   
}
