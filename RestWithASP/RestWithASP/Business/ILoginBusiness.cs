using RestWithASP.Data.VO;

namespace RestWithASP.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);

        TokenVO ValidateCredentials(TokenVO tokenVO);

        bool RevokeToken(string userName);
    }
}
