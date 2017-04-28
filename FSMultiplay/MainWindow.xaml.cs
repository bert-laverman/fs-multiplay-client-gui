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
using System.Runtime.InteropServices;

namespace FSMultiplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("MultiPlayDll.dll", EntryPoint = "srvmgr_start")]
        public static extern void srvmgr_start();

        public delegate void CsSimConnectEventHandler(int iEvent);
        private CsSimConnectEventHandler sceHandler;
        [DllImport("MultiPlayDll.dll", EntryPoint = "scmgr_addSimconnectHandler")]
        public static extern void scmgr_addSimconnectHandler(CsSimConnectEventHandler callback);

        private void mainSceHandler(int iEvent)
        {
            string msg = "Something happened";
            switch (iEvent) {
                case 0: // SCE_CONNECT
                    msg = "Connected to simulator";
                    break;
                case 1: // SCE_DISCONNECT
                    msg = "Disconnected from simulator";
                    break;
                case 2: // SCE_START
                    msg = "Simulator engine started";
                    break;
                case 3: // SCE_STOP
                    msg = "Simulator engine stopped";
                    break;
                case 4: // SCE_PAUSE
                    msg = "Simulator engine paused";
                    break;
                case 5: // SCE_UNPAUSE
                    msg = "Simulator engine continued";
                    break;
                case 6: // SCE_AIRCRAFTLOADED
                    msg = "Loaded aircraft model";
                    break;
                case 7: // SCE_OBJECTADDED
                    msg = "AI object added";
                    break;
                case 8: // SCE_OBJECTREMOVED
                    msg = "AI object removed";
                    break;
                default:
                    break;
            }
            this.Dispatcher.Invoke(() =>
            {
                MainSnackbar.MessageQueue.Enqueue(msg);
            });
        }

        public MainWindow()
        {
            InitializeComponent();

            sceHandler = new CsSimConnectEventHandler(mainSceHandler);
            scmgr_addSimconnectHandler(sceHandler);

            srvmgr_start();
        }

        private void menu_OnPreviewMouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            //TODO
        }

        private void menu_HelloWorld(object sender, RoutedEventArgs e)
        {
            MainSnackbar.MessageQueue.Enqueue("Hello World!");
        }

        private void menu_NicePopup(object sender, RoutedEventArgs e)
        {
            MainSnackbar.MessageQueue.Enqueue("Nice popup...");
        }

        private void menu_Goodbye(object sender, RoutedEventArgs e)
        {
            MainSnackbar.MessageQueue.Enqueue("Goodbye");
        }
    }
}
