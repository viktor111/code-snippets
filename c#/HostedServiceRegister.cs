public static IServiceCollection AddHostedServices(this IServiceCollection serviceCollection)
{
        serviceCollection.AddHostedService<DepositHostedService>();

        return serviceCollection;
}