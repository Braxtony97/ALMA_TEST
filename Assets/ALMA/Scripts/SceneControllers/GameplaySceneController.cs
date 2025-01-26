using Zenject;

public class GameplaySceneController : IInitializable
{
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private UIController _UIController;

    public void Initialize()
    {
        _UIController.OpenScreen(Enums.ScreenType.GameplayScreen);
        ((GameplayScreen)_UIController.CurrentScreen).BackButton.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        _sceneLoader.LoadSceneByType(Enums.SceneType.MainMenu);
    }
}
