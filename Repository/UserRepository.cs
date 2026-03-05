using ReactAndJwt.Appdata;
using ReactAndJwt.Dto;
using ReactAndJwt.Model;

namespace ReactAndJwt.Repository
{
    public class UserRepository: IRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context; 
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User Get(UserRequestDto req)
        {
          var user =_context.Users.FirstOrDefault(u => u.UserName == req.UserName);
            if (user == null) {return null;}
            return user;
        }
        public void AddApplication(Application app)
        {
            _context.Application.Add(app);
            _context.SaveChanges();
        }
    }
}
