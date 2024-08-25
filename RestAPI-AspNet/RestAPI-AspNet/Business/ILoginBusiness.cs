using RestAPI_AspNet.Data.VO;

namespace RestAPI_AspNet.Business
{
    public interface ILoginBusiness
    {
        //valida
        TokenVO ValidateCredentials(UserVO user);

        //valida
        TokenVO ValidateCredentials(TokenVO token);

        //renovar um token
        bool RevokeToken(string userName);

    }
}
