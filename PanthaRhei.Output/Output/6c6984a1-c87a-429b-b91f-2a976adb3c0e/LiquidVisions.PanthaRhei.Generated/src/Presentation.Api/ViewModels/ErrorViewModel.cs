using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels
{
    public class ErrorViewModel : ViewModel
    {
        public string TimeStamp => DateTime.Now.ToString();

        public string Path { get; set; }

        public List<ErrorDetails> Details = new();
    }
}
