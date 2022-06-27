using Microsoft.EntityFrameworkCore;
using PropertiesListings.Data;
using PropertiesListings.Data.Repo;
using PropertiesListings.DataContext;
using PropertiesListings.Interfaces;
using SignalRWeb.HubConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connections = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connections, ServerVersion.AutoDetect(connections));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
         builder =>
         {
             builder.AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod();
         }
        );
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

/*Add SignalR*/
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

//configure cors policies
app.UseCors("AllowAllHeaders");

//Configure endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<SignalRHub>("/toastr");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
