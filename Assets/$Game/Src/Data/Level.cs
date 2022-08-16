using UnityEngine;

/// <summary>
/// Wrapper that holds information of level sets
/// </summary>
[System.Serializable]
public class Level
{
    /// <summary>
    /// Holds all nodes data with their respected color
    /// </summary>
    [SerializeField]
    Connectors[] _Connectors = null;

    public Connectors[] pConnectors
    { get { return _Connectors; } }
}
