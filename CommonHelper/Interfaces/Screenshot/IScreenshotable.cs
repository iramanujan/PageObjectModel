namespace CommonHelper.Interfaces.Screenshot
{

    public interface IScreenshotable
    {
        void MakeAndSaveScreenshot(string filePath, string fileName);
    }
}