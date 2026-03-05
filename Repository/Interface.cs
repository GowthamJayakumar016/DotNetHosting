using ReactAndJwt.Dto;
using ReactAndJwt.Model;

namespace ReactAndJwt.Repository
{
    public interface IRepository
    {
        public User Get(UserRequestDto req);
        public void Add(User user);
        public void AddApplication(Application app);
    }
}
