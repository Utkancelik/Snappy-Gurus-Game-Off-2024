using UnityEngine;

[CreateAssetMenu(fileName = "NewPool", menuName = "Object Pool/Pool")]
public class PoolScriptableObject : ScriptableObject
{
    public string tag;
    public GameObject prefab;
    public int size;
}

