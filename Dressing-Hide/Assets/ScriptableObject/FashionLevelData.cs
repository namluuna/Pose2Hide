using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FashionLevelData", menuName = "ScriptableObject/FashionLevelData", order = 1)]
public class FashionLevelData : ScriptableObject
{
    public FashionValue FashionValue;
    
}

[System.Serializable]
public class FashionValue
{
    public float Elegant;
    public float Sexy;
    public float Cute;
    public float Stylish;
    public float Warm;

    public float CalculateTotal()
    {
        return Elegant + Sexy + Cute + Stylish + Warm;
    }
}
