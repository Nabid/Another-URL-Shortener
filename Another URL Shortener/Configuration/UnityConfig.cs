using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Models;
using Unity;

namespace Another_URL_SHortener.Configuration
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IRepository<Entity>, Repository<Entity>>();
        }
    }
}