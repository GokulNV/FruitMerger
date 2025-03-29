using UnityEngine;

public class FruitMerger : MonoBehaviour
{
    private Fruit _thisFruit;
    private void Awake()
    {
        _thisFruit = GetComponent<Fruit>();
    }

    /// <summary>
    /// Detects collisions with other fruit objects and triggers the merge event if applicable.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var layer = collision.gameObject.layer;
        if(layer == LayerMask.NameToLayer("Fruit"))
        {
            Fruit fruit2 = collision.gameObject.GetComponent<Fruit>();
            if (fruit2 != null && _thisFruit.FruitType == fruit2.FruitType)
            {
                EventManager.InvokeFruitMerged(_thisFruit, fruit2);
            }
        }
    }
}
