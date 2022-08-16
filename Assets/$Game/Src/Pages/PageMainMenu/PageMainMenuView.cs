using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// View component of main menu UI
/// </summary>
public class PageMainMenuView : UIPageView
{
	[SerializeField]
	Transform _BackgroundImgPrefab = null;

	[SerializeField]
	Transform _LevelButton = null;

	[SerializeField]
	Transform dataparent = null;

    [SerializeField]
    ScrollRect scrollRectRef = null;

    public event System.Action<int, int> OnLevelSelected;

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

    public void DrawLevelData(LevelPack[] _LevelPack)
    {
        if (scrollRectRef != null) scrollRectRef.gameObject.SetActive(true);
        for (int i = 0; i < _LevelPack.Length; i++)
        {
            LevelPack levelPack = _LevelPack[i];
            GameObject levelsBg = Instantiate(_BackgroundImgPrefab.gameObject, dataparent);
            for (int j = 0; j < levelPack.pLevels.Length; j++)
            {
                Level level = levelPack.pLevels[j];
                GameObject levelObj = Instantiate(_LevelButton.gameObject, levelsBg.transform);
                DataGrid grid = levelObj.GetComponent<DataGrid>();
                int currLevelSize = i;
                int currLevel = j;
                grid.SetMenuGridData(currLevelSize, currLevel, OnLevelBtnClicked);
            }
        }
    }

    public void OnLevelBtnClicked(int currLevelSize, int currLevel)
    {
        if(OnLevelSelected != null)
        {
            OnLevelSelected.Invoke(currLevelSize, currLevel);
        }
    }
}
