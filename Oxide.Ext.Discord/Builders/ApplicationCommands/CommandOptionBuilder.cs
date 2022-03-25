using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Builder for command options
    /// </summary>
    public class CommandOptionBuilder<T>
    {
        private readonly CommandOption _option;
        private readonly T _builder;
        
        internal CommandOptionBuilder(List<CommandOption> parent, CommandOptionType type, string name, string description, T builder)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Value cannot be null or empty.", nameof(description));
            
            if (type == CommandOptionType.SubCommand || type == CommandOptionType.SubCommandGroup)
            {
                throw new InvalidApplicationCommandException($"{type} is not allowed to be used here. Valid types are any non command type.");
            }

            _option = new CommandOption
            {
                Name = name,
                Description = description,
                Type = type
            };
            parent.Add(_option);
            _builder = builder;
        }
        
        /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public CommandOptionBuilder<T> AddNameLocalizations(Plugin plugin, string langKey)
        {
            _option.NameLocalizations = LocaleConverter.GetCommandLocalization(plugin, langKey);
            return this;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public CommandOptionBuilder<T> AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            _option.DescriptionLocalizations = LocaleConverter.GetCommandLocalization(plugin, langKey);
            return this;
        }

        /// <summary>
        /// Set the required state for the option
        /// </summary>
        /// <param name="required">If the option is required (Default: true)</param>
        /// <returns>This</returns>
        public CommandOptionBuilder<T> Required(bool required = true)
        {
            _option.Required = required;
            return this;
        }
        
        /// <summary>
        /// Enable auto complete for the option
        /// </summary>
        /// <param name="autoComplete">If the option support auto complete (Default: true)</param>
        /// <returns>This</returns>
        public CommandOptionBuilder<T> AutoComplete(bool autoComplete = true)
        {
            _option.Autocomplete = autoComplete;
            return this;
        }
        
        /// <summary>
        /// Min Value for Integer Option
        /// </summary>
        /// <param name="minValue">Min Value</param>
        /// <returns>This</returns>
        public CommandOptionBuilder<T> SetMinValue(int minValue)
        {
            if (_option.Type != CommandOptionType.Integer && _option.Type != CommandOptionType.Number)
            {
                throw new InvalidApplicationCommandException("Can only set min value for Integer or Number Type");
            }
            
            _option.MinValue = minValue;
            return this;
        }
        
        /// <summary>
        /// Min Value for Number Option
        /// </summary>
        /// <param name="minValue">Min Value</param>
        /// <returns>This</returns>
        public CommandOptionBuilder<T> SetMinValue(double minValue)
        {
            if (_option.Type != CommandOptionType.Number)
            {
                throw new InvalidApplicationCommandException("Can only set min value for Number Type");
            }
            
            _option.MinValue = minValue;
            return this;
        }
        
        /// <summary>
        /// Max Value for Integer Option
        /// </summary>
        /// <param name="maxValue">Min Value</param>
        /// <returns>This</returns>
        public CommandOptionBuilder<T> SetMaxValue(int maxValue)
        {
            if (_option.Type != CommandOptionType.Integer && _option.Type != CommandOptionType.Number)
            {
                throw new InvalidApplicationCommandException("Can only set max value for Integer or Number Type");
            }
            
            _option.MaxValue = maxValue;
            return this;
        }
        
        /// <summary>
        /// Max Value for Number Option
        /// </summary>
        /// <param name="maxValue">Min Value</param>
        /// <returns>This</returns>
        public CommandOptionBuilder<T> SetMaxValue(double maxValue)
        {
            if (_option.Type != CommandOptionType.Integer && _option.Type != CommandOptionType.Number)
            {
                throw new InvalidApplicationCommandException("Can only set max value for Number Type");
            }
            
            _option.MaxValue = maxValue;
            return this;
        }
        
        /// <summary>
        /// Set's the channel types for the option
        /// </summary>
        /// <param name="types">Types of channels the option allows</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if <see cref="CommandOptionType"/> is not Channel</exception>
        public CommandOptionBuilder<T> SetChannelTypes(List<ChannelType> types)
        {
            if (_option.Type != CommandOptionType.Channel)
            {
                throw new InvalidApplicationCommandException("Can only set ChannelTypes for CommandOptionType.Channel");
            }
            
            _option.ChannelTypes = types;
            return this;
        }

        /// <summary>
        /// Adds a choice to this option of type string
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if option type is not string</exception>
        public CommandOptionBuilder<T> AddChoice(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            
            if (_option.Type != CommandOptionType.String)
            {
                throw new InvalidApplicationCommandException($"Cannot add a string choice to non string type: {_option.Type}");
            }
            
            return AddChoiceInternal(name, value);
        }
        
        
        /// <summary>
        /// Adds a choice to this option of type int
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if option type is not int</exception>
        public CommandOptionBuilder<T> AddChoice(string name, int value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            
            if (_option.Type != CommandOptionType.Integer)
            {
                throw new InvalidApplicationCommandException($"Cannot add a integer choice to non integer type: {_option.Type}");
            }

            return AddChoiceInternal(name, value);
        }
        
        /// <summary>
        /// Adds a choice to this option of type double
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if option type is not double</exception>
        public CommandOptionBuilder<T> AddChoice(string name, double value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            
            if (_option.Type != CommandOptionType.Number)
            {
                throw new InvalidApplicationCommandException($"Cannot add a number choice to non number type: {_option.Type}");
            }

            return AddChoiceInternal(name, value);
        }

        private CommandOptionBuilder<T> AddChoiceInternal(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            
            if (_option.Choices == null)
            {
                _option.Choices = new List<CommandOptionChoice>();
            }
            
            _option.Choices.Add(new CommandOptionChoice
            {
                Name = name,
                Value = value
            });
            
            return this;
        }

        /// <summary>
        /// Builds the CommandOptionBuilder
        /// </summary>
        /// <returns></returns>
        public T Build()
        {
            return _builder;
        }
    }
}