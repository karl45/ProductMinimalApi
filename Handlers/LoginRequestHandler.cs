using LoginProductMinimalApi.Entities;
using LoginProductMinimalApi.Models;
using LoginProductMinimalApi.Repositories.LoginRepository;
using LoginProductMinimalApi.ResponseModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoginProductMinimalApi.Handlers
{
    public class LoginRequestHandler : BaseRequestHandler<LoginRequestModel, LoginResponseModel>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginRequestHandler(IConfiguration configuration, ILoginRepository loginRepository)
        {
            _configuration = configuration;
            _loginRepository = loginRepository;
        }

        protected override async Task<LoginResponseModel?> HandleInternal(LoginRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _loginRepository.Authorize(request.UserName, request.Password, cancellationToken);
                if (user == null)
                {

                    return null;
                }

                bool valid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

                if (!valid)
                {
                    return null;
                }

                var kid = _configuration["JWKC:Kid"]!;

                var token = GenerateToken(user, kid);

                user.RefreshToken = Guid.NewGuid().ToString();
                await _loginRepository.UpdateClientRefreshToken(user, cancellationToken);

                return new LoginResponseModel()
                {
                    CsrfToken = "",
                    Token = token,
                    RefreshToken = user.RefreshToken
                };
            }
            catch (Exception)
            {
                throw;
            }

            
        }

        private string GenerateToken(Client client, string kid)
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
