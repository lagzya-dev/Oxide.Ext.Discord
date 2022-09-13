namespace Oxide.Ext.Discord.Plugins.Core.Templates
{
    internal static class TemplateKeys
    {
        public static class Commands
        {
            private const string Base = nameof(Commands) + ".";
            
            public static class Delete
            {
                private const string Base = Commands.Base + nameof(Delete) + ".";

                public const string Success = Base + nameof(Success);

                public static class Errors
                {
                    private const string Base = Delete.Base + nameof(Errors) + ".";

                    public const string InvalidSelection = Base + nameof(InvalidSelection);
                    public const string CommandIdNotFound = Base + nameof(CommandIdNotFound);
                    public const string DeleteCommandError = Base + nameof(DeleteCommandError);
                }
            }
        }
    }
}