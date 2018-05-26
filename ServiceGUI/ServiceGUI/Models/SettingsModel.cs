using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Infrustructure;
using ServiceGUI.Events;
using System.Windows.Data;

namespace ServiceGUI.Models
{
    class SettingsModel : ISettingsModel
    {
        // an event that raises when a property is being changed
        private static SettingsModel settingsModel = null;
        public event PropertyChangedEventHandler PropertyChanged;
        private string outputDirectory;
        private string sourceName;
        private string logName;
        private string thumbnailSize;
        private string chosenHandler;
        private ObservableCollection<string> directoriesCollection;
        private SettingsModel()
        {
            outputDirectory = "Output Directory:";
            sourceName = "Source Name:";
            logName = "Log Name:";
            thumbnailSize = "Thumbnail Size:";
            directoriesCollection = new ObservableCollection<string>();
            Object locker = new object();
            BindingOperations.EnableCollectionSynchronization(directoriesCollection, locker);
        }

        public static SettingsModel getModel()
        {
            if(settingsModel==null)
            {
                settingsModel = new SettingsModel();
            }
            return settingsModel;
        }

        /// <summary>
        /// Update that the property was changed
        /// </summary>
        /// <param name="name">The property that was changed.</param>
        public void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // the output directory 
        public string OutputDirectory
        {
            get { return outputDirectory; }
            set
            {
                outputDirectory = "OutputDirectory: " + value;
                OnPropertyChanged("OutputDirectory");
            }
        }

        // sourceName property
        public string ServiceSourceName
        {
            get { return sourceName; }
            set
            {
                sourceName = "Source Name: " + value;
                OnPropertyChanged("ServiceSourceName");
            }
        }

        // logName property
        public string ServiceLogName
        {
            get { return logName; }
            set
            {
                logName = "Log Name: " + value;
                OnPropertyChanged("ServiceLogName");
            }
        }

        // thumbnail size property
        public string ThumbSize
        {
            get { return thumbnailSize; }
            set
            {
                thumbnailSize = "Thumbnail Size: " + value;
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
                OnPropertyChanged("ChosenHandler");
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