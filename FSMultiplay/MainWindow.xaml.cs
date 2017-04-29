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

        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getOverrideCallsign")]
        public static extern bool mpmgr_getOverrideCallsign();
        [DllImport("MultiPlayDll.dll", EntryPoint = "mpmgr_getCallsign")]
        public static extern string mpmgr_getCallsign();

        private void setSimulator()
        {

        }

        private void setAircraft(bool isConnected)
        {
            if (isConnected)
            {
                txtCallsign.Text = /*mpmgr_getOverrideCallsign() ? mpmgr_getCallsign() : */ "PH-BLA";
                txtCallsign.Foreground = Brushes.Black;
                txtTitle.Text = "HS Twin_vista_CARIB";
                txtTitle.Foreground = Brushes.Black;

                txtAtcId.Text = "PH-BLA";
                txtAtcId.Foreground = Brushes.Black;
                txtAtcType.Text = "DEHAVILLAND";
                txtAtcType.Foreground = Brushes.Black;
                txtAtcModel.Text = "DHC6";
                txtAtcModel.Foreground = Brushes.Black;
                txtAtcAirline.Text = "-";
                txtAtcAirline.Foreground = Brushes.Black;
                txtAtcFlightNumber.Text = "-";
                txtAtcFlightNumber.Foreground = Brushes.Black;
            }
            else
            {
                txtCallsign.Text = "(callsign)";
                txtCallsign.Foreground = Brushes.LightGray;
                txtTitle.Text = "(aircraft title)";
                txtTitle.Foreground = Brushes.LightGray;

                txtAtcId.Text = "(callsign)";
                txtAtcId.Foreground = Brushes.LightGray;
                txtAtcType.Text = "(type)";
                txtAtcType.Foreground = Brushes.LightGray;
                txtAtcModel.Text = "(model)";
                txtAtcModel.Foreground = Brushes.LightGray;
                txtAtcAirline.Text = "(airline)";
                txtAtcAirline.Foreground = Brushes.LightGray;
                txtAtcFlightNumber.Text = "(flightnumber)";
                txtAtcFlightNumber.Foreground = Brushes.LightGray;
            }
        }

        private void mainSceHandler(int iEvent)
        {
            string msg = "Something happened";
            switch (iEvent) {
                case 0: // SCE_CONNECT
                    txtSimSummary.Text = "Connected to FSX";
                    imgNoSim.Visibility = Visibility.Collapsed;
                    imgFsx.Visibility = Visibility.Visible;
                    imgFsxSe.Visibility = Visibility.Collapsed;
                    imgP3d.Visibility = Visibility.Collapsed;
                    msg = "Connected to simulator";

                    setAircraft(true);
                    break;

                case 1: // SCE_DISCONNECT
                    txtSimSummary.Text = "Simulator not running";
                    imgNoSim.Visibility = Visibility.Visible;
                    imgFsx.Visibility = Visibility.Collapsed;
                    imgFsxSe.Visibility = Visibility.Collapsed;
                    imgP3d.Visibility = Visibility.Collapsed;
                    msg = "Disconnected from simulator";

                    setAircraft(false);
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
//                case 7: // SCE_OBJECTADDED
//                    msg = "AI object added";
//                    break;
//                case 8: // SCE_OBJECTREMOVED
//                    msg = "AI object removed";
//                    break;
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

            setAircraft(false);
        }

        private void menu_HelloWorld(object sender, RoutedEventArgs e)
        {
            txtSimSummary.Text = "Simulator not running";
            imgNoSim.Visibility = Visibility.Visible;
            imgFsx.Visibility = Visibility.Collapsed;
            imgFsxSe.Visibility = Visibility.Collapsed;
            imgP3d.Visibility = Visibility.Collapsed;

            setAircraft(false);
        }

        private void menu_NicePopup(object sender, RoutedEventArgs e)
        {
            txtSimSummary.Text = "Connected to FSX";
            imgNoSim.Visibility = Visibility.Collapsed;
            imgFsx.Visibility = Visibility.Visible;
            imgFsxSe.Visibility = Visibility.Collapsed;
            imgP3d.Visibility = Visibility.Collapsed;

            setAircraft(true);
        }

        private void menu_CantTouchThis(object sender, RoutedEventArgs e)
        {
            txtSimSummary.Text = "Connected to FSX-SE";
            imgNoSim.Visibility = Visibility.Collapsed;
            imgFsx.Visibility = Visibility.Collapsed;
            imgFsxSe.Visibility = Visibility.Visible;
            imgP3d.Visibility = Visibility.Collapsed;

            setAircraft(true);
        }

        private void menu_Goodbye(object sender, RoutedEventArgs e)
        {
            txtSimSummary.Text = "Connected to Prepar3D";
            imgNoSim.Visibility = Visibility.Collapsed;
            imgFsx.Visibility = Visibility.Collapsed;
            imgFsxSe.Visibility = Visibility.Collapsed;
            imgP3d.Visibility = Visibility.Visible;

            setAircraft(true);
        }
    }
}
