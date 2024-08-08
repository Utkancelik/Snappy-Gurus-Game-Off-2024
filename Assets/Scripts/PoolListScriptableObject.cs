using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPoolList", menuName = "Object Pool/Pool List")]
public class PoolListScriptableObject : ScriptableObject
{
    public List<PoolScriptableObject> pools;
}
