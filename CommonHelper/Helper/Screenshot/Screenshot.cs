using CommonHelper.Helper.Log;
using CommonHelper.Interfaces.Screenshot;
using System;

namespace CommonHelper.Helper.Screenshot
{
    public class ScreenshotManager
    {
        private IScreenshotable screenshotable;

        public ScreenshotManager(IScreenshotable screenshotable)
        {
            this.screenshotable = screenshotable;
        }

        public void MakeScreenshotAndUpload(string className, string methodName)
        {
            try
            {
                Logger.Log("Generating of screenshot started.");
                var screenshotName = this.GenerateScreenshotName(className, methodName);
                //screenshotable.MakeAndSaveScreenshot(ReportUtils.ErrorImagePath, screenshotName);
                //ReportUtils.UploadScreenshot(screenshotName);
                //ReportUtils.UploadScreenshotToRP(Path.Combine(ReportUtils.ErrorImagePath, screenshotName));
                Logger.Log("Generating of screenshot finished.");
            }
            catch (Exception)
            {
                Logger.Error("Failed to capture screenshot. Skip logging this exception");
            }

        }

        private string GenerateScreenshotName(string className, string methodName)
        {
            return className +"_"+ methodName +"_"+ string.Format("{0:yy_MM_dd_hh_mm_ss}", DateTime.Now) + ".jpeg";
        }
    }
}
