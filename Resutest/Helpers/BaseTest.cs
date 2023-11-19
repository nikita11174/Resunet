using Microsoft.AspNetCore.Http;
using Resunet.BL.Auth;
using Resunet.DAL;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected readonly IAuthDAL authDal = new AuthDAL();
        protected readonly IEncrypt encrypt = new Encrypt();
        protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuthBL authBL;

        public BaseTest()
        {
            authBL = new AuthBL(authDal, encrypt, httpContextAccessor);

            
        }
    }
}
