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

namespace ServiceGUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.MainWindowViewModel();

            ComunicationClient client = ComunicationClient.GetClient(8000);
            Controller controller = new Controller(LogModel.getModel(), null);
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
            client.sendCommand((int)CommandsEnum.LogsCommand, strs);

        }
    }
}
