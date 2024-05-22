using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FashionUIController : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    public TextMeshProUGUI Elegant;
    public TextMeshProUGUI Sexy;
    public TextMeshProUGUI Cute;
    public TextMeshProUGUI Stylish;
    public TextMeshProUGUI Warm;

    IEnumerator MoveWin()
    {
        FashionValue totalFashionValue = FashionController.instance.SumAllItem();
        FashionValue RequireFashionValue = FashionController.instance.FashionLevelData.FashionValue;

        float tween_value = totalFashionValue.Elegant > RequireFashionValue.Elegant ? 0.2f : totalFashionValue.Elegant / RequireFashionValue.Elegant;

        iTween.ValueTo(gameObject, iTween.Hash(
           "from", slider.value,
           "to", tween_value,
           "onupdate", "UpdateSliderValue",
           "time", 1.0f, // Thời gian của tween
           "easetype", iTween.EaseType.easeOutQuad // Loại easing
        ));

        yield return new WaitForSeconds(1f);


    }


    public void TestTween()
    {
        
    }

    private void Start()
    {
        TestTween();
    }

    void UpdateSliderValue(float value)
    {
        slider.value = value;
    }
}
