namespace LiquidVisions.PanthaRhei.Presentation.Cli
{
    internal interface IRunSettings
    {
        public string Path { get; set; }

        void Set(string section, string key, string value);
    }
}
