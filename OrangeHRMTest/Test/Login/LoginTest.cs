using NUnit.Framework;
using OrangeHRM.PageSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHRMTest.Test.Login
{
    [TestFixture]
    public class LoginTest
    {
        private LoginStep ObjLoginStep;

        [SetUp]
        public void SetUp()
        {
            this.ObjLoginStep = new LoginStep();
        }

        [Test]
        public void ValidateLogin()
        {
            this.ObjLoginStep.LoginOrangeHRM("Admin", "admin123");
        }

        
        [Test]
        public void ValidateErrorMessage(LoginStep.ErrorMessageType errorMessageType)
        {
            this.ObjLoginStep.VerifyErrorMessage(errorMessageType);
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
