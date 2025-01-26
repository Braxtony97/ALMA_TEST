using UnityEngine;
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

        Debug.Log(sceneName); 
    }
}
