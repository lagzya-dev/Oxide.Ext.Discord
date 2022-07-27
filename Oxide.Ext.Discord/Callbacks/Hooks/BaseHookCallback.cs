using Oxide.Core;

namespace Oxide.Ext.Discord.Callbacks.Hooks
{
    internal abstract class BaseHookCallback : BaseNextTickCallback
    {
        protected string Hook;
        protected object[] Args;

        protected void Init(string hook, object[] args)
        {
            Hook = hook;
            Args = args;
        }

        protected override void EnterPool()
        {
            Hook = null;
            Args = null;
        }

        protected override void DisposeInternal()
        {
            ArrayPool.Free(Args);
        }
    }
}