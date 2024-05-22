using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character",order =1)]
public class CharacterData : ScriptableObject
{
    public string CharacterId;
    public List<SkeletonDataAsset> ListOfAnswer;

    public List<SkeletonDataAsset> ListOfAnime;

    public List<SkinData> ListSkinData;

}


[Serializable]
public class SkinData
{
    public string Name; 
    public Sprite Avatar;
    public int Price;
}