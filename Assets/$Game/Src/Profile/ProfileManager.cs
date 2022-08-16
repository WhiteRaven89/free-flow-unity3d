using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class responsible for holding user stats
/// </summary>
public class ProfileManager : MonoBehaviour 
{
    public void SaveWinLevel(int alevelSize, int aCurrentGame)
    {
        List<int> winLevels = Util.DataOperations.Load<int>(alevelSize.ToString()).ToList();
        bool isLvlSaved = winLevels.Exists(p => p == aCurrentGame);
        if(!isLvlSaved)
        {
            winLevels.Add(aCurrentGame);
            Util.DataOperations.Save(winLevels.ToArray(), alevelSize.ToString());
        }
    }

    public List<int> GetWinLevels(int alevelSize)
    {
        return Util.DataOperations.Load<int>(alevelSize.ToString()).ToList();
    }
}
