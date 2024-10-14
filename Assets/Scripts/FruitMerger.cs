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
        Debug.Log("Collision happened");

        if (fruit2 != null && fruit1.FruitType == fruit2.FruitType)
        {
            // Invoke the FruitMerge event through EventManager
            EventManager.InvokeFruitMerge(fruit1, fruit2);
        }
    }
}
