using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace RestAPI_AspNet.Services
{
    public interface ITokenService
    {

        //Gera um token de acesso
        string GenerateAccesToken(IEnumerable <Claim> claims);


        //Gera um token de atualização 
        string GenerateRefreshToken();


        //Extrai as informações do usuário(representadas por um ClaimsPrincipal) a partir de um token que já expirou
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);


    }
}
