using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NotifyController : MonoBehaviour
{
    [SerializeField]
    SkeletonGraphic Spine;
    [SerializeField]
    List<SkeletonDataAsset> skeletonDataAssets = new List<SkeletonDataAsset>();

    float countdown;
    int index;

    private void Start()
    {
        countdown = 4f;
        index = 0;
        Spine.skeletonDataAsset = skeletonDataAssets[index];
        Spine.Initialize(true);
    }
    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            NextImage();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                NextImage();
            }
        }
        
    }

    void NextImage()
    {
        countdown = 1.5f;
        index++;
        if (index < skeletonDataAssets.Count)
        {
            Spine.skeletonDataAsset = skeletonDataAssets[index];
            Spine.Initialize(true);
        }
        else
        {
            OnDoneUpdate();
        }
    }

    public void OnDoneUpdate()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("IsUpdate" + Application.version, 1);
    }
}
