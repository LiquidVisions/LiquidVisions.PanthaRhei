using System.Reflection;

namespace LiquidVisions.PanthaRhei.Application.Usecases
{
    internal class AssemblyProvider : IAssemblyProvider
    {
        public Assembly EntryAssembly => Assembly.GetEntryAssembly();
    }
}
