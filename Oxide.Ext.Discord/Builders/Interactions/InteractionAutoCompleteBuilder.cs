using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Exceptions.Builders;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Builders.Interactions
{
    /// <summary>
    /// Builder for Auto Complete Interaction
    /// </summary>
    public class InteractionAutoCompleteBuilder
    {
        private readonly InteractionAutoCompleteMessage _message;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interaction">Interaction this build is for</param>
        public InteractionAutoCompleteBuilder(DiscordInteraction interaction) : this(interaction, new InteractionAutoCompleteMessage()) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interaction">Interaction this build is for</param>
        /// <param name="message">Starting <see cref="InteractionAutoCompleteMessage"/></param>
        public InteractionAutoCompleteBuilder(DiscordInteraction interaction, InteractionAutoCompleteMessage message)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsNotAutoComplete(interaction.Type);
            _message = message;
            if (_message.Choices == null)
            {
                _message.Choices = new List<CommandOptionChoice>();
            }
        }

        private static readonly Permission Permissions = Interface.Oxide.GetLibrary<Permission>();
        private static readonly Covalence Covalence = Interface.Oxide.GetLibrary<Covalence>();

        /// <summary>
        /// Adds a <see cref="CommandOptionChoice"/> to the response
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <returns>This</returns>
        public InteractionAutoCompleteBuilder AddAutoCompleteChoice(string name, object value)
        {
            InvalidAutoCompleteChoiceException.ThrowIfInvalidName(name);
            InvalidAutoCompleteChoiceException.ThrowIfInvalidValue(value);
            return AddAutoCompleteChoice(new CommandOptionChoice(name, value));
        }

        /// <summary>
        /// Adds a <see cref="CommandOptionChoice"/> to the response
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <param name="plugin">Plugin to lookup the langkey for</param>
        /// <param name="langKey">Lang key for the name</param>
        /// <returns>This</returns>
        [Obsolete("AddAutoCompleteChoice has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public InteractionAutoCompleteBuilder AddAutoCompleteChoice(string name, object value, Plugin plugin, string langKey)
        {
            InvalidAutoCompleteChoiceException.ThrowIfInvalidName(name);
            InvalidAutoCompleteChoiceException.ThrowIfInvalidValue(value);
            return AddAutoCompleteChoice(new CommandOptionChoice(name, value,  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey)));
        }
        
        /// <summary>
        /// Adds a <see cref="CommandOptionChoice"/> to the response
        /// </summary>
        /// <param name="choice">Choice to be added</param>
        /// <returns>This</returns>
        public InteractionAutoCompleteBuilder AddAutoCompleteChoice(CommandOptionChoice choice)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            InvalidCommandOptionChoiceException.ThrowIfMaxChoices(_message.Choices.Count + 1);
            _message.Choices.Add(choice);
            return this;
        }
        
        /// <summary>
        /// Adds a collection of <see cref="CommandOptionChoice"/> to the response
        /// </summary>
        /// <param name="choices">Choices to be added</param>
        /// <returns>This</returns>
        public InteractionAutoCompleteBuilder AddAutoCompleteChoices(ICollection<CommandOptionChoice> choices)
        {
            if (choices == null) throw new ArgumentNullException(nameof(choices));

            InvalidCommandOptionChoiceException.ThrowIfMaxChoices(_message.Choices.Count + choices.Count);
            _message.Choices.AddRange(choices);
            return this;
        }

        /// <summary>
        /// Returns if the Auto Complete can add any more choices
        /// </summary>
        /// <returns></returns>
        public bool CanAddChoice()
        {
            return (_message.Choices?.Count ?? 0) < 25;
        }

        /// <summary>
        /// Returns the built message
        /// </summary>
        /// <returns><see cref="InteractionAutoCompleteMessage"/></returns>
        public InteractionAutoCompleteMessage Build()
        {
            return _message;
        }

        #region Oxide
        /// <summary>
        /// Adds Oxide Groups to the AutoComplete
        /// </summary>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddGroups(string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            AddList(Permissions.GetGroups(), filter, comparison, search);
        }
        
        /// <summary>
        /// Adds Oxide Permissions to the AutoComplete
        /// </summary>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddPermissions(string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            AddList(Permissions.GetPermissions(), filter, comparison, search);
        }

        /// <summary>
        /// Adds Oxide Group Permissions to the AutoComplete
        /// </summary>
        /// <param name="group">Group to get the permissions for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddPermissionsInGroup(string group, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            AddList(Permissions.GetGroupPermissions(group), filter, comparison, search);
        }
        
        /// <summary>
        /// Adds Oxide Permissions Not In Group to the AutoComplete
        /// </summary>
        /// <param name="group">Group to get the permissions for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddPermissionsNotInGroup(string group, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            string[] groupPerms = Permissions.GetGroupPermissions(group);
            AddList(Permissions.GetPermissions().Except(groupPerms), filter, comparison, search);
        }
        
        /// <summary>
        /// Adds The List of Groups that have this permission
        /// </summary>
        /// <param name="permission">Permission to get groups for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddGroupsWithPermission(string permission, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            AddList(Permissions.GetPermissionGroups(permission), filter, comparison, search);
        }
        
        /// <summary>
        /// Adds The List of Groups that have this permission
        /// </summary>
        /// <param name="permission">Permission to get groups for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddGroupsWithoutPermission(string permission, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            string[] groups = Permissions.GetPermissionGroups(permission);
            AddList(Permissions.GetGroups().Except(groups), filter, comparison, search);
        }

        /// <summary>
        /// Adds The List of Groups that playerId has
        /// </summary>
        /// <param name="playerId">Player ID to get groups for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddGroupsUserIn(string playerId, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            AddList(Permissions.GetUserGroups(playerId), filter, comparison, search);
        }
        
        /// <summary>
        /// Adds The List of Groups that playerId has
        /// </summary>
        /// <param name="playerId">Player ID to get groups for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddGroupsUserNotIn(string playerId, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            string[] groups = Permissions.GetGroups();
            AddList(Permissions.GetUserGroups(playerId).Except(groups), filter, comparison, search);
        }
        
        /// <summary>
        /// Adds The List of Permissions that playerId has
        /// </summary>
        /// <param name="playerId">Player ID to get permissions for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddPermissionsUserIn(string playerId, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            AddList(Permissions.GetUserPermissions(playerId), filter, comparison, search);
        }
        
        /// <summary>
        /// Adds The List of Permissions that playerId has
        /// </summary>
        /// <param name="playerId">Player ID to get permissions for</param>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        public void AddPermissionsUserNotIn(string playerId, string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
        {
            string[] perms = Permissions.GetPermissions();
            AddList(Permissions.GetUserPermissions(playerId).Except(perms), filter, comparison, search);
        }

        /// <summary>
        /// Adds Online Players to the list
        /// </summary>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        /// <param name="options"><see cref="AutoCompletePlayerSearchOptions"/> Search Options</param>
        public void AddOnlinePlayers(string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.Contains, AutoCompletePlayerSearchOptions options = AutoCompletePlayerSearchOptions.Default)
        {
            AddPlayerList(Covalence.Players.Connected, filter, comparison, search, options);
        }
        
        /// <summary>
        /// Adds Online Players to the list
        /// </summary>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        /// <param name="options"><see cref="AutoCompletePlayerSearchOptions"/> Search Options</param>
        public void AddOfflinePlayers(string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.Contains, AutoCompletePlayerSearchOptions options = AutoCompletePlayerSearchOptions.Default)
        {
            AddPlayerList(Covalence.Players.All.Where(p => !p.IsConnected), filter, comparison, search, options);
        }
        
        /// <summary>
        /// Adds Online Players to the list first
        /// If there is still space add Offline Players
        /// </summary>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        /// <param name="options"><see cref="AutoCompletePlayerSearchOptions"/> Search Options</param>
        public void AddAllOnlineFirstPlayers(string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.Contains, AutoCompletePlayerSearchOptions options = AutoCompletePlayerSearchOptions.Default)
        {
            AddPlayerList(Covalence.Players.Connected, filter, comparison, search, options);
            AddPlayerList(Covalence.Players.All, filter, comparison, search, options);
        }
        
        /// <summary>
        /// Adds Any Player to the list
        /// </summary>
        /// <param name="filter">String to filter by</param>
        /// <param name="comparison"><see cref="StringComparison"/> to use</param>
        /// <param name="search"><see cref="AutoCompleteSearchMode"/> Filter search mode</param>
        /// <param name="options"><see cref="AutoCompletePlayerSearchOptions"/> Search Options</param>
        public void AddAllPlayers(string filter = null, StringComparison comparison = StringComparison.OrdinalIgnoreCase, AutoCompleteSearchMode search = AutoCompleteSearchMode.Contains, AutoCompletePlayerSearchOptions options = AutoCompletePlayerSearchOptions.Default)
        {
            AddPlayerList(Covalence.Players.All, filter, comparison, search, options);
        }

        private void AddList(IEnumerable<string> list, string filter, StringComparison comparison, AutoCompleteSearchMode search)
        {
            if (!CanAddChoice())
            {
                return;
            }
            
            List<CommandOptionChoice> choices = _message.Choices;
            foreach (string value in list)
            {
                if (IsMatch(value, filter, comparison, search))
                {
                    choices.Add(new CommandOptionChoice(value, value));
                    if (!CanAddChoice())
                    {
                        return;
                    }
                }
            }
        }

        private void AddPlayerList(IEnumerable<IPlayer> list, string filter, StringComparison comparison, AutoCompleteSearchMode search, AutoCompletePlayerSearchOptions options)
        {
            if (!CanAddChoice())
            {
                return;
            }
            
            List<CommandOptionChoice> choices = _message.Choices;
            foreach (IPlayer player in list)
            {
                string name = DiscordExtensionCore.Instance.GetPlayerName(player, options);
                if (IsMatch(name, filter, comparison, search))
                {
                    choices.Add(new CommandOptionChoice(name, player.Id));
                    if (!CanAddChoice())
                    {
                        return;
                    }
                }
            }
        }
        
        private static bool IsMatch(string value, string filter, StringComparison comparison, AutoCompleteSearchMode search)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return true;
            }
            
            bool match = false;
            switch (search)
            {
                case AutoCompleteSearchMode.StartsWith:
                    match = value.StartsWith(filter, comparison);
                    break;
                case AutoCompleteSearchMode.Contains:
                    match = value.IndexOf(filter, comparison) >= 0;
                    break;
                case AutoCompleteSearchMode.EndsWith:
                    match = value.EndsWith(filter, comparison);
                    break;
            }

            return match;
        }
        #endregion
    }
}