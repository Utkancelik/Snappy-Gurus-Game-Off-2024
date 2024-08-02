using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "ScriptableObjects/Levels", order = 1)]
public class LevelsScriptableObject : ScriptableObject
{
    [SerializeField] public List<LevelInfo> _levelInfos;
}