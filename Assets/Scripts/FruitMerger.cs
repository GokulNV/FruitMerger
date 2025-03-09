using UnityEngine;

public class FruitMerger : MonoBehaviour
{
    /// <summary>
    /// Detects collisions with other fruit objects and triggers the merge event if applicable.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fruit fruit1 = GetComponent<Fruit>();
        Fruit fruit2 = collision.gameObject.GetComponent<Fruit>();

        if (fruit2 != null && fruit1.FruitType == fruit2.FruitType)
        {
            // Invoke the FruitMerge event through EventManager
            EventManager.InvokeFruitMerged(fruit1, fruit2);
        }

        if(fruit1.IsInitialCollision)
        {
           fruit1.IsInitialCollision = false;
           EventManager.InvokeNextSpawn(); //ToDo: change this logic later. this to initiate next spawn. instead do it after dropping.
        }
    }
}
