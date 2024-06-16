using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.UseCases
{
    internal class SetDatabaseUseCase(ILogger logger) : ICommand<SetDatabaseCommandModel>
    {
        public bool Enabled => true;

        public void Execute(SetDatabaseCommandModel model)
        {
            // Path to the appsettings.json file
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            JsonObject jsonObject = Load(path);

            Clear(jsonObject);

            Set(model, path, jsonObject);

            logger.Info("Successfully configured the connection to the source database of the models.");
            logger.Info($"Database {model.Name} will be used from now on.");
        }

        private static void Set(SetDatabaseCommandModel model, string path, JsonObject jsonObject)
        {
            jsonObject["ConnectionStrings"][model.Name] = "PUT_YOU_CONNECTION_STRING HERE";

            // Save the updated JSON back to the file
            File.WriteAllText(path, jsonObject.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }

        private static void Clear(JsonObject jsonObject)
        {
            if (jsonObject["ConnectionStrings"] != null)
            {
                jsonObject["ConnectionStrings"].AsObject().Clear();
            }
            else
            {
                jsonObject["ConnectionStrings"] = new JsonObject();
            }
        }

        private static JsonObject Load(string path)
        {
            // Load the JSON document
            string jsonString = File.ReadAllText(path);
            JsonObject jsonObject = JsonNode.Parse(jsonString).AsObject();
            return jsonObject;
        }
    }
}
