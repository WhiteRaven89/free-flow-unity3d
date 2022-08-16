using UnityEngine;

/// <summary>
/// Scriptable object resposible for saving levels
/// </summary>
[CreateAssetMenu(fileName = "LevelData", menuName = "CreateLevelData", order = 1)]
public class LevelDataSO : ScriptableObject
{
    /// <summary>
    /// List that holds array of all levels
    /// </summary>
    [SerializeField]
    LevelPack[] _LevelPack = null;

    public LevelPack[] pLevelPack
    { get { return _LevelPack; } /*set { _LevelPack = value; }*/ }
}
