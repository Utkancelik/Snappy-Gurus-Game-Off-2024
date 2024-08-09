using System.Collections;
using System.Collections.Generic;
using Level1_TrafficJam;
using UnityEngine;
using UnityEngine.UIElements;

public class L5EnemyMovementController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public LayerMask avoidableLayer;
    private Vector2 movementDirection;
    [SerializeField] private Transform _enemy;
    private bool _canRotate = true;

    void Start()
    {
        
        // Initialize movement direction to the right
        
        int direction = Random.Range(0,4);
        switch (direction)
        {
            case 0:
                movementDirection = Vector2.right;
                break;
            case 1:
                movementDirection = Vector2.left;
                break;
            case 2:
                movementDirection = Vector2.up;
                break;
            case 3:
                movementDirection = Vector2.down;
                break;
            default:
                movementDirection = Vector2.right;
                break;
        }
        
        
        
        StartCoroutine(RotateCooldown());
    }

    void Update()
    {
        // Move the enemy
        _enemy.transform.Translate(movementDirection * (moveSpeed * Time.deltaTime), Space.World);

        // Rotate the enemy to face the movement direction
        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            _enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with an object on the avoidable layer
        if ((collision.gameObject.layer == (int)Layers.Avoidable || collision.gameObject.layer == (int)Layers.Enemy))
        {
            // Rotate 180 degrees on collision
            Rotate90Degrees();
        }
    }

    private IEnumerator RotateCooldown()
    {
        while (true)
        {
            Rotate90Degrees();
            yield return new WaitForSeconds(2);
        }
    }

    void Rotate90Degrees()
    {
        // Reverse the movement direction
        movementDirection = new Vector2(-movementDirection.y, movementDirection.x);
    }
}
