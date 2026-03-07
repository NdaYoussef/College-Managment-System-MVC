using CloudinaryDotNet;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.Application.Mappings;
using UniManagementSystem.Application.Services;
using UniManagementSystem.Application.UploadSettings;
using UniManagementSystem.Domain.Models;
using UniManagementSystem.Infrastructure.DBContext;


namespace UniManagementSystem.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

                // Add services to the container.
                #region SeriLog settinges
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
            Log.Information("App Starts!");
            builder.Host.UseSerilog();
            //try
            //{
            //    Log.Information("Application starts");

            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal("Failed to run program");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
            #endregion


            #region Connection String
            builder.Services.AddDbContext<UniSystemContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion
            #region Add Authentication
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(op =>
            //{
            //    op.SaveToken = true;
            //    op.RequireHttpsMetadata = false;
            //    op.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["JWT:Issuer"],
            //        ValidAudience = builder.Configuration["JWT:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
            //        ClockSkew = TimeSpan.Zero,
            //        RoleClaimType = ClaimTypes.Role,
            //        NameClaimType = ClaimTypes.Name,
            //    };
            //});

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            });
            #endregion
            #region Add Authorization

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Lecturer", policy => policy.RequireRole("Lecturer"));
                options.AddPolicy("Student", policy => policy.RequireRole("Student"));
            });
            #endregion
            #region Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UniSystemContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Dependency Injection
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAccountServicecs, AccountServicecs>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion


            #region Adding Mapster

            builder.Services.AddMapster();
            TypeAdapterConfig.GlobalSettings.Scan(typeof(MapsterConfig).Assembly);
            builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            builder.Services.AddScoped<IMapper, ServiceMapper>();

            #endregion

            #region Cloudinary Service 
            builder.Services.Configure<CloudinarySettings>(
               builder.Configuration.GetSection("CloudinarySettings"));
            var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            if (cloudinarySettings == null ||
                string.IsNullOrEmpty(cloudinarySettings.CloudName) ||
                string.IsNullOrEmpty(cloudinarySettings.ApiKey) ||
                string.IsNullOrEmpty(cloudinarySettings.ApiSecret))
            {
                throw new InvalidOperationException("Cloudinary settings are not properly configured.");
            }
            var account = new Account(
            cloudinarySettings.CloudName,
            cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecret
            );
            var cloudinary = new Cloudinary(account);

            builder.Services.AddSingleton(cloudinary);
            #endregion
            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.UseDeveloperExceptionPage();
            #region Call Seeding
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                await SeedData.SeedRoles(service); 
            }
            //var hasher = new PasswordHasher<ApplicationUser>();

            //var user = new ApplicationUser();

            //string hash = hasher.HashPassword(user, "Admin@123");

            //Console.WriteLine(hash);
            #endregion
            app.Run();
        }
    }
}
