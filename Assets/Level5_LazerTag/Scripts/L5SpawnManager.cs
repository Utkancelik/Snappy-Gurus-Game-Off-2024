using UnityEngine;

public class L5SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemyPrefabs();
    }

    private void InitializeEnemyPrefabs()
    {
        for (float x = -6.5f; x <= 6.5; x += 2.0f)
        {
            for (float y = -6.5f; y <= 6.5; y += 2.0f)
            {
                if (Random.Range(0, 3) == 1)
                {
                    Instantiate(_enemyPrefab, new Vector3(x, y), _enemyPrefab.transform.rotation);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
