using System.Reflection;

namespace ProfileUpdate.Infra.Context.DbContext;

public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly ILogger<DatabaseContext> _logger;
    private readonly ICredential _credential;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger,
        ICredential credential) : base(options)
    {
        _logger = logger;
        _credential = credential;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _logger.LogInformation("Start configuring the database context connection");

        var connectionString = _credential.ExecuteAsync().Result;

        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        _logger.LogInformation("Finish configuring the database context connection");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _logger.LogInformation("Starting mappings entities");

        base.OnModelCreating(modelBuilder);

        Assembly.Load("ProfileUpdate.Infra.Context").GetTypes().Where(t =>
                t is { IsAbstract: false, IsGenericTypeDefinition: false } && t.GetTypeInfo().ImplementedInterfaces.Any(
                    i =>
                        i.GetTypeInfo().IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList().ForEach(x =>
            {
                dynamic configInstance = Activator.CreateInstance(x)!;
                modelBuilder.ApplyConfiguration(configInstance);
            });
    }
}