using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders
{
    /// <summary>
    /// Builder for command options
    /// </summary>
    public abstract class BaseCommandOptionBuilder<TBuilder, TParent>
        where TBuilder : BaseCommandOptionBuilder<TBuilder, TParent>
    {
        private readonly CommandOption _option;
        private readonly TBuilder _builder;
        private readonly TParent _parent;

        internal BaseCommandOptionBuilder(List<CommandOption> parent, CommandOptionType type, string name, string description, TParent parentBuilder, string defaultLanguage)
        {
            InvalidCommandOptionException.ThrowIfInvalidName(name, false);
            InvalidCommandOptionException.ThrowIfInvalidDescription(description, false);
            InvalidCommandOptionException.ThrowIfInvalidType(type);

            _option = new CommandOption(name, description, type);

            parent.Add(_option);
            _builder = (TBuilder)this;
            _parent = parentBuilder;

            AddNameLocalization(name, defaultLanguage);
            AddDescriptionLocalization(description, defaultLanguage);
        }
        
        /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddNameLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddNameLocalization(string name, string lang) instead.")]
        public TBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            _option.NameLocalizations = DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return _builder;
        }
        
        public TBuilder AddNameLocalization(string name, string lang)
        {
            if (DiscordExtension.DiscordLang.TryGetDiscordLocale(lang, out string discordLocale))
            {
                lang = discordLocale;
            }
            
            _option.NameLocalizations[lang] = name;
            return _builder;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddDescriptionLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddDescriptionLocalization(string name, string lang) instead.")]
        public TBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            _option.DescriptionLocalizations = DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return _builder;
        }
        
        public TBuilder AddDescriptionLocalization(string name, string lang)
        {
            if (DiscordExtension.DiscordLang.TryGetDiscordLocale(lang, out string discordLocale))
            {
                lang = discordLocale;
            }
            
            _option.DescriptionLocalizations[lang] = name;
            return _builder;
        }

        /// <summary>
        /// Set the required state for the option
        /// </summary>
        /// <param name="required">If the option is required (Default: true)</param>
        /// <returns>This</returns>
        public TBuilder Required(bool required = true)
        {
            _option.Required = required;
            return _builder;
        }
        
        /// <summary>
        /// Enable auto complete for the option
        /// </summary>
        /// <param name="autoComplete">If the option support auto complete (Default: true)</param>
        /// <returns>This</returns>
        public TBuilder AutoComplete(bool autoComplete = true)
        {
            _option.Autocomplete = autoComplete;
            return _builder;
        }
        
        /// <summary>
        /// Min Value for Integer Option
        /// </summary>
        /// <param name="minValue">Min Value</param>
        /// <returns>This</returns>
        public TBuilder SetMinValue(int minValue)
        {
            InvalidCommandOptionException.ThrowIfInvalidMinIntegerType(_option.Type);
            _option.MinValue = minValue;
            return _builder;
        }
        
        /// <summary>
        /// Min Value for Number Option
        /// </summary>
        /// <param name="minValue">Min Value</param>
        /// <returns>This</returns>
        public TBuilder SetMinValue(double minValue)
        {
            InvalidCommandOptionException.ThrowIfInvalidMinNumberType(_option.Type);
            _option.MinValue = minValue;
            return _builder;
        }
        
        /// <summary>
        /// Max Value for Integer Option
        /// </summary>
        /// <param name="maxValue">Max Value</param>
        /// <returns>This</returns>
        public TBuilder SetMaxValue(int maxValue)
        {
            InvalidCommandOptionException.ThrowIfInvalidMaxIntegerType(_option.Type);
            _option.MaxValue = maxValue;
            return _builder;
        }
        
        /// <summary>
        /// Max Value for Number Option
        /// </summary>
        /// <param name="maxValue">Max Value</param>
        /// <returns>This</returns>
        public TBuilder SetMaxValue(double maxValue)
        {
            InvalidCommandOptionException.ThrowIfInvalidMaxNumberType(_option.Type);
            _option.MaxValue = maxValue;
            return _builder;
        }
        
        /// <summary>
        /// Min Length for String Option
        /// Max Of 6000
        /// </summary>
        /// <param name="minLength">Min Length for the string</param>
        /// <returns>This</returns>
        public TBuilder SetMinLength(int minLength)
        {
            InvalidCommandOptionException.ThrowIfInvalidMinLengthType(_option.Type);
            InvalidCommandOptionException.ThrowIfInvalidMinLength(minLength);
            _option.MinLength = minLength;
            return _builder;
        }
        
        /// <summary>
        /// Max Length for String Option
        /// Max Of 6000
        /// </summary>
        /// <param name="maxLength">Max Length</param>
        /// <returns>This</returns>
        public TBuilder SetMaxLength(int maxLength)
        {
            InvalidCommandOptionException.ThrowIfInvalidMaxLengthType(_option.Type);
            InvalidCommandOptionException.ThrowIfInvalidMaxLength(maxLength);
            _option.MaxLength = maxLength;
            return _builder;
        }
        
        /// <summary>
        /// Set's the channel types for the option
        /// </summary>
        /// <param name="types">Types of channels the option allows</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if <see cref="CommandOptionType"/> is not Channel</exception>
        public TBuilder SetChannelTypes(List<ChannelType> types)
        {
            InvalidCommandOptionException.ThrowIfInvalidChannelType(_option.Type);
            _option.ChannelTypes = types;
            return _builder;
        }

        /// <summary>
        /// Adds a choice to this option of type string
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <param name="nameLocalizations">Localizations for the name</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if option type is not string</exception>
        public TBuilder AddChoice(string name, string value, Hash<string, string> nameLocalizations = null)
        {
            InvalidCommandOptionChoiceException.ThrowIfInvalidType(_option.Type, CommandOptionType.String);
            InvalidCommandOptionChoiceException.ThrowIfInvalidStringValue(name);
            return AddChoiceInternal(name, value, nameLocalizations);
        }

        /// <summary>
        /// Adds a choice to this option of type int
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <param name="nameLocalizations">Localizations for the name</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if option type is not int</exception>
        public TBuilder AddChoice(string name, int value, Hash<string, string> nameLocalizations = null)
        {
            InvalidCommandOptionChoiceException.ThrowIfInvalidType(_option.Type, CommandOptionType.Integer);
            return AddChoiceInternal(name, value, nameLocalizations);
        }
        
        /// <summary>
        /// Adds a choice to this option of type double
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <param name="nameLocalizations">Localizations for the name</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if option type is not double</exception>
        public TBuilder AddChoice(string name, double value, Hash<string, string> nameLocalizations = null)
        {
            InvalidCommandOptionChoiceException.ThrowIfInvalidType(_option.Type, CommandOptionType.Number);
            return AddChoiceInternal(name, value, nameLocalizations);
        }

        private TBuilder AddChoiceInternal(string name, object value, Hash<string, string> nameLocalizations)
        {
            InvalidCommandOptionChoiceException.ThrowIfInvalidName(name, false);
            
            if (_option.Choices == null)
            {
                _option.Choices = new List<CommandOptionChoice>();
            }
            
            InvalidCommandOptionChoiceException.ThrowIfMaxChoices(_option.Choices.Count);
            
            _option.Choices.Add(new CommandOptionChoice(name, value, nameLocalizations));
            
            return _builder;
        }

        /// <summary>
        /// Builds the CommandOptionBuilder
        /// </summary>
        /// <returns></returns>
        public TParent Build()
        {
            return _parent;
        }
    }
}