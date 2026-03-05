using ReactAndJwt.Model;
using ReactAndJwt.Repository;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ReactAndJwt.Service;
using System.Text;
using ReactAndJwt.Dto;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
namespace ReactAndJwt.Service
{
    public class UserService:IService
    {
        private readonly IRepository _repo;
        public UserService(IRepository repo)
        {
            _repo = repo;
        }
        

        public CheckCredit Check()
        {
            int cs = CreditScore();
            int cl=CheckCreditLimit(cs);
            return new CheckCredit
            {
                
               CreditScore = cs,
               CreditLimit = cl,
            };
        }
       

        public void Application(ApplicationTakingDto req, int UserId)
        {
            var Income = req.Income;
            var creditScore= CreditScore();
            var creditlimit = CheckCreditLimit(Income);
            var app = new Application
            {
                Name = req.Name,
                Income = req.Income,
                DOB = req.DOB,
                PAN = req.PAN,
                CreditScore = creditScore,
                Creditlimit=creditlimit,
                UserId = UserId

            };


            _repo.AddApplication(app);
            
        }

        public int CheckCreditLimit(decimal Income)
        {
           if(Income<=200000)
            {
                return 50000;
            }
           else if(Income>200000 && Income<=300000)
            {
                return 75000;
            }
           else if(Income>300000 && Income<=500000)
            {
                return 100000;
            }
            else
            {
                return 0;
            }
        }

        public int CreditScore()
        {
            Random r = new Random();
            return r.Next(600, 900);
        }

      
    }
}
