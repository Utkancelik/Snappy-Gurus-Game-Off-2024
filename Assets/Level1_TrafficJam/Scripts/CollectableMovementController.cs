using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * _moveSpeed));    
        transform.Translate(Vector3.left* (Time.deltaTime * _moveSpeed));
        
        if(gameObject.transform.position.x < -10)
            ObjectPoolManager.Instance.ReturnToPool("Avoidable", gameObject);
    }
}
