using UnityEngine;

public class AlexMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject flashlight;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flashlight.SetActive(false); // Başlangıçta fener kapalı
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        movement = new Vector2(moveX, 0);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RepairZone"))
        {
            FindObjectOfType<OfficeCableGameController>().StartRepairGame();
        }
    }

    public void ActivateFlashlight()
    {
        flashlight.SetActive(true);
    }

    public void DeactivateFlashlight()
    {
        flashlight.SetActive(false);
    }
}