
namespace Oxide.Ext.Discord.Data
{
    internal abstract class BaseDataFile<TData> where TData : BaseDataFile<TData>, new()
    {
        internal static TData Instance;
        
        internal bool DataUpdated { get; private set; }

        internal virtual void OnDataLoaded() { }
        
        internal void OnDataChanged()
        {
            DataUpdated = true;
        }

        internal void OnDataSaved()
        {
            DataUpdated = false;
        }
    }
}