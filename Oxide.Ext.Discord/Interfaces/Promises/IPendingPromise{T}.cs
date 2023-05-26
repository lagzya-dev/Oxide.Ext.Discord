// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

namespace Oxide.Ext.Discord.Interfaces.Promises
{
    public interface IPendingPromise<TPromised> : IPromise<TPromised>, IRejectable
    {
        void Resolve(TPromised value);
    }
}