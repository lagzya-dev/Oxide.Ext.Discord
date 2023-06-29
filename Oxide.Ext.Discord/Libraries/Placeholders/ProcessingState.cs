namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    internal struct ProcessingState
    {
        private readonly PlaceholderData _data;
        private readonly bool _disposable;

        public ProcessingState(PlaceholderData data) 
        {
            _data = data;
            if (data != null && data.AutoPool)
            {
                _disposable = true;
                data.ManualPool();
            }
            else
            {
                _disposable = false;
            }
        }

        public void Complete()
        {
            if (_disposable)
            {
                _data.Dispose();
            }
        }
    }
}