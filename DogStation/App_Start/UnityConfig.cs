using System.Web.Http;
using Unity.WebApi;

namespace DogStation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = DependencyRegisterType.Container();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}