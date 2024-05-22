using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Message : MonoBehaviour
{
    public static Message Instance;

    [SerializeField]
    GameObject message;
    [SerializeField]
    TextMeshProUGUI MessageText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string content,Vector3 position)
    {
        StopAllCoroutines();
        MessageText.text = content;
        message.transform.position = position;
        message.SetActive(true);
        StartCoroutine(MessagePopUp());
    }

    IEnumerator MessagePopUp()
    {
        iTween.MoveTo(message, iTween.Hash(
           "y", message.transform.position.y + 50f, 
           "time", 1,
           "easetype", iTween.EaseType.easeOutQuad
       ));

        iTween.ValueTo(gameObject, iTween.Hash(
           "from", 1, 
           "to", 0.0f,
           "time", 1,
           "onupdate", "UpdateTextAlpha" 
       ));
        yield return new WaitForSeconds(1f);
        message.SetActive(false);
    }
    void UpdateTextAlpha(float newAlpha)
    {
        // Cập nhật giá trị alpha cho văn bản
        Color textColor = MessageText.color;
        textColor.a = newAlpha;
        MessageText.color = textColor;
    }
}
