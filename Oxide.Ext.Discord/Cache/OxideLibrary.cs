using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Types;

namespace Oxide.Ext.Discord.Cache
{
    internal sealed class OxideLibrary : Singleton<OxideLibrary>
    {
        internal readonly Covalence Covalence = Interface.Oxide.GetLibrary<Covalence>();
        internal readonly Permission Permission = Interface.Oxide.GetLibrary<Permission>();
        internal readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();
        internal readonly Oxide.Core.Libraries.Plugins Plugins = Interface.Oxide.GetLibrary<Oxide.Core.Libraries.Plugins>();
        
        private OxideLibrary() { }
    }
}