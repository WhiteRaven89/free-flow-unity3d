using UnityEngine;

/// <summary>
/// Controller for ingame UI
/// </summary>
public class PageGameplayController : UIPageController<PageGameplayView, UIPageModel> 
{
	public PageGameplayController(UIPageModel model, PageGameplayView view)
		:base(model, view)
	{
		this.view.DrawLevelData(GameManager.pInstance.pCurrentLevel, GameManager.pInstance.pGridSize, GameManager.pInstance.GetInputHandler());
		this.view.OnMenuSelected += () =>
		{
			GameManager.pInstance.GoToMenu();
		};
	}
}
