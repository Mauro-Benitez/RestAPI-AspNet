using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;

namespace RestAPI_AspNet.Repository
{
    public interface IUserRepository
    {
        //Valida as credenciais do usuário 
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string userName);

        User RefreshUserInfo(User user);

        bool RevokeToken(string userName);

    }
}
