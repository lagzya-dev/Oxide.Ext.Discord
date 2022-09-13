namespace Oxide.Ext.Discord.Libraries.Templates
{
    public class BulkTemplateRegistration<T> where T : BaseTemplate
    {
        public string Language { get; set; }
        public T Template { get; set; }
    }
}