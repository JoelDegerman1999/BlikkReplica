using System.Threading.Tasks;
using BlikkBasicReplica.Models.Auth;

namespace BlikkBasicReplica.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> Register(RegisterModel model);
        Task<UserManagerResponse> Login(LoginModel model);
    }
}
