using UnityEngine;

public class Fruit : MonoBehaviour
{
    public FruitType FruitType { get; private set; }

    [SerializeField] private ParticleSystem _mergeVFX;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Initializes the fruit with the given FruitDetail.
    /// </summary>
    /// <param name="detail">The FruitDetail to initialize the fruit with.</param>
    public void Initialize(FruitDetail detail)
    {
        FruitType = detail.FruitType;
        UpdateAppearance(detail);
        if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true; // Prevent it from falling until dropped
    }

    private void UpdateAppearance(FruitDetail detail)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = detail.FruitSprite;

        // Update the scale based on the size
        var scale = detail.InitialSize;
        transform.localScale = new Vector3(scale, scale, 1);
    }

    public void Drop()
    {
        _rigidbody.isKinematic = false; // Allow it to fall
    }

    public void PlayMergeVFX()
    {
        _mergeVFX.Play();
    }
}
