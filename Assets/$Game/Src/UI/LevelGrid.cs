using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// In game grid
/// </summary>
public class LevelGrid : DataGrid, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Node _Node = null;

    public Node pNode
    { get { return _Node; } }

    [SerializeField]
    bool _HasColorNode = false;

    public bool pHasColorNode
    { get { return _HasColorNode; } }

    [SerializeField]
    bool _IsGridOccupied = false;

    public bool pIsGridOccupied
    { get { return _IsGridOccupied; } /*set { _IsGridOccupied = value; }*/ }

    [SerializeField]
    int _ColorIndex = -1;

    public int pColorIndex
    { get { return _ColorIndex; } /*set { _ColorIndex = value; }*/ }

    [SerializeField]
    LevelGrid _ParentGrid = null;

    public LevelGrid pParentGrid
    { get { return _ParentGrid; } /*set { _ParentGrid = value; }*/ }

    [SerializeField]
    LevelGrid _ChildGrid = null;

    public LevelGrid pChildGrid
    { get { return _ChildGrid; } /*set { _ChildGrid = value; }*/ }

    OnMouseEnterEvent onMouseEnterEvent = null;
    OnMouseExitEvent onMouseExitEvent = null;

    public void SetInGameGridData(int x, int y)
    {
        Button button = GetComponent<Button>();
        button.interactable = false;
        txtLevelNo.gameObject.SetActive(false);
        _Node = new Node(x,y);
    }

    public void SetColorNodeFlag(int aColorIndex)
    {
        _HasColorNode = true;
        _ColorIndex = aColorIndex;
        _IsGridOccupied = true;
    }

    public void RegisterInputEvents(InputHandler inputHandler)
    {
        this.onMouseEnterEvent = inputHandler.MouseEnterEvent;
        this.onMouseExitEvent = inputHandler.MouseExitEvent;
        GameManager.pInstance.pLevelGrid.Add(this);
    }

    public void DeRegisterInputEvents()
    {
        this.onMouseEnterEvent = null;
        this.onMouseExitEvent = null;
    }

    void OnDestroy()
    {
        DeRegisterInputEvents();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onMouseEnterEvent != null) onMouseEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onMouseExitEvent != null) onMouseExitEvent(this);
    }

    public void SetParameters(LevelGrid parent, bool isOccupied, int colorIndex)
    {
        _ParentGrid = parent;
        _IsGridOccupied = isOccupied;
        _ColorIndex = colorIndex;
    }

    public void SetChild(LevelGrid child)
    {
        _ChildGrid = child;
    }

    public void ResetParameters()
    {
        _ParentGrid = null;
        _ChildGrid = null;
        if(!pHasColorNode)
        {
            _IsGridOccupied = false;
            _ColorIndex = -1;
        }
    }
}
