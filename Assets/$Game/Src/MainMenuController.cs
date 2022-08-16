using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu hud
/// </summary>
public class MainMenuController : MonoBehaviour 
{
	[Header("Page views")]
	[SerializeField] PageMainMenuView pageMainMenuView;

	PageMainMenuController pageMainMenuController;

	void Start()
	{
		this.pageMainMenuController = new PageMainMenuController(
			new UIPageModel(),
			this.pageMainMenuView
		);
		pageMainMenuController.Open(null, true);
		this.pageMainMenuController.OnLevelSelected += this.OnPlayPressed;
	}

	void OnDestroy()
	{
		this.pageMainMenuController.OnLevelSelected -= this.OnPlayPressed;
	}

	void OnPlayPressed(int currLevelSize, int currLevel)
	{
		GameManager.pInstance.LoadLevel(currLevelSize, currLevel);
	}
}
