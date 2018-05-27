using ServiceGUI.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ServiceGUI.Models;
using Infrustructure;
using System.Windows.Input;
using Prism.Commands;
namespace ServiceGUI.ViewModels
{

    /// <summary>
    /// the class for the view model of the GUI settings
    /// </summary>
    class SettingsViewModel : INotifyPropertyChanged
    {
        public ISettingsModel settingsModel;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// the constructor
        /// </summary>
        public SettingsViewModel()
        {
            this.settingsModel = SettingsModel.getModel();
            settingsModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e) {
                   NotifyPropertyChanged(e.PropertyName);
               };
            this.RemoveCommand = new DelegateCommand<object>(this.RemoveHandler, this.CanRemoveHandler);
            
        }
        //the outputdirectory name
        public string OutputDirectory
        {
            get { return this.settingsModel.OutputDirectory; }
            set
            {
                this.settingsModel.OutputDirectory = value;
            }
        }
        //the source name
        public string ServiceSourceName
        {
            get { return this.settingsModel.ServiceSourceName; }
            set
            {
                this.settingsModel.ServiceSourceName = value;
            }
        }
        //the log name
        public string ServiceLogName
        {
            get { return this.settingsModel.ServiceLogName; }
            set
            {
                this.settingsModel.ServiceLogName = value;
            }
        }
        //the thumbnail size
        public string ThumbSize
        {
            get { return this.settingsModel.ThumbSize; }
            set
            {
                this.settingsModel.ThumbSize = value;
            }
        }
        //the chosen handler
        public string ChosenHandler
        {
            get { return this.settingsModel.ChosenHandler; }
            set
            {
                this.settingsModel.ChosenHandler = value;
                //need to add
                var command = this.RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }
        //the handlers collection
        public ObservableCollection<string> DirectoriesCollection
        {
            get { return this.settingsModel.DirectoriesCollection; }
            set
            {
                this.settingsModel.DirectoriesCollection = value;
            }
        }

        public ICommand RemoveCommand { get; private set; }
        /// <summary>
        /// the class is responsible for the logic of the settings window
        /// </summary>
        private void RemoveHandler(object obj)
        {
           // string handler = obj.ToString();
            this.settingsModel.SendCommandToServer(CommandsEnum.RemoveDirCommand, this.ChosenHandler);
        }
        //checks if a chosen handler can be removed 
        private bool CanRemoveHandler(object obj)
        {
            if (string.IsNullOrEmpty(this.settingsModel.ChosenHandler))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Notify binded elements that the values have changed.
        /// </summary>
        /// <param name="name">The name of the field whose property was changed</param>
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

