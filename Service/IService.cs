using Microsoft.AspNetCore.Mvc;
using ReactAndJwt.Dto;

namespace ReactAndJwt.Service
{
    public interface IService
    {
        public void Application(ApplicationTakingDto req, int userid);
        public CheckCredit Check();
    }
}
