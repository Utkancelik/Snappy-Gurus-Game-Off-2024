using Level1_TrafficJam;
using UnityEngine;

[SelectionBase]
public class L1ObstacleMovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] public SpriteRenderer SpriteRenderer;

    private void Start()
    {
        L1GameManager.Instance.OnGameOver.AddListener(OnGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * _moveSpeed));    
        transform.Translate(Vector3.left* (Time.deltaTime * _moveSpeed));
        
        if(gameObject.transform.position.x < -10)
            ObjectPoolManager.Instance.ReturnToPool("Avoidable", gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int)Layers.Collectable)
        {
            ObjectPoolManager.Instance.ReturnToPool(nameof(Layers.Avoidable), gameObject );
        }
    }
    private void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}
