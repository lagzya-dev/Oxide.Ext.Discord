using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Libraries
{
    internal class SubscriptionCallback : BaseNextTickCallback
    {
        private DiscordMessage _message;
        private Action<DiscordMessage> _messageCallback;
        private Plugin _plugin;

        public static SubscriptionCallback CreateCallback(Plugin plugin, DiscordMessage message, Action<DiscordMessage> messageCallback)
        {
            SubscriptionCallback callback = DiscordPool.Get<SubscriptionCallback>();
            callback.Init(plugin, message, messageCallback);
            return callback;
        }
        
        private void Init(Plugin plugin, DiscordMessage message, Action<DiscordMessage> messageCallback)
        {
            _message = message;
            _messageCallback = messageCallback;
            _plugin = plugin;
        }
        
        protected override void HandleCallback()
        {
            try
            {
                _plugin.TrackStart();
                _messageCallback.Invoke(_message);
                _plugin.TrackEnd();
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An exception occured for discord subscription in channel {0} for plugin {1}", _message?.ChannelId, _plugin?.Name, ex);
            }
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        protected override void EnterPool()
        {
            _plugin = null;
            _message = null;
            _messageCallback = null;
        }
    }
}