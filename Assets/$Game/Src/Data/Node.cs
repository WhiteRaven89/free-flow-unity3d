using UnityEngine;

/// <summary>
/// Responsible for holding single node data
/// </summary>
[System.Serializable]
public class Node
{
    /// <summary>
    /// Coordinate of node in the grid
    /// </summary>
    [SerializeField]
    Vector2 _Coordinates = Vector2.zero;

    public Vector2 pCoordinates
    { get { return _Coordinates; } }

    public Node(int x, int y)
    {
        _Coordinates = new Vector2(x, y);
    }
}
