using Microsoft.IdentityModel.Tokens;
using RestAPI_AspNet.Configuration;
using System.ComponentModel.DataAnnotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestAPI_AspNet.Services.Implementations
{
    public class TokenService : ITokenService
    {
        //parametros do token
        private TokenConfiguration _configuration;

        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Metodo responsavel por criar e retornar um token JWT assinado
        public string GenerateAccesToken(IEnumerable<Claim> claims)
        {


            //Uma chave de segurança simétrica é criada usando um Secret  
            //A chave é codificada em UTF-8 para bytes antes de ser usada
            //Essa chave será utilizada para assinar digitalmente o token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));


            //Cria as credenciais de assinatura que combinam a chave secreta (secretKey) e o algoritmo de hashing HMAC-SHA256     
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



            //Um novo token JWT é criado 
            var options = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                signingCredentials: signinCredentials

                );


            //Esta linha utiliza o JwtSecurityTokenHandler para converter o objeto JwtSecurityToken em uma string codificada, que é o token JWT em si
            string tokeString = new JwtSecurityTokenHandler().WriteToken(options);


            //Retorna o token JWT que pode ser usado para autenticação em chamadas subsequentes à API
            return tokeString;

        }



        //Este método gera um token de atualização aleatório e seguro
        public string GenerateRefreshToken()
        {

            //um array de bytes é criado 
            var randomNumber = new byte[32];


            //RandomNumberGenerator é uma classe que fornece um gerador de números aleatórios criptograficamente seguro
            //O método GetBytes do objeto rng preenche o array randomNumber com valores aleatórios
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);


                //converte o array de bytes aleatórios em uma string codificada em Base64 e o retorna
                return Convert.ToBase64String(randomNumber);
            }

        }


        //Este método valida um token JWT expirado, garantindo sua autenticidade, e retorna as informações de identidade (claims) contidas nele
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            //Aqui, são definidos os parâmetros de validação do token (TokenValidationParameters). Esses parâmetros determinam como o token será validado
            var tokenValidationParameters = new TokenValidationParameters
            {
                //não valida o público-alvo (audience) do token.
                ValidateAudience = false,

                //não valida o emissor (issuer) do token.
                ValidateIssuer = false,

                //valida a chave de assinatura do emissor para garantir a autenticidade do token.
                ValidateIssuerSigningKey = true,

                //define a chave de segurança simétrica usada para validar a assinatura do token.
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),

                //desabilita a validação do tempo de vida do token, permitindo que tokens expirados sejam processados
                ValidateLifetime = false

            };


            //Cria uma instância de JwtSecurityTokenHandler, que é responsável por manipular, validar e gerar tokens JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            //Será usada para armazenar o token de segurança validado.
            SecurityToken securityToken;

            //Valida o token JWT usando os parâmetros de validação definidos anteriormente
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            //Aqui, o securityToken é convertido para o tipo JwtSecurityToken, que é uma representação mais específica do token JWT, Isso permite acessar propriedades específicas do JWT, como seu cabeçalho (Header) e payload (corpo)
            var jwtSecurityToken = securityToken as JwtSecurityToken;


            //Este bloco verifica se o token é nulo ou se o algoritmo de assinatura (Alg) no cabeçalho do token não é HmacSha256
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("Invalid Token");

            }

            //retorna o objeto que contém as informações de identidade (claims) do token, mesmo que ele tenha expiradp
            return principal;



        }
    }
}
