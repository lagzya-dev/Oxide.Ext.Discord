namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    public abstract class PlaceholderCollection<T>
    {
        public abstract void RegisterPlaceholders(DiscordPlaceholders placeholders);

        protected virtual string GetDataKey()
        {
            return typeof(T).Name;
        }
    }
}