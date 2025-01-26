using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using static Enums;

public class SceneLoader
{
    private readonly AllScenesData _scenesData;

    private SceneLoader (AllScenesData data)
    {
        _scenesData = data;
    }

    public void LoadSceneByType (SceneType sceneType)
    {
        string sceneName = _scenesData.GetSceneNameByType(sceneType);

        if (!string.IsNullOrEmpty(sceneName))
        {
            LoadSceneByName(sceneName);
        }
        else
        {
            Debug.LogError("Scene with type " + sceneType + " could not be found!");
        }
    }

    public void LoadSceneByName(string sceneName)
    {
        if (CanLoadScene(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene with name " + sceneName + " not found!");
        }
    }
    private bool CanLoadScene(string sceneName)
    {
        return Application.CanStreamedLevelBeLoaded(sceneName);
    }

}
