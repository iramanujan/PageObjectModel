using CommonHelper.Helper.Attributes;
using CommonHelper.Helper.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrangeHRM.PageEntity.Login;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace OrangeHRM.PageSteps
{
    public class LoginStep
    {
        public enum ErrorMessageType
        {
            [Description("Username cannot be empty")]
            UserNameEmpty = 0,

            [Description("Password cannot be empty")]
            PasswordEmpty = 1,

            [Description("Invalid credentials")]
            InvalidCredentials = 2
        }

        private LoginPage ObjLoginPage = new LoginPage();
        public void LoginOrangeHRM(string userName, string password)
        {
            ObjLoginPage.Username.ClearAndSendKeys(userName);
            ObjLoginPage.Username.ClearAndSendKeys(password);
            ObjLoginPage.Login.Submit();
        }

        public void VerifyErrorMessage(ErrorMessageType errorMessageType)
        {
            if (errorMessageType.Equals(ErrorMessageType.UserNameEmpty))
            {
                Logger.LogValidate("Verify User Empty Error Message");
                Assert.AreEqual(ErrorMessageType.UserNameEmpty.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
            if (errorMessageType.Equals(ErrorMessageType.PasswordEmpty))
            {
                Logger.LogValidate("Verify Password Empty Error Message");
                Assert.AreEqual(ErrorMessageType.PasswordEmpty.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
            if (errorMessageType.Equals(ErrorMessageType.InvalidCredentials))
            {
                Logger.LogValidate("Verify Invalid Credentials Error Message");
                Assert.AreEqual(ErrorMessageType.InvalidCredentials.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
        }
    }
}
