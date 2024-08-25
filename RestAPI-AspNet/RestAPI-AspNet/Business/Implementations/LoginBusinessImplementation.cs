using RestAPI_AspNet.Configuration;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Repository;
using RestAPI_AspNet.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestAPI_AspNet.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private TokenConfiguration _configuration;

        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository userRepository, ITokenService tokenService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userVO)
        {
            //valida o UserVO é retorna uma entidade do tipo User
            var user = _userRepository.ValidateCredentials(userVO);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            //gera um token
            var accessToken = _tokenService.GenerateAccesToken(claims);

            //gera un token atualizado
            var refreshToken = _tokenService.GenerateRefreshToken();

            //seta o valor na entidade user
            user.RefreshToken = refreshToken;

            //seta o valor na entidade user
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            //atualiza os dados do User e salva
            _userRepository.RefreshUserInfo(user);

            //data da criação do token
            DateTime createDate = DateTime.Now;

            //data da expiração do token
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            //retorna um token com os valores 
            return new TokenVO
                (
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                );       
          
        }


        //função que gera um novo token a partir do refresh token
        //gerar novos tokens se as credenciais forem válidas 
        public TokenVO ValidateCredentials(TokenVO token)
        {
           var accessToken = token.AccessToken;
           var refreshToken = token.RefreshToken;

           var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

           var userName = principal.Identity.Name;

           var user = _userRepository.ValidateCredentials(userName);

            
            if(user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;


            accessToken = _tokenService.GenerateAccesToken(principal.Claims);
           
            refreshToken = _tokenService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
               true,
               createDate.ToString(DATE_FORMAT),
               expirationDate.ToString(DATE_FORMAT),
               accessToken,
               refreshToken
               );
        }

        //função para que revoga o token
        public bool RevokeToken(string userName)
        {
            return _userRepository.RevokeToken(userName);
        }


    }
}
