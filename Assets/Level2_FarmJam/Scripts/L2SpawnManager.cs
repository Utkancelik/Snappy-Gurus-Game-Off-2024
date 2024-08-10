using System.Collections;
using UnityEngine;

public class L2SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints = new Transform[3];
    private int _commonPoint;
    private int _rndObstacle;

    private Coroutine _spawnObstacle;
    private Coroutine _spawnCollectable;
    
    
    void Start()
    {
        _spawnCollectable = StartCoroutine(SpawnCollectable());
        L2GameManager.Instance.OnGameOver.AddListener(KillCoroutines);
    }
    
    IEnumerator SpawnCollectable()
    {
        while (true)
        {
            ObjectPoolManager.Instance.SpawnFromPool("Collectable", _spawnPoints[Random.Range(0,_spawnPoints.Length)].position);
            yield return new WaitForSeconds(2);
        }
    }
    
    private void KillCoroutines()
    {
        StopCoroutine(_spawnCollectable);
    }
}
