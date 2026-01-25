namespace Eventmaster.BLL.Services.Abstraction
{
    public interface IAccountService
    {
        Task<(bool success, string message)> Register(RegisterVM dto);
        Task<(bool success, string message, string token, DateTime expiration)> Login(LoginVM dto);
    }

}
