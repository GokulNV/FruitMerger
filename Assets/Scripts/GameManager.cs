using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FruitData FruitData; // Reference to the FruitData asset
    [SerializeField] private FruitData _fruitSpawnOptions; // Reference to the FruitData asset

    [SerializeField] private GameObject _fruitPrefab; // Prefab for the fruit
    [SerializeField] private Transform _spawnParent; //spawn parent for the fruit clones
    [SerializeField] private float _spawnDelay = 2f; // Delay before spawning the next fruit

    private GameObject _currentFruit; // Currently spawned fruit
    private InputHandler _inputHandler; // Reference to the InputHandler

    [SerializeField] private bool _test;
    [SerializeField] private FruitType _nextSpawnType;

    private void Start()
    {
        _inputHandler = GetComponent<InputHandler>();
        
        // Subscribe to events from EventManager
        EventManager.OnFruitMoved += HandleFruitMoved;
        EventManager.OnFruitDropped += HandleFruitDropped;
        EventManager.FruitMergeEvent += HandleFruitMerge;
        SpawnFruit();
    }

    /// <summary>
    /// Spawns a new fruit at a random position and initializes it with random details.
    /// </summary>
    private void SpawnFruit()
    {
        // Instantiate the fruit prefab at a fixed height
        _currentFruit = Instantiate(_fruitPrefab, _spawnParent);
        Fruit fruit = _currentFruit.GetComponent<Fruit>();
        
        // Set the fruit in the input handler
        _inputHandler.SetCurrentFruit(_currentFruit);

        // Initialize with a random FruitDetail
        FruitDetail randomFruitDetail = _test ? FruitData.GetFruitDetailByType(_nextSpawnType) : GetRandomFruitDetail();
        if (randomFruitDetail != null)
        {
            fruit.Initialize(randomFruitDetail); // Only pass detail
        }
    }

    /// <summary>
    /// Handles the movement of the fruit.
    /// </summary>
    /// <param name="newPosition">The new position of the fruit.</param>
    private void HandleFruitMoved(Vector3 newPosition)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, -5f, 5f); // Adjust the bounds as needed
        _currentFruit.transform.position = newPosition;
    }

    /// <summary>
    /// Handles the dropping of the fruit.
    /// </summary>
    private void HandleFruitDropped()
    {
        // Call the Drop method on the fruit to allow it to fall
        _currentFruit.GetComponent<Fruit>().Drop();
        
        // Start coroutine to spawn a new fruit after a delay
        StartCoroutine(SpawnFruitAfterDelay(_spawnDelay));
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
        FruitDetail newFruitDetail = FruitData.GetFruitDetailByType(nextType);
        if (newFruitDetail != null)
        {
            // Initialize fruit1 with the new details
            fruit1.Initialize(newFruitDetail);
        }

        // Destroy the merged fruit
        Destroy(fruit2.gameObject);
    }

    /// <summary>
    /// Coroutine to spawn a new fruit after a specified delay.
    /// </summary>
    /// <param name="delay">The delay before spawning the next fruit.</param>
    private IEnumerator SpawnFruitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnFruit();
    }

    /// <summary>
    /// Retrieves a random FruitDetail from the available options.
    /// </summary>
    private FruitDetail GetRandomFruitDetail()
    {
        int randomIndex = Random.Range(0, _fruitSpawnOptions.Fruits.Count);
        return _fruitSpawnOptions.Fruits[randomIndex]; // Assuming FruitData has a List<FruitDetail> called Fruits
    }
}
