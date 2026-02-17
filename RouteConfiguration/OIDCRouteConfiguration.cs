using LoginProductMinimalApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace LoginProductMinimalApi.RouteConfiguration
{
    public class OIDCRouteConfiguration : IRouteConfiguration
    {
        public void ConfigureEndPoints(WebApplication webApplication)
        {
            webApplication.MapGet("/.well-known/openid-configuration", () => ConfigureOIDC(webApplication.Configuration));
            webApplication.MapGet("/.well-known/jwks.json", () => GetJwks(webApplication.Configuration));
        }

        public IResult ConfigureOIDC(IConfiguration configuration)
        {
            var oidcIssuer = configuration["OIDC:Issuer"];
            var oidcJwksPath = configuration["OIDC:JwksSubPath"];
            var tokenPath = configuration["OIDC:TokenPath"];
            var tokenSigningAlgValues = configuration["OIDC:TokenSignAlg"]?
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                        ?? new string[0];

            var model = new OIDCConfigurationModel
            {
                Issuer = oidcIssuer,
                JwksUri = $"{oidcIssuer}{oidcJwksPath}",
                TokenEndpoint = $"{oidcIssuer}{tokenPath}",
                IdTokenSigningAlgValuesSupported = tokenSigningAlgValues
            };

            return Results.Ok(new OIDCConfigurationModel()
            {
                Issuer = oidcIssuer,
                JwksUri = $"{oidcIssuer}{oidcJwksPath}",
                TokenEndpoint = $"{oidcIssuer}{tokenPath}",
                IdTokenSigningAlgValuesSupported = tokenSigningAlgValues
            });
        }

        public IResult GetJwks(IConfiguration configuration)
        {
            string publicKeyPem = File.ReadAllText(configuration["Public:Key"]!);

            RSA rsa = RSA.Create();

            rsa.ImportFromPem(publicKeyPem.ToCharArray());

            var kid = configuration["JWKC:Kid"];

            #region kid generating code
            //Код используется для генерации kid

            //var parameters = rsa.ExportParameters(false);

            //var modulus = parameters.Modulus!;
            //var exponent = parameters.Exponent!;

            //byte[] keyBytes = modulus.Concat(exponent).ToArray();

            //using var sha256 = SHA256.Create();
            //var hash = sha256.ComputeHash(keyBytes);
            //var kid = Base64UrlEncoder.Encode(hash);
            #endregion

            var key = new
            {
                kty = "RSA",
                use = "sig",
                kid,
                n = Base64UrlEncoder.Encode(rsa.ExportParameters(false).Modulus), // модуль RSA, часть публичного ключа для проверки подписи
                e = Base64UrlEncoder.Encode(rsa.ExportParameters(false).Exponent) // модуль RSA, публичная экспонента для проверки подписи
            };

            var jwks = new { keys = new[] { key } };

            return Results.Ok(jwks);

        }


    }

}
