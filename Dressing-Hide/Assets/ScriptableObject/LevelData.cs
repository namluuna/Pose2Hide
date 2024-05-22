using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public List<Sprite> Background;
    public List<Sprite> Background_Frame;
    public List<Vector3> Positon_Frame;
    public int pose;
    public int time;
    public int money;
    public List<PoseAnswer> Answers;
    public List<int> Skin;
    public GameObject MapLevel;
    public GameObject BossLevel;
}

[System.Serializable]
public class PoseAnswer
{
    public string _character;
    public Vector3 _positon;
    public List<int> ListOfPose;
    public int _pose;
}

public enum ItemType
{
    Cloth,
    Pants,
    Bag,
    Hat
}