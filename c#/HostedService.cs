public class DepositHostedService: IHostedService
{
    private readonly ILogger<DepositHostedService> _logger;
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<Transaction> _transactionRepository ;
    private readonly Electrum _electrumClient;
    private readonly ExternalServicesOptions _externalServicesOptions;
    private readonly IIdentityService _identityService;
    private readonly ICurrencyDataService _currencyDataService;
    private readonly IDepositService _depositHttpService;

    private Timer? _timer = null;
    private int _executionTimes = 0;

    public DepositHostedService(IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var services = scope.ServiceProvider;

        _addressRepository = services.GetRequiredService<IRepository<Address>>();
        _transactionRepository = services.GetRequiredService<IRepository<Transaction>>();

        _depositHttpService = services.GetRequiredService<IDepositService>();
        
        _identityService = services.GetRequiredService<IIdentityService>();
        _currencyDataService = services.GetRequiredService<ICurrencyDataService>();
        
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
        
        _logger = loggerFactory.CreateLogger<DepositHostedService>();
        _externalServicesOptions = services.GetRequiredService<IOptions<ExternalServicesOptions>>().Value;
        
        _electrumClient = new ElectrumFactory()
            .WithUsername("sendcrypto")
            .WithPassword("sendcrypto1234")
            .WithUrl(_externalServicesOptions.ElectrumUrl)
            .Build();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("--- Deposit Hosted Service running ---");
        
        _timer = new Timer(async o => await DepositWork(o), null, TimeSpan.Zero,
            TimeSpan.FromMinutes(4));
        
        return Task.CompletedTask;
    }

    private async Task DepositWork(object? state)
    {}
}