using UnityEngine;

public static class EventManager
{
    public delegate void FruitInitialiseDelegate(GameObject fruitObj);
    public static event FruitInitialiseDelegate OnFruitInitialise;

    public delegate void FruitDroppedDelegate();
    public static event FruitDroppedDelegate OnFruitDroppedEvent;

    public delegate void FruitMergeDelegate(Fruit fruit1, Fruit fruit2);
    public static event FruitMergeDelegate OnFruitMergeEvent;

    public delegate void NextSpawnDelegate();
    public static event NextSpawnDelegate OnNextSpawnEvent;

    public delegate void UpdateScoreDelegate(int score);
    public static event UpdateScoreDelegate OnUpdateScoreEvent;
    public static event UpdateScoreDelegate OnUpdateHighScoreEvent;

    /// <summary>
    /// Invokes the FruitMerge event.
    /// </summary>
    /// <param name="fruit1">The first fruit involved in the merge.</param>
    /// <param name="fruit2">The second fruit involved in the merge.</param>
    public static void InvokeFruitInitialise(GameObject fruitObj)
    {
        OnFruitInitialise?.Invoke(fruitObj);
    }

    /// <summary>
    /// Invokes the FruitDropped event.
    /// </summary>
    public static void InvokeFruitDropped()
    {
        OnFruitDroppedEvent?.Invoke();
    }

    /// <summary>
    /// Invokes the FruitMerge event.
    /// </summary>
    /// <param name="fruit1">The first fruit involved in the merge.</param>
    /// <param name="fruit2">The second fruit involved in the merge.</param>
    public static void InvokeFruitMerged(Fruit fruit1, Fruit fruit2)
    {
        OnFruitMergeEvent?.Invoke(fruit1, fruit2);
    }

    public static void InvokeNextSpawn()
    {
        OnNextSpawnEvent?.Invoke();
    }

    public static void InvokeUpdateScore(int score)
    {
        OnUpdateScoreEvent?.Invoke(score);
    }

    public static void InvokeUpdateHighScore(int score)
    {
        OnUpdateHighScoreEvent?.Invoke(score);
    }
}