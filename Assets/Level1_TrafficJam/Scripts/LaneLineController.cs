using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneLineController : MonoBehaviour
{
    [SerializeField] private Transform _upLaneLine;
    [SerializeField] private Transform _downLaneLine;
    [SerializeField] private float _lineSpeed;
    [SerializeField] private float _threshold;
    
    void Update()
    {
        if(Level1GameManager.Instance.IsGameOver)
            return;
        
        _upLaneLine.Translate(Vector3.left * (Time.deltaTime * _lineSpeed));    
        _downLaneLine.Translate(Vector3.left* (Time.deltaTime * _lineSpeed));

        if (_upLaneLine.position.x <= _threshold)
        {
            _upLaneLine.position = new Vector2(0, _upLaneLine.position.y);
            _downLaneLine.position = new Vector2(0, _downLaneLine.position.y);
        }
    }
}
