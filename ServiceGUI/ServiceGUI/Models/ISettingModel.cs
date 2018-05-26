using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Models
{
    public interface ISettingsModel : INotifyPropertyChanged
    {
        string OutputDirectory { get; set; }
        string ServiceSourceName { get; set; }
        string ServiceLogName { get; set; }
        string ThumbSize { get; set; }
        string ChosenHandler { get; set; }
        ObservableCollection<string> DirectoriesCollection { get; set; }
    }
}
