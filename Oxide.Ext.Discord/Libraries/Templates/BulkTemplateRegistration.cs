namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Used for bulk template registration
    /// </summary>
    /// <typeparam name="T">Type of the template being registered</typeparam>
    public class BulkTemplateRegistration<T>
    {
        /// <summary>
        /// Language for this template
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// Template to register
        /// </summary>
        public T Template { get; set; }
        
        public TemplateVersion Version { get; set; }

        public BulkTemplateRegistration(T template, string language, TemplateVersion version)
        {
            Template = template;
            Language = language;
            Version = version;
        }
    }
}