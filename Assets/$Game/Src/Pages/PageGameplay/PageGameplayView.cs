using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// View component of gameplay UI
/// </summary>
public class PageGameplayView : UIPageView
{
    [SerializeField]
    Transform _BackgroundImg = null;

    [SerializeField]
    Transform _LevelGrid = null;

    [SerializeField]
    Transform _DotPrefab = null;

    [SerializeField]
    Button goToMenuBtn = null;

    public event System.Action OnMenuSelected;

    public override void Init()
    {
        base.Init();
    }

    public override void Enable()
    {
        base.Enable();
    }

    public override void Disable()
    {
        base.Disable();
    }

    public void DrawLevelData(Level level, int gridSize, InputHandler inputHandler)
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject levelGridObj = Instantiate(_LevelGrid.gameObject, _BackgroundImg.transform);
                LevelGrid levelGrid = levelGridObj.GetComponent<LevelGrid>();
                levelGrid.SetInGameGridData(x, y);
                for (int i = 0; i < level.pConnectors.Length; i++)
                {
                    Connectors connectors = level.pConnectors[i];
                    for (int j = 0; j < connectors.pConnectorNode.Length; j++)
                    {
                        Node node = connectors.pConnectorNode[j];
                        if(node.pCoordinates.x == x && node.pCoordinates.y == y)
                        {
                            GameObject dot = Instantiate(_DotPrefab.gameObject, levelGrid.transform);
                            Image dotColor = dot.GetComponent<Image>();
                            dotColor.color = connectors.pColor;
                            levelGrid.SetColorNodeFlag(i);
                            break;
                        }
                    }
                }
                levelGrid.RegisterInputEvents(inputHandler);
            }
        }
    }

    public void OnMenuBtnClicked()
    {
        if(OnMenuSelected != null)
        {
            OnMenuSelected.Invoke();
        }
    }
}
