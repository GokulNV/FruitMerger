using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private FruitData _fruitSpawnOptions; // Reference to the FruitData asset that is used to spawn

    [SerializeField] private GameObject _fruitPrefab; // Prefab for the fruit
    [SerializeField] private Transform _spawnParent; //spawn parent for the fruit clones
    [SerializeField] private float _spawnDelay = 1.5f; // Delay before spawning the next fruit
    [SerializeField] private Image _nextSpawnFruitImg;

    private FruitDetail _nextFruitDetail;
    private GameObject _currentFruit; // Currently spawned fruit

    private void OnEnable()
    {       
        // Subscribe to events from EventManager
        EventManager.OnNextSpawnEvent += StartTimerForNextSpawn;
        EventManager.OnInitialiseGame += InitialiseFirstSpawn;
    }

    private void OnDisable()
    {       
        // Subscribe to events from EventManager
        EventManager.OnNextSpawnEvent -= StartTimerForNextSpawn;
        EventManager.OnInitialiseGame -= InitialiseFirstSpawn;
    }

    private void InitialiseFirstSpawn()
    {
        
    }

    public void StartSpawn()
    {
        SpawnFruit();
    }

    /// <summary>
    /// Spawns a new fruit at a random position and initializes it with random details.
    /// </summary>
    private void SpawnFruit()
    {      
        // Instantiate the fruit prefab at a fixed height
        _currentFruit = Instantiate(_fruitPrefab, _spawnParent);
        EventManager.InvokeFruitInitialise(_currentFruit);
        
        // Initialize with FruitDetail
        Fruit fruit = _currentFruit.GetComponent<Fruit>();
        FruitDetail fruitDetail;
        if (_nextFruitDetail == null)
            fruitDetail = GetRandomFruitDetail();
        else
            fruitDetail = _nextFruitDetail; 
       
        fruit.Initialize(fruitDetail);
        SetNextFruitDetails();
    }

    /// <summary>
    /// Handles the dropping of the fruit.
    /// </summary>
    private void StartTimerForNextSpawn()
    {        
        // Start coroutine to spawn a new fruit after a delay
        StartCoroutine(SpawnFruitAfterDelay(_spawnDelay));
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

    private void SetNextFruitDetails()
    {
        _nextFruitDetail = GetRandomFruitDetail();
        _nextSpawnFruitImg.sprite = _nextFruitDetail.FruitSprite;
    }
}
