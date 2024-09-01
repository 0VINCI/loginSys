using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using profsysinf.Core.Events;
using profsysinf.Core.Repositories;
using projsysinf.Application.Commands;
using projsysinf.Application.Dto;
using projsysinf.Application.Events;
using projsysinf.Application.Services;
using projsysinf.Infrastructure;
using projsysinf.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<ICommandHandler<SignInCommand, SignInResponseDto>, SignInCommandHandler>();

builder.Services.AddScoped<ICommandHandler<RegisterCommand, string>, RegisterCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ChangePasswordCommand, string>, ChangePasswordCommandHandler>();
builder.Services.AddScoped<ICommandHandler<PasswordReminderCommand, string>, PasswordReminderCommandHandler>();
builder.Services.AddScoped<ICommandHandler<LogoutCommand>, LogoutCommandHandler>();

builder.Services.AddSingleton<IEmailService>(sp => 
    new MailerSendEmailService("mlsn.xx"));
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
builder.Services.AddScoped<IEventHandler<UserSignedInEvent>, UserSignedInEventHandler>();
builder.Services.AddScoped<IEventHandler<UserSignedInEvent>, UserSignedInEventHandler>();
builder.Services.AddScoped<IEventHandler<UserFailedLoginEvent>, UserFailedLoginEventHandler>();
builder.Services.AddScoped<IEventHandler<UserLockedOutEvent>, UserLockedOutEventHandler>();
builder.Services.AddScoped<IEventHandler<UserRegisterEvent>, UserRegisterEventHandler>();
builder.Services.AddScoped<IEventHandler<ChangePasswordEvent>, ChangePasswordEventHandler>();
builder.Services.AddScoped<IEventHandler<PasswordReminderEvent>, PasswordReminderEventHandler>();
builder.Services.AddScoped<IEventHandler<UserLoggedOutEvent>, UserLoggedOutEventHandler>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); 
});

app.Run();