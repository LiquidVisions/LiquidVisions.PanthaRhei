using System;
using System.Threading.Tasks;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.UpdateCore
{
    internal class UpdateCoreUseCase(ICommandLine cli) : IUpdateCoreUseCase
    {
        private readonly ICommandLine cli = cli;

        public Task<Response> Update()
        {
            Response response = new();

            try
            {
                cli.Start("dotnet tool update Liquidvisions.PanthaRhei -g");
            }
            catch (InvalidOperationException e)
            {
                response.AddError(FaultCodes.InternalServerError, $"Failed to update LiquidVisions.PanthaRhei to latest version: {e.Message}");
            }
            

            return Task.FromResult(response);
        }
    }
}
