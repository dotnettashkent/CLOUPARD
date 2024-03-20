using Microsoft.EntityFrameworkCore;
using Server.Infrastructure.ServiceCollection;
using Service.Data;
using Stl.Fusion;

#region Builder
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var cfg = builder.Configuration;
var env = builder.Environment;
#endregion

#region
services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseNpgsql(cfg.GetConnectionString("Default"));
});

services.AddDbContext<AppDbContext>(options =>
{
    // Configure options for AppDbContext
    options.UseNpgsql(cfg.GetConnectionString("Default"));
});

#endregion

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

#region STL.Fusion
IComputedState.DefaultOptions.MustFlowExecutionContext = true;
builder.Services.AddFusionServices();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}
/*else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}*/

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
