using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Libraries
{
    internal class SubscriptionCallback : BaseNextTickCallback
    {
        private DiscordMessage _message;
        private Action<DiscordMessage> _callback;
        private Plugin _plugin;

        public static void Start(Plugin plugin, DiscordMessage message, Action<DiscordMessage> callback)
        {
            SubscriptionCallback sub = DiscordPool.Get<SubscriptionCallback>();
            sub._plugin = plugin;
            sub._message = message;
            sub._callback = callback;
            sub.Run();
        }

        protected override void HandleCallback()
        {
            if (_plugin == null || !_plugin.IsLoaded)
            {
                return;
            }
            
            try
            {
                _plugin.TrackStart();
                _callback.Invoke(_message);
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An exception occured for discord subscription in channel {0} for plugin {1}", _message.ChannelId, _plugin.FullName(), ex);
            }
            finally
            {
                _plugin.TrackEnd();
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
            _callback = null;
        }
    }
}