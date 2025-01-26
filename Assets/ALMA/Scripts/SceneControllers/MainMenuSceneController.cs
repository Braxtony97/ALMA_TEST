using UnityEngine;
using Zenject;

public class MainMenuSceneController : IInitializable
{
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private UIController _UIController;

    public void Initialize()
    {
        _UIController.OpenScreen(Enums.ScreenType.MainMenuScreen);
        ((MainMenuScreen)_UIController.CurrentScreen).GameplayButton.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        _sceneLoader.LoadSceneByType(Enums.SceneType.Gameplay);
    }
}
