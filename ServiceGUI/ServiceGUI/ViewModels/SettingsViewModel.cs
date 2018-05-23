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
        private SettingsModel SettingsModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            SettingsModel = new SettingsModel();
            SettingsModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e) {
                   NotifyPropertyChanged(e.PropertyName);
               };
            //this.removeHandlerCommand = new DelegateCommand<object>(this.removeHandlerCommand, this.CanRemoveHandler);
            
        }
        public string OutputDirectory
        {
            get { return this.SettingsModel.OutputDirectory; }
            set
            {
                this.SettingsModel.OutputDirectory = value;
            }
        }

        public string ServiceSourceName
        {
            get { return this.SettingsModel.ServiceSourceName; }
            set
            {
                this.SettingsModel.ServiceSourceName = value;
            }
        }

        public string ServiceLogName
        {
            get { return this.SettingsModel.ServiceLogName; }
            set
            {
                this.SettingsModel.ServiceLogName = value;
            }
        }

        public string ThumbSize
        {
            get { return this.SettingsModel.ThumbSize; }
            set
            {
                this.SettingsModel.ThumbSize = value;
            }
        }

        public string ChosenHandler
        {
            get { return this.SettingsModel.ChosenHandler; }
            set
            {
                this.SettingsModel.ChosenHandler = value;
                //need to add
                //var command = this.removeHandlerCommand as DelegateCommand<object>;
                //command.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<string> DirectoriesCollection
        {
            get { return this.SettingsModel.DirectoriesCollection; }
            set
            {
                this.SettingsModel.DirectoriesCollection = value;
            }
        }

        //public ICommand removeHandlerCommand { get; private set; }

        private void RemoveHandler(object obj)
        {
            this.SettingsModel.SendCommandToServer();
        }
        //checks if a chosen handler can be removed 
        private bool CanRemoveHandler(object obj)
        {
            if (string.IsNullOrEmpty(this.SettingsModel.ChosenHandler))
            {
                return false;
            }
            return true;
        }


        protected void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

