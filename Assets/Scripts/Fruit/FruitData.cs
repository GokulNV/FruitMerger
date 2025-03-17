using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Fruit/FruitData", order = 1)]
public class FruitData : ScriptableObject
{
    public List<FruitDetail> Fruits = new List<FruitDetail>(); // List of all fruit details

    /// <summary>
    /// Gets the FruitDetail based on the provided FruitType.
    /// </summary>
    public FruitDetail GetFruitDetailByType(FruitType type)
    {
        foreach (var detail in Fruits)
        {
            if (detail.FruitType == type)
            {
                return detail;
            }
        }
        return null; // Return null if not found
    }
}

[Serializable]
public class FruitDetail
{
    public FruitType FruitType; // Use the FruitType enum
    public float InitialSize; // Starting size of the fruit
    public Sprite FruitSprite; // Sprite for the fruit appearance
    public int ScoreForMerge; // Score awarded for each merge by this type 
}

[Serializable]
public enum FruitType
{
    Blueberry,
    Strawberry,
    Grapes,
    Peach,
    Lemon,
    Orange,
    Apple,
    Pineapple,
    Watermelon
}
