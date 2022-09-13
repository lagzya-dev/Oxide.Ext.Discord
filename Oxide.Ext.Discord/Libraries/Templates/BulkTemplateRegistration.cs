namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Used for bulk template registration
    /// </summary>
    /// <typeparam name="T">Type of the template being registered</typeparam>
    public class BulkTemplateRegistration<T> where T : BaseTemplate
    {
        /// <summary>
        /// Language for this template
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// Template to register
        /// </summary>
        public T Template { get; set; }
    }
}