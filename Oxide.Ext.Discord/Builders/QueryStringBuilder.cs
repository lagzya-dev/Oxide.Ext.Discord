using System.Collections.Generic;
using System.Text;
using Oxide.Ext.Discord.Types;

namespace Oxide.Ext.Discord.Builders;

/// <summary>
/// Builder used to build query strings for urls
/// </summary>
public class QueryStringBuilder : BasePoolable
{
    private StringBuilder _builder;

    /// <summary>
    /// Creates a pooled <see cref="QueryStringBuilder"/>
    /// </summary>
    /// <returns><see cref="QueryStringBuilder"/></returns>
    public static QueryStringBuilder Create(DiscordPluginPool pool) => pool.Get<QueryStringBuilder>();

    /// <summary>
    /// Add a key value pair to the query string
    /// </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Key value</param>
    public void Add(string key, string value)
    {
        AddKey(key);
        _builder.Append(value);
    }

    /// <summary>
    /// Add a list of values with the specified separator
    /// </summary>
    /// <param name="key">Key name</param>
    /// <param name="list">List to be added</param>
    /// <param name="separator">Separator for the list</param>
    /// <typeparam name="T"></typeparam>
    public void AddList<T>(string key, List<T> list, string separator)
    {
        AddKey(key);
        for (int index = 0; index < list.Count; index++)
        {
            T entry = list[index];
            _builder.Append(entry.ToString());
            if (index + 1 != list.Count)
            {
                _builder.Append(separator);
            }
        }
    }

    private void AddKey(string key)
    {
        if (_builder.Length > 1)
        {
            _builder.Append('&');
        }
            
        _builder.Append(key);
        _builder.Append('=');
    }

    /// <summary>
    /// Returns the query string as a string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return _builder.Length <= 1 ? string.Empty : _builder.ToString();
    }
        
    /// <summary>
    /// Returns the query string and returns the builder to the pool
    /// </summary>
    /// <returns></returns>
    public string ToStringAndFree()
    {
        string query = ToString();
        Dispose();
        return query;
    }

    /// <inheritdoc/>
    protected override void EnterPool()
    {
        PluginPool.FreeStringBuilder(_builder);
    }

    /// <inheritdoc/>
    protected override void LeavePool()
    {
        _builder = PluginPool.GetStringBuilder();
        _builder.Append("?");
    }
}