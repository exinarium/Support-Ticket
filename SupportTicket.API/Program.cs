using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SupportTicket.API.Domain.Config;
using SupportTicket.API.Domain.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SupportTicket.API.Domain.Services;
using GraphQL;
using GraphQL.Types;
using SupportTicket.API.Domain.GraphQL.Mutation;
using SupportTicket.API.Domain.GraphQL.Query;
using SupportTicket.API.Domain.GraphQL.Schema;
using SupportTicket.API.Domain.GraphQL.Type;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql());

// Configuration
builder.Services.Configure<DatabaseConfig>(
    builder.Configuration.GetSection(nameof(DatabaseConfig)));

var databaseConfig = builder.Configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();

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

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddTransient<AccountType>();
builder.Services.AddTransient<UserType>();
builder.Services.AddTransient<UserInputType>();
builder.Services.AddTransient<UserQuery>();
builder.Services.AddTransient<UserMutation>();
builder.Services.AddTransient<ISchema, UserSchema>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(options => options
        .UseNpgsqlConnection(
            $"Server={databaseConfig!.Host};" +
            $"Port={databaseConfig.Port};" +
            $"Database={databaseConfig.DatabaseName};" +
            $"User Id={databaseConfig.Username};" +
            $"Password={databaseConfig.Password}")
    ));

builder.Services.AddHangfireServer();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddGraphQL(graph =>
{
    graph.AddAutoSchema<ISchema>();
    graph.AddSystemTextJson();
    graph.AddAuthorizationRule();
});

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