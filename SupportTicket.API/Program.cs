var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseConfig>(
    builder.Configuration.GetSection(nameof(DatabaseConfig)));

builder.Services.AddPooledDbContextFactory<DataContext>((serviceProvider, options) =>
{
    var databaseConfig = serviceProvider.GetRequiredService<IOptions<DatabaseConfig>>().Value;

    options.UseNpgsql(
        $"Server={databaseConfig.Host};" +
        $"Port={databaseConfig.Port};" +
        $"User Id={databaseConfig.Username};" +
        $"Password={databaseConfig.Password};" +
        $"Database={databaseConfig.DatabaseName};");

    options.UseLazyLoadingProxies();
});

builder.Services.Configure<JwtConfig>(
    builder.Configuration.GetSection(nameof(JwtConfig)));

var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();

builder.Services.Configure<EmailConfig>(
    builder.Configuration.GetSection(nameof(EmailConfig)));

builder.Services.Configure<SendGridConfig>(
    builder.Configuration.GetSection(nameof(SendGridConfig)));

builder.Services.Configure<ServerConfig>(
    builder.Configuration.GetSection(nameof(ServerConfig)));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtConfig?.Issuer,
        ValidAudience = jwtConfig?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig?.Key ?? string.Empty)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();
builder.Services.AddSystemServices();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire((serviceProvider, configuration) =>
{
    var databaseConfig = serviceProvider.GetRequiredService<IOptions<DatabaseConfig>>().Value;

    configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(options =>
        {
            options.UseNpgsqlConnection(
                $"Server={databaseConfig!.Host};" +
                $"Port={databaseConfig.Port};" +
                $"Database={databaseConfig.DatabaseName};" +
                $"User Id={databaseConfig.Username};" +
                $"Password={databaseConfig.Password}");
        });
});

builder.Services.AddHangfireServer();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddGraphQLServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseGraphQLGraphiQL("/graphiql");
}

app.UseCors("AllowAll");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHangfireDashboard();

app.UseHttpsRedirection();
app.UseGraphQL<ISchema>();

app.Run();