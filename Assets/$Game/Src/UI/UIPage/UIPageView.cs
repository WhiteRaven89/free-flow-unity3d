using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup)), DisallowMultipleComponent]
public class UIPageView : MonoBehaviour
{
	[Tooltip("Content object is containing everything on page except child Popup pages!")]
	[SerializeField] protected CanvasGroup content;

	CanvasGroup canvasGroup;

	public virtual void Init()
	{
		this.gameObject.SetActive(false);
	}

	public virtual void Enable()
	{
		this.content.interactable = true;
	}

	public virtual void Disable()
	{
		this.content.interactable = false;
	}
}
