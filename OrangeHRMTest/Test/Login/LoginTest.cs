using NUnit.Framework;
using OrangeHRM.PageSteps;
using OrangeHRMTest.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverHelper.WebDriverFactory;
using CommonHelper.Helper.Config;
using CommonHelper.Utils;

namespace OrangeHRMTest.Test.Login
{
    [TestFixture]
    public class LoginTest : BaseWebTest
    {
        private LoginStep ObjLoginStep;

        [SetUp]
        public void SetUp()
        {
            this.ObjLoginStep = new LoginStep();
            //var ObjLocalChromeDriver = new WebDriverFactory().CreateWebDriverFactory(ToolConfigReader.GetToolConfig().Browser, ToolConfigReader.GetToolConfig().ExecutionType).InitializeWebDriver();
        }

        [TestCase(TestName = "ValidateLogin")]
        public void ValidateLogin()
        {
            this.ObjLoginStep.LoginOrangeHRM("Admin", "admin123");
        }

        
        [TestCase(LoginStep.ErrorMessageType.InvalidCredentials)]
        [TestCase(LoginStep.ErrorMessageType.PasswordEmpty)]
        [TestCase(LoginStep.ErrorMessageType.UserNameEmpty)]
        [TestCase(TestName = "ValidateErrorMessage")]
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
