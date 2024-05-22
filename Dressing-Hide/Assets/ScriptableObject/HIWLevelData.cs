using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HIWLevel", menuName = "ScriptableObject/HIWLevel", order = 1)]
public class HIWLevelData : ScriptableObject
{
    public int pose;
    public List<ListHIWAnswer> Answers;
    public List<HIWAnswerSpawn> Characters;
    public ListWall Walls;
    public int money;
}

[System.Serializable]
public class HIWAnswer
{
    public string _character;
    public int _pose;
}
[System.Serializable]
public class HIWAnswerSpawn
{
    public string _character;
    public List<int> _pose;
}

[System.Serializable]
public class ListHIWAnswer
{
    public List<HIWAnswer> _listAnswer;
}


[System.Serializable]
public class ListWall
{
    public List<Sprite> Sprites;
    public List<int> Anime;
}