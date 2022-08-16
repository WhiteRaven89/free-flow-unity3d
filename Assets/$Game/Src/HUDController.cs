using UnityEngine;

/// <summary>
/// In game hud
/// </summary>
[DisallowMultipleComponent]
public class HUDController : MonoBehaviour
{
	[Header("Page views")]
	[SerializeField] PageGameplayView 	pageGameplayView;

	// Page controllers
	PageGameplayController 	pageGameplayController;

	void Start()
    {
		Init();
    }

	public void Init()
	{
		this.CreatePageControllers();
	}

	void CreatePageControllers()
	{
		this.pageGameplayController = new PageGameplayController(
			new UIPageModel(),
			this.pageGameplayView
		);
		pageGameplayController.Open(null, true);
	}
}
