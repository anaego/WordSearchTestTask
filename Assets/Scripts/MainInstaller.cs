using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private FieldView fieldView;
    [SerializeField] private InputView inputView;
    [SerializeField] private DataConfigScriptableObject dataConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(fieldView);
        Container.BindInstance(inputView);
        Container.BindInstance(dataConfig);
        Container.Bind<FieldController>().AsSingle().NonLazy();
        Container.Bind<InputController>().AsSingle().NonLazy();
    }
}