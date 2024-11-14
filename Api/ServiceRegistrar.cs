using CommonLibrary.Protos;
using NLog;

namespace Api
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddUserGrpcClient(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = LogManager.GetLogger("ServiceRegistrar");

            var userServiceUrl = configuration.GetConnectionString("UserSerice");
            if (userServiceUrl == null || userServiceUrl == string.Empty) 
            {
                logger.Error(new ArgumentNullException(), "Не задан адрес для сервиса пользователей.");
                throw new ArgumentNullException(nameof(userServiceUrl), "Не задан адрес для сервиса пользователей.");
            }

            services.AddGrpcClient<User.UserClient>(o =>
            {
                o.Address = new Uri(userServiceUrl);
                logger.Info($"{o.Address} установлен в качестве адреса сервиса пользователей.");
            });

            return services;
        }
    }
}
