using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _dragAreaRect; 
    
    private GameObject _currentFruit; // Reference to the current fruit being moved
    private bool _isDragging;

    private void Start()
    {
        EventManager.OnFruitInitialise += SetCurrentFruit;
    } 

    /// <summary>
    /// Sets the current fruit being manipulated.
    /// </summary>
    /// <param name="fruit">The fruit to be set as the current fruit.</param>
    private void SetCurrentFruit(GameObject fruit)
    {
        _currentFruit = fruit;
    }

    private void Update()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPosition.z = 0; // Ensure z-axis is zero

        // Handle input for moving the fruit
        if (Input.GetMouseButton(0) && _currentFruit != null && (IsTouchInsideArea(touchPosition) || _isDragging))
        {
            _isDragging = true;
            touchPosition.y = _currentFruit.transform.position.y; // Keep the same y position
            _currentFruit.transform.position = touchPosition; // Move the current fruit directly
        }

        // Handle input for dropping the fruit
        else if (Input.GetMouseButtonUp(0) && _currentFruit != null && _isDragging)
        {
            EventManager.InvokeFruitDropped();
            _currentFruit = null; // Clear the current fruit
        }
    }

   /// <summary>
    /// Checks if the touch or mouse click is inside the drag area (RectTransform).
    /// </summary>
    private bool IsTouchInsideArea(Vector3 touchPosition)
    {
        // Get the world corners of the RectTransform (UI element)
        Vector3[] worldCorners = new Vector3[4];
        _dragAreaRect.GetWorldCorners(worldCorners);

        float minX = worldCorners[0].x;
        float maxX = worldCorners[2].x;
        float minY = worldCorners[0].y;
        float maxY = worldCorners[2].y;

        // Check if the touch position is inside the bounds of the RectTransform
        return touchPosition.x >= minX && touchPosition.x <= maxX && touchPosition.y >= minY && touchPosition.y <= maxY;
    }
}
