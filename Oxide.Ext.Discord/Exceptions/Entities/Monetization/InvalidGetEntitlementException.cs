namespace Oxide.Ext.Discord.Exceptions;

/// <summary>
/// Exceptions for invalid entitlements
/// </summary>
public class InvalidGetEntitlementException : BaseDiscordException
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Exception message</param>
    private InvalidGetEntitlementException(string message) : base(message) { }
        
    internal static void ThrowIfInvalidLimit(int? limit)
    {
        const int MinLimit = 1;
        const int MaxLimit = 100;
            
        if (limit < MinLimit)
        {
            throw new InvalidGetEntitlementException($"Limit cannot be less than {MinLimit}");
        }
            
        if (limit > MaxLimit)
        {
            throw new InvalidGetEntitlementException($"Limit cannot be more than {MaxLimit}");
        }
    }
}