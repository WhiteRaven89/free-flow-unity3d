using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Grid for Menu
/// </summary>
public class DataGrid : MonoBehaviour 
{
	[SerializeField]
	Image buttonImg = null;

	[SerializeField]
	protected Text txtLevelNo = null;

    public void SetMenuGridData(int currLevelSize, int currLevel, UnityAction<int, int> callback)
    {
        txtLevelNo.text = (currLevel + 1).ToString();
        Button lvlBtn = GetComponent<Button>();
        lvlBtn.onClick.AddListener(() => callback(currLevelSize, currLevel));
    }
}
