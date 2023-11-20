using Microsoft.AspNetCore.Http;
using Resunet.BL.Auth;
using Resunet.DAL;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuthDAL authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuthBL authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;

        public BaseTest()
        {
            dbSession = new DbSession(dbSessionDAL, httpContextAccessor);
            authBL = new AuthBL(authDal, encrypt, httpContextAccessor, dbSession);
        }
    }
}
