using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCommander
{
    public class FileCopyViewModel : BaseViewModel
    {
        public double ProgressValue { get; set; }

        public Dictionary<string,string> SourceDestination { get; set; }

        public string CopyMessage { get; set; }

        public FileCopyViewModel(Dictionary<string,string> sourDest)
        {
            this.ProgressValue = 1.0;
            this.SourceDestination = sourDest;
            var key = sourDest.Keys.First() ?? string.Empty;
            this.CopyMessage = string.Format($"Хотите скопировать из {key} в { sourDest[key] ?? string.Empty}?");
            
        }
    }
}
