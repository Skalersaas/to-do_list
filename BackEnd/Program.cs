using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using To_Do_List.Data;

namespace To_Do_List
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = SetUpBuilder(args).Build();
            app.UseCors("Frontend");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Accounts}/{action=Index}/{id?}");

            //app.Map("/data", [Authorize] () => new { message = "Hello World!" });
            //app.Map("/", (ApplicationContext db) => db.Users);
            app.Run();
        }
        private static WebApplicationBuilder SetUpBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Frontend",
                    policy => policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            builder.Services.AddControllers();
            SetUpDatabase(builder);
            SetUpAuthentification(builder);
            SetUpAuthorization(builder);
            return builder;
        }
        private static void SetUpDatabase(WebApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>
                            (options => options.UseNpgsql(connectionString));
        }
        private static void SetUpAuthentification(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                    };
                });

        }
        private static void SetUpAuthorization(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization();
        }
    }

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new (Encoding.UTF8.GetBytes(KEY));
        public static string GetJWTToken(string userLogin, int userId)
        {
            var claims = new List<Claim>() { 
                new("Id", userId.ToString()),
                new("Login", userLogin),
            };
            var jwt = new JwtSecurityToken(
                issuer: ISSUER,
                audience: AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
                signingCredentials: new(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
