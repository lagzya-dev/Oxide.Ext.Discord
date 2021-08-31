using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Builder for command options
    /// </summary>
    /// <typeparam name="T">Type we're building options for.</typeparam>
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
                throw new Exception($"{type} is not allowed to be used here. Valid types are any non command type.");
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
                throw new Exception($"Cannot add a string choice to non string type: {_option.Type}");
            }
            
            return AddChoice(name, (object)value);
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
                throw new Exception($"Cannot add a integer choice to non integer type: {_option.Type}");
            }

            return AddChoice(name, (object)value);
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
                throw new Exception($"Cannot add a number choice to non number type: {_option.Type}");
            }

            return AddChoice(name, (object)value);
        }

        private CommandOptionBuilder<T> AddChoice(string name, object value)
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
        /// Builds the option and returns the T previous builder
        /// </summary>
        /// <returns></returns>
        public T Build()
        {
            return _builder;
        }
    }
}