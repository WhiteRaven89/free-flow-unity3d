using UnityEngine;

/// <summary>
/// Class holds responsibilty for Holding all levels data
/// </summary>
[System.Serializable]
public class LevelPack
{
    /// <summary>
    /// Grid size for level
    /// </summary>
    [SerializeField]
    int _GridSize = 5; //  Level size of 5x5

    public int pGridSize
    { get { return _GridSize; } }

    /// <summary>
    /// Array of level in one set
    /// </summary>
    [SerializeField]
    Level[] _Levels = null;

    public Level[] pLevels
    { get { return _Levels; } }
}
