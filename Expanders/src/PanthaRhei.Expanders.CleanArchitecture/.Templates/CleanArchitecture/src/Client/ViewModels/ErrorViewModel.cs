using System;
using System.Collections.Generic;

namespace NS.Client.ViewModels
{
    public class ErrorViewModel : ViewModel
    {
        public string TimeStamp => DateTime.Now.ToString();

        public string Path { get; set; }

        public List<ErrorDetails> Details = new();
    }
}
