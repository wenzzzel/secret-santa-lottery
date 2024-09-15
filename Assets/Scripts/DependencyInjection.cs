using Zenject;
using UnityEngine;
using System.Collections;

public class DependencyInjection : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<Greeter>().AsSingle().NonLazy();

        Container.Bind<IApiHelper>().To<ApiHelper>().AsSingle();
    }
}

public class Greeter
{
    public Greeter(string message)
    {
        Debug.Log(message);
    }
}