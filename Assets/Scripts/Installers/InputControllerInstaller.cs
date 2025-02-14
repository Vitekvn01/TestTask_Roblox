using UnityEngine;
using Zenject;

public class InputControllerInstaller : MonoInstaller
{
    [SerializeField] private InputController _inputController;
    public override void InstallBindings()
    {
        Container.Bind<IInputController>()
            .FromInstance(_inputController)
            .AsSingle();
    }
}