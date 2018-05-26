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
namespace ServiceGUI.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public ISettingsModel settingsModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            this.settingsModel = SettingsModel.getModel();
            settingsModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e) {
                   NotifyPropertyChanged(e.PropertyName);
               };
            //this.removeHandlerCommand = new DelegateCommand<object>(this.removeHandlerCommand, this.CanRemoveHandler);
            
        }
        public string OutputDirectory
        {
            get { return this.settingsModel.OutputDirectory; }
            set
            {
                this.settingsModel.OutputDirectory = value;
            }
        }

        public string ServiceSourceName
        {
            get { return this.settingsModel.ServiceSourceName; }
            set
            {
                this.settingsModel.ServiceSourceName = value;
            }
        }

        public string ServiceLogName
        {
            get { return this.settingsModel.ServiceLogName; }
            set
            {
                this.settingsModel.ServiceLogName = value;
            }
        }

        public string ThumbSize
        {
            get { return this.settingsModel.ThumbSize; }
            set
            {
                this.settingsModel.ThumbSize = value;
            }
        }

        public string ChosenHandler
        {
            get { return this.settingsModel.ChosenHandler; }
            set
            {
                this.settingsModel.ChosenHandler = value;
                //need to add
                //var command = this.removeHandlerCommand as DelegateCommand<object>;
                //command.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<string> DirectoriesCollection
        {
            get { return this.settingsModel.DirectoriesCollection; }
            set
            {
                this.settingsModel.DirectoriesCollection = value;
            }
        }

        //public ICommand removeHandlerCommand { get; private set; }

        private void RemoveHandler(object obj)
        {
            //this.SettingsModel.SendCommandToServer();
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


        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

