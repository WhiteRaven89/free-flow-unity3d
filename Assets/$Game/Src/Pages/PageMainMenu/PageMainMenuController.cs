using UnityEngine;

/// <summary>
/// Main menu UI controller 
/// </summary>
public class PageMainMenuController : UIPageController<PageMainMenuView, UIPageModel>
{
	public event System.Action<int,int> OnLevelSelected;

	public PageMainMenuController(UIPageModel model, PageMainMenuView view)
		: base(model, view)
	{
		this.view.DrawLevelData(GameManager.pInstance.pLevelPack);
		this.view.OnLevelSelected += (_CurrLevelSize, _CurrLevel) => {
			if (this.OnLevelSelected != null)
				this.OnLevelSelected(_CurrLevelSize, _CurrLevel);
		};
	}
}
