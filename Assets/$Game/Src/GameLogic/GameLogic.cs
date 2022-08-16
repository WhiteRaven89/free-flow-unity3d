using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class responsible for game logic of free flow
/// </summary>
public class GameLogic
{
    [SerializeField]
    List<LevelGrid> _LevelGrid = null;

    [SerializeField]
    LevelGrid previousGrid = null;

    public List<LevelGrid> pLevelGrid
    { get { return _LevelGrid; } set { _LevelGrid = value; } }

    public void Initialize()
    {
        _LevelGrid = GameManager.pInstance.pLevelGrid;
    }

    public void ApplyGameLogic(LevelGrid levelGrid, bool isMouseDown)
    {
        if (previousGrid != null)
        {
            if(previousGrid.pNode.pCoordinates.x != levelGrid.pNode.pCoordinates.x || previousGrid.pNode.pCoordinates.y != levelGrid.pNode.pCoordinates.y)
            {
                //  check if previous node is neighbour to current node
                if (IsNeighbourNode(levelGrid.pNode, previousGrid.pNode))
                {
                    if (previousGrid.pHasColorNode)
                    {
                        if (!levelGrid.pHasColorNode)
                        {
                            if(!levelGrid.pIsGridOccupied)
                            {
                                List<Vector3> positions = new List<Vector3>();
                                positions.Add(previousGrid.transform.position);
                                positions.Add(levelGrid.transform.position);
                                GameManager.pInstance.DrawLine(previousGrid.pColorIndex, positions.ToArray());
                                levelGrid.SetParameters(previousGrid, true, previousGrid.pColorIndex);
                                previousGrid.SetChild(levelGrid);
                            }
                            else
                            {
                                int colorIndex = levelGrid.pColorIndex;
                                LevelGrid childGrid = levelGrid.pChildGrid;
                                LevelGrid parentGrid = levelGrid.pParentGrid;
                                LevelGrid currGrid = levelGrid;
                                while (childGrid != null)
                                {
                                    childGrid = currGrid.pChildGrid;
                                    currGrid.ResetParameters();
                                    currGrid = childGrid;
                                }

                                List<Vector3> positions = new List<Vector3>();
                                currGrid = levelGrid;
                                while (parentGrid != null)
                                {
                                    positions.Add(parentGrid.transform.position);
                                    currGrid = parentGrid;
                                    parentGrid = currGrid.pParentGrid;
                                }
                                positions.Reverse();
                                GameManager.pInstance.DrawLine(colorIndex, positions.ToArray());

                                List<Vector3> newLinepositions = new List<Vector3>();
                                newLinepositions.Add(previousGrid.transform.position);
                                newLinepositions.Add(levelGrid.transform.position);
                                GameManager.pInstance.DrawLine(previousGrid.pColorIndex, newLinepositions.ToArray());
                                levelGrid.SetParameters(previousGrid, true, previousGrid.pColorIndex);
                                previousGrid.SetChild(levelGrid);
                            }
                        }
                        else
                        {
                            if (levelGrid.pIsGridOccupied && previousGrid.pColorIndex != levelGrid.pColorIndex)
                            {
                                
                            }
                            else {}
                        }
                    }
                    else
                    {
                        if(previousGrid.pIsGridOccupied)
                        {
                            int colorIndex = previousGrid.pColorIndex;
                            if (levelGrid.pHasColorNode && levelGrid.pColorIndex == previousGrid.pColorIndex)
                            {
                                List<Vector3> positions = new List<Vector3>();
                                levelGrid.SetParameters(previousGrid, true, previousGrid.pColorIndex);
                                previousGrid.SetChild(levelGrid);
                                LevelGrid parentGrid = levelGrid.pParentGrid;
                                LevelGrid currGrid = levelGrid;
                                while (parentGrid != null)
                                {
                                    positions.Add(parentGrid.transform.position);
                                    currGrid = parentGrid;
                                    parentGrid = currGrid.pParentGrid;
                                }
                                positions.Reverse();
                                positions.Add(levelGrid.transform.position);
                                GameManager.pInstance.DrawLine(colorIndex, positions.ToArray());
                                GameManager.pInstance.CheckForGameFinish();
                            }
                            else if (levelGrid.pIsGridOccupied)
                            {
                                if (levelGrid.pColorIndex != previousGrid.pColorIndex && !levelGrid.pHasColorNode)
                                {
                                    colorIndex = levelGrid.pColorIndex;
                                    LevelGrid childGrid = levelGrid.pChildGrid;
                                    LevelGrid parentGrid = levelGrid.pParentGrid;
                                    LevelGrid currGrid = levelGrid;
                                    while (childGrid != null)
                                    {
                                        childGrid = currGrid.pChildGrid;
                                        currGrid.ResetParameters();
                                        currGrid = childGrid;
                                    }

                                    List<Vector3> positions = new List<Vector3>();
                                    currGrid = levelGrid;
                                    while (parentGrid != null)
                                    {
                                        positions.Add(parentGrid.transform.position);
                                        currGrid = parentGrid;
                                        parentGrid = currGrid.pParentGrid;
                                    }
                                    positions.Reverse();
                                    GameManager.pInstance.DrawLine(colorIndex, positions.ToArray());

                                    List<Vector3> newLinepositions = new List<Vector3>();
                                    levelGrid.SetParameters(previousGrid, true, previousGrid.pColorIndex);
                                    previousGrid.SetChild(levelGrid);
                                    parentGrid = levelGrid.pParentGrid;
                                    currGrid = levelGrid;
                                    while (parentGrid != null)
                                    {
                                        newLinepositions.Add(parentGrid.transform.position);
                                        currGrid = parentGrid;
                                        parentGrid = currGrid.pParentGrid;
                                    }
                                    newLinepositions.Reverse();
                                    newLinepositions.Add(levelGrid.transform.position);
                                    GameManager.pInstance.DrawLine(previousGrid.pColorIndex, newLinepositions.ToArray());
                                }
                            }
                            else if(!levelGrid.pIsGridOccupied)
                            {
                                List<Vector3> positions = new List<Vector3>();
                                levelGrid.SetParameters(previousGrid, true, previousGrid.pColorIndex);
                                previousGrid.SetChild(levelGrid);
                                LevelGrid parentGrid = levelGrid.pParentGrid;
                                LevelGrid currGrid = levelGrid;
                                while (parentGrid != null)
                                {
                                    positions.Add(parentGrid.transform.position);
                                    currGrid = parentGrid;
                                    parentGrid = currGrid.pParentGrid;
                                }
                                positions.Reverse();
                                positions.Add(levelGrid.transform.position);
                                GameManager.pInstance.DrawLine(colorIndex, positions.ToArray());
                            }
                            else
                            {
                                Debug.LogError("Condition not evaluated");
                            }
                        }
                        else
                        {}
                    }
                }
                else {}
                previousGrid = levelGrid;
            }
            else
            {
                //  Same Node
            }
        }
        else
        {
            if(isMouseDown)
            {
                previousGrid = levelGrid;
            }
        }
    }

    public void SetPreviousNodeEmpty()
    {
        if(previousGrid != null)
        {
            previousGrid = null;
        }
    }

    bool IsNeighbourNode(Node node, Node neighbour)
    {
        int rows = GameManager.pInstance.pGridSize;
        int columns = GameManager.pInstance.pGridSize;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)    //  Same node
                    continue;

                int xCoord = (int)node.pCoordinates.x + x;
                int yCoord = (int)node.pCoordinates.y + y;

                if(xCoord >= 0 && xCoord < rows && yCoord >= 0 && yCoord < columns)
                {
                    if(xCoord == neighbour.pCoordinates.x && yCoord == neighbour.pCoordinates.y)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
