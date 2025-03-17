using System;
using UnityEngine;

public static class FruitTypeLogic
{
    /// <summary>
    /// Gets the next FruitType based on the current FruitType.
    /// </summary>
    /// <param name="currentType">The current FruitType.</param>
    /// <returns>The next FruitType.</returns>
    public static FruitType GetNextFruitType(FruitType currentType)
    {
        // Get all the values of the FruitType enum
        Array fruitTypes = Enum.GetValues(typeof(FruitType));
        
        // Find the current index
        int currentIndex = Array.IndexOf(fruitTypes, currentType);

        // Check if there is a next type
        if (currentIndex >= 0 && currentIndex < fruitTypes.Length - 1)
        {
            // Return the next FruitType
            return (FruitType)fruitTypes.GetValue(currentIndex + 1);
        }

        // If it's the last type, return the current type or handle accordingly
        return currentType; // Or return a default type if needed
    }
}
