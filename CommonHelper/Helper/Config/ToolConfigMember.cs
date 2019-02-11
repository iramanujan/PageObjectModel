using CommonHelper.Helper.Attributes;
using System.Configuration;

namespace CommonHelper.Helper.Config
{
    public class ToolConfigMember : ConfigurationSection
    {

        public enum LocalizationType
        {
            [StringValue("en")]
            en,
            [StringValue("de")]
            de,
            [StringValue("es")]
            es,
            [StringValue("fr")]
            fr,
            [StringValue("it")]
            it
        }

        public enum BrowserType
        {
            [StringValue("Unknown")]
            Unknown = 0,

            [StringValue("Chrome")]
            Chrome = 1,

            [StringValue("InternetExplorer")]
            IE = 2,

            [StringValue("Edge")]
            Edge = 3,

            [StringValue("Firefox")]
            Firefox = 4,

            [StringValue("Safari")]
            Safari = 5
        }

        public enum WebDriverExecutionType
        {
            [StringValue("Local")]
            Local = 1,

            [StringValue("Grid")]
            Grid = 2
        }

        [ConfigurationProperty("Browser", DefaultValue = "Chrome", IsRequired = true)]
        public BrowserType Browser
        {
            get => (BrowserType)this["Browser"];
            set => value = (BrowserType)this["Browser"];
        }

        [ConfigurationProperty("ExecutionType", DefaultValue = "Local", IsRequired = true)]
        public WebDriverExecutionType ExecutionType
        {
            get => (WebDriverExecutionType)this["ExecutionType"];
            set => value = (WebDriverExecutionType)this["ExecutionType"];
        }

        [ConfigurationProperty("Localization", DefaultValue = "en", IsRequired = true)]
        public LocalizationType Localization
        {
            get => (LocalizationType)this["Localization"];
            set => value = (LocalizationType)this["Localization"];
        }

        [ConfigurationProperty("CommandTimeout", DefaultValue = "90000", IsRequired = true)]
        public double CommandTimeout
        {
            get => (double)this["CommandTimeout"];
            set => value = (double)this["CommandTimeout"];
        }


        [ConfigurationProperty("PageLoadWait", DefaultValue = "90000", IsRequired = true)]
        public double PageLoadWait
        {
            get => (double)this["PageLoadWait"];
            set => value = (double)this["PageLoadWait"];
        }


    }
}
