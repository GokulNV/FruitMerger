using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private GameObject _currentFruit; // Reference to the current fruit being moved

    /// <summary>
    /// Sets the current fruit being manipulated.
    /// </summary>
    /// <param name="fruit">The fruit to be set as the current fruit.</param>
    public void SetCurrentFruit(GameObject fruit)
    {
        _currentFruit = fruit;
    }

    private void Update()
    {
        // Handle input for moving the fruit
        if (Input.GetMouseButton(0) && _currentFruit != null)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0; // Ensure z-axis is zero
            touchPosition.y = _currentFruit.transform.position.y; // Keep the same y position
            EventManager.InvokeFruitMoved(touchPosition);
            _currentFruit.transform.position = touchPosition; // Move the current fruit directly
        }

        // Handle input for dropping the fruit
        else if (Input.GetMouseButtonUp(0) && _currentFruit != null)
        {
            EventManager.InvokeFruitDropped();
            _currentFruit = null; // Clear the current fruit
        }
    }
}
