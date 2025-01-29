using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AllScenesData _allScenesData;
    [SerializeField] private UIController _uiController;

    public override void InstallBindings()
    {
        Container.Bind<AllScenesData>().FromInstance(_allScenesData).AsSingle().NonLazy();
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
        Container.Bind<SceneLoader>().AsSingle().NonLazy();
        Container.Bind<DataSaver>().AsSingle().NonLazy();   
    }
}
