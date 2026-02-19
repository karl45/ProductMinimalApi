using LoginProductMinimalApi.Entities;
using LoginProductMinimalApi.Repositories.UserRepository;
using LoginProductMinimalApi.RequestModels;
using LoginProductMinimalApi.ResponseModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LoginProductMinimalApi.Handlers.RefreshToken
{
    public class RefreshTokenRequestHandler : BaseRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly IUserRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public RefreshTokenRequestHandler(IUserRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        protected override async Task<RefreshTokenResponse> HandleInternal(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var client = await _loginRepository.GetUserByRefreshToken(request.RefreshToken, cancellationToken);
            if (client == null) { 
                return new RefreshTokenResponse { Token = string.Empty};
            }

            string newToken = GenerateNewToken(client, _configuration["Jwkc:Kid"]!);

            client.RefreshToken = Guid.NewGuid().ToString();

            await _loginRepository.UpdateUser(client, cancellationToken);

            return new RefreshTokenResponse { Token = newToken, RefreshToken = client.RefreshToken!,  };

        }

        private string GenerateNewToken(User client, string kid)
        {
            string privateKeyPem = File.ReadAllText(_configuration["Private:Key"]!);

            RSA rsa = RSA.Create();

            rsa.ImportFromPem(privateKeyPem.ToCharArray());

            var signingCrednetials = new SigningCredentials(new RsaSecurityKey(rsa) { KeyId = kid },
                SecurityAlgorithms.RsaSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, client.UserName.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["OIDC:Issuer"],
                audience: _configuration["OIDC:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCrednetials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
