using Oxide.Ext.Discord.Libraries;

namespace Oxide.Ext.Discord.Plugins
{
    internal static class TemplateKeys
    {
        public static class Commands
        {
            private const string Base = nameof(Commands) + ".";
            
            public static class Delete
            {
                // ReSharper disable once MemberHidesStaticFromOuterClass
                private const string Base = Commands.Base + nameof(Delete) + ".";

                public static readonly TemplateKey Success = new TemplateKey( Base + nameof(Success));

                public static class Errors
                {
                    // ReSharper disable once MemberHidesStaticFromOuterClass
                    private const string Base = Delete.Base + nameof(Errors) + ".";

                    public static readonly TemplateKey InvalidSelection = new TemplateKey(Base + nameof(InvalidSelection));
                    public static readonly TemplateKey CommandIdNotFound = new TemplateKey(Base + nameof(CommandIdNotFound));
                    public static readonly TemplateKey DeleteCommandError = new TemplateKey(Base + nameof(DeleteCommandError));
                }
            }
        }
    }
}