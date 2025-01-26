using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private UIController _UIcontroller;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        LoadGame();
    }

    private void LoadGame()
    {
        _sceneLoader.LoadSceneByType(Enums.SceneType.MainMenu);
    }
}
