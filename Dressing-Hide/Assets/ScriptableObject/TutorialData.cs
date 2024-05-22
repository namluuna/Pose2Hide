using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Tutorial", menuName = "ScriptableObject/Tutorial", order = 1)]
public class TutorialData : ScriptableObject
{
    public List<TutorialContent> ListTutorial;
}

[System.Serializable]
public class TutorialContent
{
    public string Name;
    public List<Sprite> sprites;
}
