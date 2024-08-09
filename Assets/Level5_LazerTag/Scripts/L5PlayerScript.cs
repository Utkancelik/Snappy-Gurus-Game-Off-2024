using System;
using System.Collections;
using System.Collections.Generic;
using Level1_TrafficJam;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class L5PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    [SerializeField] private Transform _barrelTransform;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ObjectPoolManager.Instance.SpawnFromPool(nameof(Layers.Bullet), _barrelTransform.position,
                _barrelTransform.rotation);
        
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
        if (other.gameObject.layer == (int)Layers.Collectable)
        {
            UIManager.Instance.ToggleWinMenu();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == (int)Layers.Enemy)
        {
            UIManager.Instance.ToggleLoseMenu();
            Destroy(gameObject);
        }
    }
}
