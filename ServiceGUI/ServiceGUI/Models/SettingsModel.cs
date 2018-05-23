using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Infrustructure;
using ServiceGUI.Events;


namespace ServiceGUI.Models
{
    class SettingsModel : INotifyPropertyChanged
    {
        // an event that raises when a property is being changed
        public event PropertyChangedEventHandler PropertyChanged;
        private string outputDirectory;
        private string sourceName;
        private string logName;
        private string thumbnailSize;
        private string chosenHandler;
        private ObservableCollection<string> directoriesCollection;
        public SettingsModel()
        {
            outputDirectory = "Output Directory:";
            sourceName = "Source Name:";
            logName = "Log Name:";
            thumbnailSize = "Thumbnail Size:";
            directoriesCollection = new ObservableCollection<string>();
            directoriesCollection.Add("Source Folder 1");
            directoriesCollection.Add("Source Folder 2");
        }
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // the output directory 
        public string OutputDirectory
        {
            get { return outputDirectory; }
            set
            {
                outputDirectory = "Output Directory:" + value;
                OnPropertyChanged("OutputDirectory");
            }
        }

        // sourceName property
        public string ServiceSourceName
        {
            get { return sourceName; }
            set
            {
                outputDirectory = "Source Name:" + value;
                OnPropertyChanged("SourceName");
            }
        }

        // logName property
        public string ServiceLogName
        {
            get { return logName; }
            set
            {
                logName = "Log Name:" + value;
                OnPropertyChanged("LogName");
            }
        }

        // thumbnail size property
        public string ThumbSize
        {
            get { return thumbnailSize; }
            set
            {
                outputDirectory = "Thumbnail Size:" + value;
                OnPropertyChanged("ThumbSize");
            }
        }

        // the selected handler property
        public string ChosenHandler
        {
            get { return chosenHandler; }
            set
            {
                chosenHandler = value;
                OnPropertyChanged("SelectedHandler");
            }
        }

        public ObservableCollection<string> DirectoriesCollection
        {
            get { return directoriesCollection; }
            set
            {
                directoriesCollection = value;
                OnPropertyChanged("Directories");
            }
        }

        //sends commands to the server
        public void SendCommandToServer()
        {
            string[] args = { };
            CommandReceivedEventArgs e = new CommandReceivedEventArgs((int)CommandsEnum.GetConfigCommand, args, "Empty");
        }
    }
}