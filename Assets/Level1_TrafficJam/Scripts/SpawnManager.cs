using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private Transform[] _spawnPoints = new Transform[3];
    [SerializeField] private Sprite[] _carSprites = new Sprite[7];
    private int _commonPoint;
    private int _rndObstacle;
    private int _rndCollectable;

    private Coroutine _spawnObstacle;
    private Coroutine _spawnCollectable;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnObstacle = StartCoroutine(SpawnObstacle());
        _spawnCollectable = StartCoroutine(SpawnCollectable());
        
        Level1GameManager.Instance.OnGameOver.AddListener(KillCoroutines);
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            _rndObstacle = Random.Range(0, 3);
            var car = ObjectPoolManager.Instance.SpawnFromPool("Avoidable", _spawnPoints[_rndObstacle].position);
            car.GetComponent<ObstacleMovementController>().SpriteRenderer.sprite = _carSprites[Random.Range(0, 7)];
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator SpawnCollectable()
    {
        while (true)
        {
            _rndCollectable = Random.Range(0, 3);
            if (_rndObstacle != _rndCollectable)
            {
                ObjectPoolManager.Instance.SpawnFromPool("Collectable", _spawnPoints[_rndCollectable].position);
                yield return new WaitForSeconds(4);
            } 
        }
    }

    private void KillCoroutines()
    {
        StopCoroutine(_spawnObstacle);
        StopCoroutine(_spawnCollectable);
    }
}
