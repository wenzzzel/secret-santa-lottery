using Zenject;

public class DependencyInjection : MonoInstaller
{
    public override void InstallBindings() // Can this be private?
    {
        Container.Bind<IApiHelper>().To<ApiHelper>().AsSingle();
    }
}