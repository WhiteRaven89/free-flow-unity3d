using UnityEngine;

public delegate void OnMouseEnterEvent(LevelGrid grid);
public delegate void OnMouseExitEvent(LevelGrid grid);

/// <summary>
/// Class responsible for taking care of inputs
/// </summary>
public class InputHandler : MonoBehaviour 
{
    bool isMouseDown = false;

    public void MouseEnterEvent(LevelGrid grid)
    {
        GameManager.pInstance.ValidateConditions(grid, isMouseDown);
    }

    public void MouseExitEvent(LevelGrid grid)
    {
        GameManager.pInstance.ValidateConditions(grid, isMouseDown);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
#else
    if (Input.touchCount == 1)
    {
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            isMouseDown = true;
        }

        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            isMouseDown = false;
        }
    }
#endif
    }
}
