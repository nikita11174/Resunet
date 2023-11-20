using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
    public class RegisterTests : Helpers.BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                //validate: should not be in the DB
                var emailValidationResult = await authBL.ValidateEmail(email);
                Assert.IsNull(emailValidationResult);

                //create user
                int userId = await authBL.CreateUser(
                    new Resunet.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });
                Assert.Greater(userId, 0);

                var userdalresult = await authDal.GetUser(userId);
                Assert.That(email, Is.EqualTo(userdalresult.Email));
                Assert.NotNull(userdalresult.Salt != null);

                var userbyemaildal = await authDal.GetUser(email);
                Assert.That(email, Is.EqualTo(userbyemaildal.Email));
                Assert.NotNull(userdalresult.Salt != null);

                //validate: should be in the DB
                emailValidationResult = await authBL.ValidateEmail(email);
                Assert.IsNotNull(emailValidationResult);

                string encpassword = encrypt.HashPassword("qwer1234", userbyemaildal.Salt);
                Assert.That(encpassword, Is.EqualTo(userdalresult.Password));

            }
        }
    }
}