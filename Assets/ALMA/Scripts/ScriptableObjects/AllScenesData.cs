using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "AllScenesData", menuName = "Scriptable Objects/AllScenesData")]
public class AllScenesData : ScriptableObject
{
    [SerializeField] private SceneData[] _sceneData;

    public string GetSceneNameByType (SceneType sceneType)
    {
        foreach (SceneData sceneData in _sceneData)
        {
            if (sceneData.SceneType == sceneType)
            {
                return sceneData.SceneName;
            }
        }

        return null;
    }
}
