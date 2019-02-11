using System.Configuration;

namespace CommonHelper.Helper.Config
{
    public class ToolConfigReader
    {
        private static readonly string ToolConfigSection = "ToolConfig";
        private static readonly ToolConfigMember toolConfigMember;

        static ToolConfigReader() => toolConfigMember = ConfigurationManager.GetSection(ToolConfigSection) as ToolConfigMember;
        public static ToolConfigMember ToolConfigMembers => toolConfigMember;
    }
}
