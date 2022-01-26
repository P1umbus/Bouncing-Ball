using UnityEngine;

public class LevelsReset : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.SetInt(Constants.PPname.NumbActiveLevel, 1);
        DataLoadSystem.GetLoader<Bank>(DataLoaders.Bank).PluralIncreaseCoinNumb(100);
        SpecialSceneLoader.instace.LoadScene(MainLevelList.Menu.ToString());
    }
}
