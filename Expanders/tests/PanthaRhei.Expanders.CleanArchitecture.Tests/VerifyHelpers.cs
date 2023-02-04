using Newtonsoft.Json;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests
{
    internal static class VerifyHelpers
    {
        public static bool AreEqualObjects(object expected, object actual)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var expectedJson = JsonConvert.SerializeObject(expected, settings);
            var actualJson = JsonConvert.SerializeObject(actual, settings);

            return expectedJson == actualJson;
        }
    }
}
