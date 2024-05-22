using Spine.Unity;
using Spine.Unity.Examples;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clother", menuName = "ScriptableObject/Clother", order =1)]
public class ClotherData : ScriptableObject
{
    public string ClotherDataId;
    public SkeletonDataAsset skeletonDataAsset;
    [SpineSkin(dataField: "skeletonDataAsset")] public string itemSkin;
    public MixAndMatchSkinsExample.ItemType itemType;

    public FashionValue fashionValue;
}
