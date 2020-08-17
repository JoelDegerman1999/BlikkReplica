using System.Threading.Tasks;
using BlikkBaiscReplica.Models.Auth;

namespace BlikkBaiscReplica.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> Register(RegisterModel model);
        Task<UserManagerResponse> Login(LoginModel model);
    }
}
