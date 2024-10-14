using System;
using UnityEngine;

public static class EventManager
{
    // Delegate for fruit merging events
    public delegate void OnFruitMerge(Fruit fruit1, Fruit fruit2);
    public static event OnFruitMerge FruitMergeEvent;

    // Delegate for fruit moving events
    public delegate void FruitMovedHandler(Vector3 newPosition);
    public static event FruitMovedHandler OnFruitMoved;

    public delegate void FruitDroppedHandler();
    public static event FruitDroppedHandler OnFruitDropped;

    /// <summary>
    /// Invokes the FruitMerge event.
    /// </summary>
    /// <param name="fruit1">The first fruit involved in the merge.</param>
    /// <param name="fruit2">The second fruit involved in the merge.</param>
    public static void InvokeFruitMerge(Fruit fruit1, Fruit fruit2)
    {
        FruitMergeEvent?.Invoke(fruit1, fruit2);
    }

    /// <summary>
    /// Invokes the FruitMoved event.
    /// </summary>
    public static void InvokeFruitMoved(Vector3 newPosition)
    {
        OnFruitMoved?.Invoke(newPosition);
    }

    /// <summary>
    /// Invokes the FruitDropped event.
    /// </summary>
    public static void InvokeFruitDropped()
    {
        OnFruitDropped?.Invoke();
    }
}