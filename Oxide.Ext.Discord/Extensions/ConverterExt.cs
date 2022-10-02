namespace Oxide.Ext.Discord.Extensions
{
    internal static class ConverterExt
    {
        public static TTarget Convert<TTarget, TSource>(TSource entity) where TTarget : TSource => (TTarget)entity;
    }
}