using Level;
using Level1_TrafficJam;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class L6PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    [SerializeField] private Transform _fruitsCount;
    [SerializeField] private int _childCount;
    public EmotionController.Character character;

    private void Start()
    {
        _childCount = _fruitsCount.childCount;
        AudioManager.Instance.PlayMusicClip("Episode6");
    }

    void Update()
    {
        // Input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Rotate the player to face the movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void FixedUpdate()
    {
        // Move the player
        transform.Translate(movement * (moveSpeed * Time.fixedDeltaTime), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _childCount -= 1;
        
        if (other.gameObject.layer == (int)Layers.Collectable)
        {
            AudioManager.Instance.PlaySFXClip("Collect1");
            Destroy(other.gameObject);
        }

        if (_childCount <= 0)
        {
            UIManager.Instance.ToggleWinMenu();
            LevelManager.Instance.LevelCompleted(character);
        }
            
    }
}
