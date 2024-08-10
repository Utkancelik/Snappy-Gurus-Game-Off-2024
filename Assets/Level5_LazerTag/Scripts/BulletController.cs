using Level1_TrafficJam;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _barrelPosition;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * 3));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == (byte)Layers.Avoidable)
        {
            ObjectPoolManager.Instance.ReturnToPool(nameof(Layers.Bullet), gameObject);
        }
        
        if (other.gameObject.layer == (byte)Layers.Enemy)
        {
            ObjectPoolManager.Instance.ReturnToPool(nameof(Layers.Bullet), gameObject);
            Destroy(other.gameObject);
        }
    }
}
