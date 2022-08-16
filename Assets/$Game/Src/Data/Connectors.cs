using System.Collections;
using UnityEngine;

/// <summary>
/// Wrapper holds information related to 2 similar nodes
/// </summary>
[System.Serializable]
public class Connectors
{
    /// <summary>
    /// Node color
    /// </summary>
    [SerializeField]
    Color _Color = Color.white;

    public Color pColor
    { get { return _Color; } }

    /// <summary>
    /// 2 Nodes in the level for creating connection
    /// </summary>
    [SerializeField]
    Node[] _ConnectorNode = new Node[2];

    public Node[] pConnectorNode
    { get { return _ConnectorNode; } }
}
