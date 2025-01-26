using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
public class SceneData : ScriptableObject
{
    public SceneType SceneType;
    public string SceneName;
}
