using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AllScenesData _allScenesData;

    public override void InstallBindings()
    {
        Container.Bind<AllScenesData>().FromInstance(_allScenesData).AsSingle().NonLazy();
        Container.Bind<SceneLoader>().AsSingle().NonLazy();
    }
}
