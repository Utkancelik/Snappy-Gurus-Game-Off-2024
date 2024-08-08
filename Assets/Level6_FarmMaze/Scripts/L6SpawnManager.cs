using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class L6SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _fruitPrefabs = new(7);
    public Transform FruitsBase;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemyPrefabs();
    }

    private void InitializeEnemyPrefabs()
    {
        for (float x = -6.5f; x <= 6.5; x += 1.0f)
        {
            for (float y = -6.5f; y <= 6.5; y += 1.0f)
            {
                if (Random.Range(0, 3) == 1)
                {
                    GameObject fruit = Instantiate(_fruitPrefabs[Random.Range(0, _fruitPrefabs.Count)], new Vector3(x, y), _fruitPrefabs[0].transform.rotation);
                    fruit.transform.SetParent(FruitsBase);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
