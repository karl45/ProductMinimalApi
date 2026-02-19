using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Org.BouncyCastle.Crypto;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace LoginProductMinimalApi.Extensions
{
    public static class RegisterJwtAuthorizationExtension
    {
        public static void RegisterJwtAuthorization(this WebApplicationBuilder webApplicationBuilder)
        {
            var oidcIssuer = webApplicationBuilder.Configuration["OIDC:Issuer"] ?? throw new InvalidOperationException("System parameter OIDC:Issuer is undefined");
            var oidcAudience = webApplicationBuilder.Configuration["OIDC:Audience"] ?? throw new InvalidOperationException("System parameter OIDC:Audience is undefined");

            string publicKeyPem = File.ReadAllText(webApplicationBuilder.Configuration["Public:Key"]!);

            RSA rsa = RSA.Create();

            rsa.ImportFromPem(publicKeyPem.ToCharArray());

            webApplicationBuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,                 // проверяем кто выдал токен
                        ValidateAudience = true,               // проверяем для кого токен
                        ValidateLifetime = true,               // проверяем срок действия
                        ValidateIssuerSigningKey = true,       // проверяем подпись
                        ValidIssuer = oidcIssuer, // issuer
                        ValidAudience = oidcAudience,          // audience / clientId
                        IssuerSigningKey = new RsaSecurityKey(rsa)   // публичный ключ RSA для проверки подписи
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Cookies["access_token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            webApplicationBuilder.Services.AddAuthorization();
        }
    }
}
