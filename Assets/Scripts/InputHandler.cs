using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _dragAreaRect;
    
    private Camera _mainCamera;
    private Transform _currentFruitTransform;
    private float _fruitRadius;
    private float _leftBound;
    private float _rightBound;
    private float _screenWidth;
    private bool _isDragging;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _screenWidth = Screen.width;

        _leftBound = -(_dragAreaRect.rect.width/2);
        _rightBound = _dragAreaRect.rect.width/2;
    }

    private void OnEnable()
    {
        EventManager.OnFruitInitialise += SetCurrentFruit;
    } 

    private void OnDisable()
    {
        EventManager.OnFruitInitialise -= SetCurrentFruit;
    }

    /// <summary>
    /// Sets the current fruit being manipulated.
    /// </summary>
    /// <param name="fruit">The fruit to be set as the current fruit.</param>
    private void SetCurrentFruit(GameObject fruit)
    {
        _currentFruitTransform = fruit.transform;
        _fruitRadius = GetCircleRadiusInUISpace(fruit);
    }

    private void Update()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPosition.z = 0; // Ensure z-axis is zero

        if(_currentFruitTransform == null)
        {
            return;
        }

        // Handle input for moving the fruit
        if (Input.GetMouseButton(0) && IsTouchInsideArea(touchPosition))
        {
            _isDragging = true;
            touchPosition.y = _currentFruitTransform.position.y; // Keep the same y position

            if(IsWithinBounds(touchPosition))
            {
                _currentFruitTransform.position = touchPosition; // Move the current fruit directly
            }
        }
        // Handle input for dropping the fruit
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            EventManager.InvokeFruitDropped();
            _isDragging = false;
            _currentFruitTransform = null;
        }
    }

    /// <summary>
    /// Checks if the touch or mouse click is inside the drag area (RectTransform).
    /// </summary>
    /// ToDo: lot of world to screen and screen to world conversions. cleanup the logic
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

    private bool IsWithinBounds(Vector3 newPos)
    {
        // Convert the fruit's world position to screen space
        var xScreenPos = _mainCamera.WorldToScreenPoint(newPos).x;
        var xPos = -(_screenWidth / 2 - xScreenPos);

        return xPos - _fruitRadius > _leftBound && xPos + _fruitRadius < _rightBound;
    }

    private float GetCircleRadiusInUISpace(GameObject fruit)
    {
        // Get the CircleCollider2D component
        CircleCollider2D collider = fruit.GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            Debug.LogError("CircleCollider2D not found on object.");
            return 0f;
        }

        // Get the radius in world space (accounts for object scale)
        float worldRadius = collider.radius * _currentFruitTransform.localScale.x;

        // Get the object's position in world space
        Vector3 objectWorldPosition = fruit.transform.position;

        // Convert the object's center and the radius point from world space to screen space
        Vector3 objectScreenPosition = _mainCamera.WorldToScreenPoint(objectWorldPosition);
        Vector3 radiusWorldPosition = objectWorldPosition + new Vector3(worldRadius, 0f, 0f); // Adding the radius to the x-axis
        Vector3 radiusScreenPosition = _mainCamera.WorldToScreenPoint(radiusWorldPosition);

        // The screen radius is the distance between the object's center in screen space and the edge (radius) in screen space
        return Vector2.Distance(objectScreenPosition, radiusScreenPosition);
    }
}
