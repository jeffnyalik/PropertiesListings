using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PropertiesListings.Authentication;
using PropertiesListings.Data;
using PropertiesListings.Data.Repo;
using PropertiesListings.DataContext;
using PropertiesListings.Helpers;
using PropertiesListings.Interfaces;
using PropertiesListings.MailSettings;
using PropertiesListings.Services;
using SignalRWeb.HubConfig;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connections = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connections, ServerVersion.AutoDetect(connections));
});

//Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
//Adding jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        ValidateIssuerSigningKey = true
    };
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
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);




/*Add SignalR*/
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

//Email config service
//builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailConfig"));
/*var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
                    .Get<EmailConfiguration>();*/

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddTransient<IMailService, MailService>();

//builder.Services.AddSingleton(emailConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

//configure cors policies
app.UseCors("AllowAllHeaders");

//Configure endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<SignalRHub>("/toastr");
});

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
