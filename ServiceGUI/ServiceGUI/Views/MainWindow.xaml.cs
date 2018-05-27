using Infrustructure;
using ServiceGUI.Commands;
using ServiceGuiComunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServiceGUI.Models;

namespace ServiceGUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //important!!!!!!!!!!!!
        //we are sorry we didn't implement the mvvm for the main window as we should, we didn't have enough time to fix it.
        //we are aware of this mistake
        //but we implemented the mvvm for the other modules
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.MainWindowViewModel();

            ComunicationClient client = ComunicationClient.GetClient(8000);
            Controller controller = new Controller(LogModel.getModel(), SettingsModel.getModel());

            try
            {
                client.ConnectToServer();
                string[] strs = { };
                client.CommandReceived += delegate (object senderObj, CommandReceivedEventArgs args)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        JsonCommand jCommand = args.JsonCommand;
                        controller.ExecuteCommand(jCommand.CommandID, jCommand.Args, jCommand.JsonData);
                    });
                };
                client.sendCommand((int)CommandsEnum.GetConfigCommand, strs);
                client.sendCommand((int)CommandsEnum.LogsCommand, strs);
            }
            catch
            {
                this.Background = Brushes.Gray;
                this.IsEnabled = false;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.IsEnabled == true)
            {
                ComunicationClient.GetClient(8000).sendCommand((int)CommandsEnum.CloseCommand, null);
            }
        }

    }
}
