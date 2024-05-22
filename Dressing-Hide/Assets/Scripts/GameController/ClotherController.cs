using Spine.Unity.Examples;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotherController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonDataAsset skeletonDataAsset;
    public MixAndMatchSkinsExample skinsSystem;

    public FashionValue fashionValue;
    [SpineSkin(dataField: "skeletonDataAsset")] public string itemSkin;
    public MixAndMatchSkinsExample.ItemType itemType;

    public bool isSetSkin = false;
    Collider2D target_collision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target_collision = collision;
            isSetSkin = true;
        }
    }

    public void SetSkin()
    {
        if (isSetSkin)
        {
            skinsSystem = target_collision.GetComponent<MixAndMatchSkinsExample>();
            skinsSystem.Equip(itemSkin, itemType);
            FashionController.instance.ListSkinValue[itemType] = fashionValue;
            isSetSkin =false;   
        }
    }


    public void SetDataSkin(ClotherData data)
    {
        fashionValue = data.fashionValue;
        skeletonDataAsset = data.skeletonDataAsset;
        itemSkin = data.itemSkin;
        itemType = data.itemType;
        skeletonAnimation.initialSkinName = data.itemSkin;
        skeletonAnimation.Initialize(true);
    }

    private bool isDraggable = false;
    private Vector3 offset;

    public bool isDone = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            if (touch.phase == TouchPhase.Began)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isSetSkin = false;
                    isDraggable = true; // Chỉ kích hoạt kéo thả cho gameobject này
                    offset = transform.position - touchPosition;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDraggable)
            {
                Vector3 touchPosition1 = Camera.main.ScreenToWorldPoint(touch.position);
                transform.position = touchPosition1 + offset;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (isDraggable)
                {
                    SetSkin();
                    transform.localPosition = Vector3.zero;
                    isDraggable = false;
                }
            }
        }
    }
}
