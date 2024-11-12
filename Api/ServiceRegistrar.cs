using CommonLibrary.Protos;

namespace Api
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddUserGrpcClient(this IServiceCollection services, IConfiguration configuration)
        {
            var userServiceUrl = configuration.GetConnectionString("UserSerice");
            if (userServiceUrl == null || userServiceUrl == string.Empty) 
            {
                throw new ArgumentNullException(nameof(userServiceUrl), "Не задан адрес для сервиса пользователей.");
            }

            services.AddGrpcClient<User.UserClient>(o =>
            {
                o.Address = new Uri(userServiceUrl);
            });

            return services;
        }
    }
}
