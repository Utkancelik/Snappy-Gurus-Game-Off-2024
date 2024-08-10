using System;
using System.Collections;
using System.Collections.Generic;
using Level1_TrafficJam;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class L1PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _car;
    [SerializeField] private Transform[] _lanes = new Transform[3];
    [SerializeField] private int _currentLane;

    private int CurrentLane
    {
        get => _currentLane;
        set
        {
            if (value < 3 && value >= 0)
            {
                _currentLane = value;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _currentLane = 1;
        L1GameManager.Instance.OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            CurrentLane += 1;
            _car.position = _lanes[CurrentLane].position;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            CurrentLane -= 1;
            _car.position = _lanes[CurrentLane].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == (int)Layers.Collectable)
        {
            Debug.Log($"Collect{Random.Range(0,2).ToString()}");
            AudioManager.Instance.PlaySFXClip($"Collect{Random.Range(1,3).ToString()}");
            L1GameManager.Instance.UpdatePlayerScore();
            ObjectPoolManager.Instance.ReturnToPool(nameof(Layers.Collectable), other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == (int)Layers.Avoidable)
        {
            L1GameManager.Instance.TriggerLoseGame();
            gameObject.SetActive(false);
        }
    }
}
