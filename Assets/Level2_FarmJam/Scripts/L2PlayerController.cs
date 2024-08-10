using System.Collections.Generic;
using Level1_TrafficJam;
using UnityEngine;
using Random = UnityEngine.Random;

public class L2PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _car;
    [SerializeField] private Transform[] _lanes = new Transform[3];
    [SerializeField] private int _currentLane;
    [SerializeField] private List<GameObject> _hays = new(9);
    private int _hayIndexToActivate = 0;

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
        gameObject.transform.position = _lanes[1].position;
        _currentLane = 1;
        L2GameManager.Instance.OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if(L2GameManager.Instance.IsGameOver)
            return;
        
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
            _hays[_hayIndexToActivate].SetActive(true);
            _hayIndexToActivate += 1;
            
            L2GameManager.Instance.UpdatePlayerScore();
            AudioManager.Instance.PlaySFXClip($"Collect{Random.Range(1,3).ToString()}");
            ObjectPoolManager.Instance.ReturnToPool(nameof(Layers.Collectable), other.gameObject);
        }
    }
}
