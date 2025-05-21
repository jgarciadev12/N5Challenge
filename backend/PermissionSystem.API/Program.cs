using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Permissions.Commands;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Persistence;
using PermissionSystem.Infrastructure;
using PermissionSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PermissionDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// Infraestructura
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePermissionCommand>());
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PermissionDbContext>();
    db.Database.Migrate();
}

app.Run();