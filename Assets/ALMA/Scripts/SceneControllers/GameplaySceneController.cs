using Zenject;

public class GameplaySceneController : IInitializable
{
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private UIController _UIController;
    [Inject] private DataSaver _dataSaver;

    public void Initialize()
    {
        _UIController.OpenScreen(Enums.ScreenType.GameplayScreen);
        ((GameplayScreen)_UIController.CurrentScreen).BackButton.onClick.AddListener(LoadMainMenu);
        ((GameplayScreen)_UIController.CurrentScreen).ClearFilesButton.onClick.AddListener(ClearFiles);
        _UIController.PopupController.LoadPins();
    }

    private void LoadMainMenu()
    {
        _sceneLoader.LoadSceneByType(Enums.SceneType.MainMenu);
    }

    private void ClearFiles()
    {
        _UIController.PopupController.DeleteAllPins();
        _dataSaver.ClearJsonFile("popupData");
    }
}
