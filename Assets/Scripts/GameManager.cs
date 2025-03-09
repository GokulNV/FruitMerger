using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FruitData FruitDataAsset; // Reference to the FruitData asset
    private GameObject _currentFruit; // Currently spawned fruit

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {        
        // Subscribe to events from EventManager
        EventManager.OnFruitInitialise += InitialiseCurrentFruit;
        EventManager.OnFruitDroppedEvent += HandleFruitDropped;
        EventManager.OnFruitMergeEvent += HandleFruitMerge;
    }

    private void InitialiseCurrentFruit(GameObject fruit)
    {
        _currentFruit = fruit;
    }

    /// <summary>
    /// Handles the dropping of the fruit.
    /// </summary>
    private void HandleFruitDropped()
    {
        // Call the Drop method on the fruit to allow it to fall
        _currentFruit.GetComponent<Fruit>().Drop();
    }

    /// <summary>
    /// Handles the merging of fruits when the merge event is triggered.
    /// </summary>
    /// <param name="fruit1">The first fruit involved in the merge.</param>
    /// <param name="fruit2">The second fruit involved in the merge.</param>
    private void HandleFruitMerge(Fruit fruit1, Fruit fruit2)
    {
        // Find the next fruit type for the merging fruit
        FruitType nextType = FruitTypeLogic.GetNextFruitType(fruit1.FruitType);

        // Get the corresponding FruitDetail for the next fruit type
        FruitDetail newFruitDetail = FruitDataAsset.GetFruitDetailByType(nextType);
        if (newFruitDetail != null)
        {
            // Initialize fruit1 with the new details
            fruit1.Initialize(newFruitDetail);
        }

        // Destroy the merged fruit
        Destroy(fruit2.gameObject);
    }
}