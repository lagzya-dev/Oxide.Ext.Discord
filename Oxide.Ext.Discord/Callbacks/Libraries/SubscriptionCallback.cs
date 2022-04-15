using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Callbacks.Libraries
{
    internal class SubscriptionCallback : BaseCallback
    {
        private DiscordMessage _message;
        private Action<DiscordMessage> _messageCallback;
        private Plugin _plugin;

        public void Init(Plugin plugin, DiscordMessage message, Action<DiscordMessage> messageCallback)
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

        protected override void EnterPool()
        {
            _plugin = null;
            _message = null;
            _messageCallback = null;
        }
    }
}