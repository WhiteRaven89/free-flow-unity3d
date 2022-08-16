using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager taking care of game states
/// </summary>
public class GameManager : MonoBehaviour 
{
    private static GameManager _instance = null;

    public static GameManager pInstance
    {
        get { return _instance; }
    }

    [SerializeField]
    LevelPack[] _LevelPack = null;

    public LevelPack[] pLevelPack
    { get { return _LevelPack; } }

    [SerializeField]
    int _CurrLevelSize = 0;

    [SerializeField]
    int _CurrLevel = 0;

    public int pGridSize
    { get { return _LevelPack[_CurrLevelSize].pGridSize; } }

    public Level pCurrentLevel
    { get { return _LevelPack[_CurrLevelSize].pLevels[_CurrLevel]; }}

    [SerializeField]
    InputHandler _InputHandler = null;

    [SerializeField]
    List<LevelGrid> _LevelGrid = null;

    public List<LevelGrid> pLevelGrid
    { get { return _LevelGrid; } set { _LevelGrid = value; } }

    [SerializeField]
    GameLogic _GameLogic = null;

    [SerializeField]
    LineRenderer[] lineRenderers = null;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        Init();
    }

    void Init()
    {
        _LevelPack = Util.FileOperations.Load<LevelPack>(Constants.LEVELDATAFILE);
    }

    public void LoadLevel(int currLevelSize, int currLevel)
    {
        _CurrLevelSize = currLevelSize;
        _CurrLevel = currLevel;
        SceneManager.LoadScene(Constants.GAMEPLAYSCENE);
    }

    public void GoToMenu()
    {
        ReleaseMemory();
        SceneManager.LoadScene(Constants.MAINMENUSCENE);
    }

    public InputHandler GetInputHandler()
    {
        if (_InputHandler == null) _InputHandler = GameObject.FindObjectOfType(typeof(InputHandler)) as InputHandler;
        return _InputHandler;
    }

    public void ValidateConditions(LevelGrid levelGrid, bool isMouseDown)
    {
        if (_GameLogic == null)
        {
            _GameLogic = new GameLogic();
            _GameLogic.Initialize();
            ResetLineRenderers();
        }
        _GameLogic.ApplyGameLogic(levelGrid, isMouseDown);
        if (!isMouseDown)
            _GameLogic.SetPreviousNodeEmpty();
    }

    public void ResetLineRenderers()
    {
        if(lineRenderers != null && lineRenderers.Length > 0)
        {
            lineRenderers = null;
        }
        lineRenderers = new LineRenderer[pCurrentLevel.pConnectors.Length];
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            GameObject lr = new GameObject();
            lr.transform.parent = this.transform;
            lr.transform.position = Vector3.zero;
            lineRenderers[i] = lr.AddComponent<LineRenderer>();
            lineRenderers[i].material = new Material(Shader.Find("Sprites/Default"));
            lineRenderers[i].startColor = pCurrentLevel.pConnectors[i].pColor;
            lineRenderers[i].endColor = pCurrentLevel.pConnectors[i].pColor;
            lineRenderers[i].positionCount = 0;
            lineRenderers[i].gameObject.SetActive(false);
            lineRenderers[i].sortingOrder = 1;
            lineRenderers[i].startWidth = Constants.LINEWIDTH;
            lineRenderers[i].startWidth = Constants.LINEWIDTH;
        }
    }

    public void CheckForGameFinish()
    {
        bool isGameFinished = true;
        for (int i = 0; i < _LevelGrid.Count; i++)
        {
            if(!_LevelGrid[i].pIsGridOccupied)
            {
                isGameFinished = false;
                break;
            }
        }

        if(isGameFinished)
        {
            Debug.Log("Game finished");
            StartCoroutine(DelayGameFinishCall());
        }
    }

    IEnumerator DelayGameFinishCall()
    {
        yield return new WaitForSeconds(0.5f);
        GoToMenu();
    }

    void ReleaseMemory()
    {
        if (lineRenderers != null && lineRenderers.Length > 0)
        {
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                Destroy(lineRenderers[i].gameObject);
            }
            lineRenderers = null;
        }

        if(_LevelGrid != null && _LevelGrid.Count > 0)
        {
            for (int i = 0; i < _LevelGrid.Count; i++)
            {
                Destroy(_LevelGrid[i].gameObject);
            }
            _LevelGrid = null;
        }
        Destroy(this.gameObject);
    }

    public void DrawLine(int colorIndex, Vector3[] positions)
    {
        if(!lineRenderers[colorIndex].gameObject.activeInHierarchy)
        {
            lineRenderers[colorIndex].gameObject.SetActive(true);
        }
        if (positions != null)
        {
            lineRenderers[colorIndex].positionCount = positions.Length;
            lineRenderers[colorIndex].SetPositions(positions);
        }
        else
        {
            lineRenderers[colorIndex].positionCount = 0;
        }
    }
}
