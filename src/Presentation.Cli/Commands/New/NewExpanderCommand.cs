using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.New
{
    internal class NewExpanderCommand : CommandLineApplicationBase
    {
        private readonly CommandOption appId;
        private readonly CommandOption nameOption;
        private readonly CommandOption typeOption;
        private readonly CommandOption pathOption;
        private readonly CommandOption buildPathOption;
        private readonly CommandOption<bool> buildOption;
        private readonly IDependencyFactory dependencyFactory;

        public NewExpanderCommand(IDependencyFactory dependencyFactory)
        {
            Name = "expander";
            HelpOption("-?", true);

            appId = Option(
                "-a|--appId",
                "The identifier of the app to which the Expander should be added to.",
                CommandOptionType.SingleValue);

            nameOption = Option(
                "--shortName",
                "Name of the new expander class.",
                CommandOptionType.SingleValue)
                .IsRequired();

            typeOption = Option(
                "--fullName",
                "Type of the new expander solution.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            pathOption = Option(
                "--path",
                "Path where the expander will be created.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            buildPathOption = Option(
                "--buildPath",
                "Path where the expander will outputs' its builds.",
                CommandOptionType.SingleValue)
                .IsRequired(true);

            buildOption = this.Option<bool>(
                "--build",
                "Builds the newly created expander",
                CommandOptionType.SingleOrNoValue);

            this.dependencyFactory = dependencyFactory;
        }

        public override async void OnExecute()
        {
            IBoundary boundary = dependencyFactory.Resolve<IBoundary>();
            ILogger logger = dependencyFactory.Resolve<ILogger>();

            NewExpanderRequestModel model = ComposeModel();

            Response response = await boundary
                .CreateNewExpander(model)
                .ConfigureAwait(true);

            HandleResponse(logger, response);
        }

        private static void HandleResponse(ILogger logger, Response response)
        {
            string message = response.IsValid
                ? Resources.ExpanderCreatedSuccessfully
                : ComposeFaultMessage(response.Errors);

            Action<string> logAction = response.IsValid
                ? logger.Info
                : logger.Fatal;

            logAction(message);
        }

        private static string ComposeFaultMessage(IEnumerable<Fault> faults)
        {
            StringBuilder sb = new();
            foreach(Fault fault in faults)
            {
                sb.AppendLine(
                    CultureInfo.InvariantCulture,
                    $"{fault.FaultCode.Code}:{fault.FaultCode.Message} - Error creating expander: {fault.FaultMessage}");
            }

            return sb.ToString();
        }

        private NewExpanderRequestModel ComposeModel() => new()
        {
            ShortName = nameOption.Value(),
            FullName = typeOption.Value(),
            Path = pathOption.Value(),
            BuildPath = buildPathOption.Value(),
            Build = buildOption.HasValue(),
            AppId = string.IsNullOrEmpty(appId.Value()) ? Guid.Empty : Guid.Parse(appId.Value())
        };
    }
}
