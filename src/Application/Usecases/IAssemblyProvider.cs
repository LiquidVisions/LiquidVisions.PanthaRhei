using System.Reflection;

namespace LiquidVisions.PanthaRhei.Application.Usecases
{
    internal interface IAssemblyProvider
    {
        Assembly EntryAssembly { get; }
    }
}
