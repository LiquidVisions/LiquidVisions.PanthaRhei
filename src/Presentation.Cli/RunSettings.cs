using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Infrastructure;

namespace LiquidVisions.PanthaRhei.Presentation.Cli
{
    internal class RunSettings(ILogger logger, IFile file) : IRunSettings
    {
        public string Path { get; set; } =
            System.IO.Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        public void Set(string section, string key, string value)
        {
            JsonObject json = Load(Path);

            SetOrClear(json, section);

            logger.Info($"Setting value {value} for key {key} in section {section}");
            json[section][key] = value;

            string jsonString = json.ToJsonString(new JsonSerializerOptions { WriteIndented = true });

            logger.Info($"Saving PanthaRhei configuration {jsonString} document to {Path}");

            file.WriteAllText(Path, jsonString);
        }

        private JsonObject Load(string path)
        {
            // Load the JSON document
            logger.Info($"Loading PanthaRhei configuration document from {path}");

            string jsonString = file.ReadAllText(path);

            return JsonNode.Parse(jsonString).AsObject();
        }

        private void SetOrClear(JsonObject json, string section)
        {
            if (json[section] != null)
            {
                logger.Info($"Clearing value {json[section]} from section {section}");
                json[section].AsObject().Clear();
            }
            else
            {
                logger.Info($"Creating new section {section}");
                json[section] = new JsonObject();
            }
        }
    }
}
