namespace MinimalWithEf;
public static class AppExtinsions
{
    public static void RegisterAppServices(this WebApplicationBuilder builder)
    {
        var ct = builder.Configuration.GetConnectionString("sqlite");
        builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlite(ct));
        builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

    }
    public static void RegisterEndPoints(this WebApplication app)
    {
        var endpointdefinition = typeof(Program).Assembly
        .GetTypes()
        .Where(t => t.IsAssignableTo(typeof(IEndPointDefinition)) &&
        !t.IsAbstract && !t.IsInterface)
        .Select(Activator.CreateInstance)
        .Cast<IEndPointDefinition>();
        foreach (var endpoint in endpointdefinition)
        {
            endpoint.RegisterEndPoints(app);
        }
    }
}