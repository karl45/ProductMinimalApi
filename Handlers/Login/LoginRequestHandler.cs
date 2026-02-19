using LoginProductMinimalApi.Entities;
using LoginProductMinimalApi.Models;
using LoginProductMinimalApi.Repositories.UserRepository;
using LoginProductMinimalApi.ResponseModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoginProductMinimalApi.Handlers.Login
{
    public class LoginRequestHandler : BaseRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginRequestHandler(IConfiguration configuration, IUserRepository loginRepository)
        {
            _configuration = configuration;
            _loginRepository = loginRepository;
        }

        protected override async Task<LoginResponse?> HandleInternal(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestUser = new User()
                {
                    UserName = request.UserName,
                    Password = request.Password
                };

                var user = await _loginRepository.GetUser(requestUser, cancellationToken);
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
                await _loginRepository.UpdateUser(user, cancellationToken);

                return new LoginResponse()
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

        private string GenerateToken(User client, string kid)
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
